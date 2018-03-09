using UnityEngine;
using UnityEngine.UI;

internal class MM_GUI_Settings_TestBuildHandler :ISettingsPanelHandler
{
    private InputField ipInputField;
    private string ipInputStartValue;
    

    public MM_GUI_Settings_TestBuildHandler()
    {
        ipInputField = GameObject.Find("GUI_Settings_Debug_IP_InputField").GetComponent<InputField>();
        ipInputField.onEndEdit.AddListener(delegate { IPChanged(); });
        ipInputField.text = UnityClientConfig.instance.GetString("IpOfMatchMaker");
    }

    public string GetPanelName() { return "TestBuild"; }

    public void IPChanged()
    {
        if (ipInputField.text.Trim().Length != 0)
        {
            UnityClientConfig.instance.SetValue("IpOfMatch", ipInputField.text.Trim());
            UnityClientConfig.instance.SetValue("IpOfMatchMaker", ipInputField.text.Trim());
        }

        ipInputField.text = UnityClientConfig.instance.GetString("IpOfMatchMaker");
    }
}