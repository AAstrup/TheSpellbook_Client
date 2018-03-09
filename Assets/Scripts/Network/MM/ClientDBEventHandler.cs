using ClientDB;
using UnityEngine;

internal class ClientDBEventHandler : IClientDBEventHandler
{
    public object serializableMsg;
    private MM_GUIHandler gUIHandler;
    public DBClient client;

    public void SendMessageOnConnectionSuccesfull(MM_GUIHandler gUIHandler,DBClient client,object serializableMsg)
    {
        this.gUIHandler = gUIHandler;
        this.client = client;
        this.serializableMsg = serializableMsg;
    }

    public void ConnectingAttempt(int connectionAttempts)
    {
        MM_GUIHandler.Instance.registerLogin_GuiHandler.SetPopupMessage("Connecting attempt " + connectionAttempts);
    }

    public void ConnectingFailed()
    {
        MM_GUIHandler.Instance.registerLogin_GuiHandler.SetPopupMessage("Server not available!", true);
    }

    public void ConnectingSuccesful()
    {
        client.Send(serializableMsg);
    }

    public void LoginResponse(Message_ServerResponse_Login objData)
    {
        if (objData.loginSucceded)
        {
            UnityConfig.GetPersistentDataContainer().SetProfile(objData);
            gUIHandler.SetUIState_MM();
        }
        else
        {
            MM_GUIHandler.Instance.registerLogin_GuiHandler.SetPopupMessage("Login suceeded were " + objData.loginSucceded, true);
        }
    }

    public void RegisterAndLoginResponse(Message_ServerResponse_Register objData)
    {
        if (objData.succeded)
        {
            UnityConfig.GetPersistentDataContainer().SetProfile(objData);
            gUIHandler.SetUIState_MM();
        }
        else
        {
            MM_GUIHandler.Instance.registerLogin_GuiHandler.SetPopupMessage(objData.message, true);
        }
    }
}