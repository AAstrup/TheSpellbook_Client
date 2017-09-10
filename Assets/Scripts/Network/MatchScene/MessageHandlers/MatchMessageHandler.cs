using UnityEngine;

internal class MatchMessageHandler : IMessageHandler
{
    Handler_Response_GameState handler_Response_GameState;
    public MatchMessageHandler()
    {
        Debug.Log("Match messagehandler started!");
        handler_Response_GameState = new Handler_Response_GameState();
    }

    public void Handle(object data)
    {
        Debug.Log("Match messagehandler got data of type " + data.GetType());
        if (data is Message_Response_GameState)
            handler_Response_GameState.Handle((Message_Response_GameState)data);
    }
}