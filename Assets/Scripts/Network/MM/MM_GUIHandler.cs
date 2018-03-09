using System;
using UnityEngine;
using UnityEngine.UI;

internal class MM_GUIHandler
{
    //Queuing up 
    public static MM_GUIHandler Instance;
    private GameObject Login;
    public RegisterLogin_GuiHandler registerLogin_GuiHandler;
    private GameObject MM;
    private GameObject Connecting;
    public Text ConnectingText;
    private GameObject InQueue;
    private GameObject QueueReady;
    private Image FillImage;
    private Counter sliderCounter;
    
    //Panels
    public MM_GUI_SettingsHandler settings;

    public MM_GUIHandler()
    {
        Instance = this;
        settings = new MM_GUI_SettingsHandler();
        Login = GameObject.Find("GUI_Login");
        registerLogin_GuiHandler = new RegisterLogin_GuiHandler(Login);
        MM = GameObject.Find("GUI_MainMenu");
        Connecting = GameObject.Find("GUI_Connecting");
        ConnectingText = GameObject.Find("ConnectingDescriptionText").GetComponent<Text>();
        InQueue = GameObject.Find("GUI_InQueue");
        QueueReady = GameObject.Find("GUI_QueueReadyCheck");
        FillImage = GameObject.Find("GUI_QueueReadyCheck_FillImage").GetComponent<Image>();
        SetUIState_Login();
    }

    public void Update()
    {
        settings.Update();
    }

    public void SetUIState_Login()
    {
        DisableAll();
        Login.SetActive(true);
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(GameObject.Find("LoginField"));
    }

    public void SetUIState_MM()
    {
        DisableAll();
        MM.SetActive(true);
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(GameObject.Find("BarButtonPlay"));
    }

    public void SetUIState_Connecting()
    {
        DisableAll();
        Connecting.SetActive(true);
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(GameObject.Find("BarButtonPlay"));
    }

    internal void SetUIState_ConnectingAttempt(int connectionAttempts)
    {
        SetUIState_Connecting();
        ConnectingText.text = "Connecting, attempt " + connectionAttempts;
    }

    internal void SetUISlider(Counter counter)
    {
        counter.timeUpdateEvent += UpdateImage;
    }

    void UpdateImage(float current,float max)
    {
        FillImage.fillAmount = current / max;
    }

    public void SetUIState_Queue(Message_Response_InQueue data)
    {
        DisableAll();
        InQueue.SetActive(true);
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(GameObject.Find("GUI_InQueue_Play"));
    }

    internal void SetUIState_QueueReady()
    {
        DisableAll();
        QueueReady.SetActive(true);
        GameObject.Find("AcceptQueueButtonContainer").GetComponent<Button>().interactable = true;
        EventSystemReference.instance.EventSystem.SetSelectedGameObject(GameObject.Find("AcceptQueueButtonContainer"));
    }

    private void DisableAll()
    {
        Login.SetActive(false);
        MM.SetActive(false);
        Connecting.SetActive(false);
        InQueue.SetActive(false);
        QueueReady.SetActive(false);
    }
}