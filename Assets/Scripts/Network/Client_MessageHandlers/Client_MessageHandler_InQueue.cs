using System;
using UnityEngine;

internal class Client_MessageHandler_InQueue
{
    internal void Handle(Message_Response_InQueue data)
    {
        Debug.Log("Message_Response_InQueue :" + data.message);
        GUIHandler.Instance.SetUIState_Queue(data);
    }
}