using UnityEngine;

internal class MatchMessageHandler : IMessageHandler
{
    Handler_Response_GameState handler_Response_GameState;
    Handler_Update_MatchFinished handler_Update_MatchFinished;
    public MatchMessageHandler()
    {
        Debug.Log("Match messagehandler started!");
        handler_Response_GameState = new Handler_Response_GameState();
        handler_Update_MatchFinished = new Handler_Update_MatchFinished();
    }

    public void Handle(object data)
    {
        Debug.Log("Match messagehandler got data of type " + data.GetType());
        if (data is Message_Response_GameState)
            handler_Response_GameState.Handle((Message_Response_GameState)data);
        else if (data is Message_Update_MatchFinished)
            handler_Update_MatchFinished.Handle((Message_Update_MatchFinished)data);
    }
}