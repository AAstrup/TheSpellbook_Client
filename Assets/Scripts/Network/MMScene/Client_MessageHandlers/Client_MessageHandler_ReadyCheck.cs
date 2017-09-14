using System;

internal class Client_MessageHandler_ReadyCheck
{
    public Client_MessageHandler_ReadyCheck()
    {
    }

    internal void Handle(Message_ServerRequest_ReadyCheck data)
    {
        MM_GUIHandler.queueReadyMsg = data;
        MM_GUIHandler.Instance.SetUIState_QueueRead();
    }
}