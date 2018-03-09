using System;
using UnityEngine;
using UnityEngine.UI;

internal class RegisterLogin_GuiHandler
{
    private GameObject container;
    private GameObject popupGameObject;
    private Text popupText;
    private Button popupCloseButton;
    private InputField usernameInputField;
    private InputField passwordInputField;

    public RegisterLogin_GuiHandler(GameObject container)
    {
        this.container = container;
        popupGameObject = GameObject.Find("LoginResponsePopup");
        popupText = GameObject.Find("LoginResponsePopupResponse").GetComponent<Text>();
        popupCloseButton = GameObject.Find("LoginResponsePopupCloseButton").GetComponent<Button>();
        popupGameObject.SetActive(false);
        usernameInputField = GameObject.Find("UsernameLoginInputField").GetComponent<InputField>();
        passwordInputField = GameObject.Find("PasswordLoginInputField").GetComponent<InputField>();

        var signInButton = GameObject.Find("SignInButton").GetComponent<Button>();
        signInButton.onClick.AddListener(delegate { SignIn(); });

        var signUpButton = GameObject.Find("SignUpButton").GetComponent<Button>();
        signUpButton.onClick.AddListener(delegate { SignUp(); });
    }

    private void SignUp()
    {
        MM_DotNetAdapter.instance.DBRegister(usernameInputField.text, passwordInputField.text);
    }

    private void SignIn()
    {
        MM_DotNetAdapter.instance.DBLogin(usernameInputField.text, passwordInputField.text);
    }

    public void SetPopupMessage(string msg, bool showCloseButton = false)
    {
        if (!popupGameObject.activeSelf)
        {
            popupGameObject.SetActive(true);
        }
        popupText.text = msg;
        popupCloseButton.gameObject.SetActive(showCloseButton);
    }
}
