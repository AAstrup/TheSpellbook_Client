using UnityEngine;
using UnityEngine.SceneManagement;

internal class Match_EventHandler : IMatchEventHandler
{
    private Match_GUIHandler guiHandler;

    public Match_EventHandler(Match_GUIHandler guiHandler)
    {
        this.guiHandler = guiHandler;
        guiHandler.SetState_Default();
    }

    public void SetUIState_Loading()
    {
    }

    public void SetUIState_JoiningGame()
    {
    }

    public void JoinedGame(Message_Response_GameAllConnected data)
    {
        Debug.Log("All connected " + data.AllPlayers.Count);
        Debug.Log("Me " + data.requestingPlayer.name);
        Debug.Log("All players");
        foreach (var item in data.AllPlayers)
        {
            Debug.Log("Player: " + item.name);
        }
    }

    public void MatchFinished(Message_Update_MatchFinished data)
    {
        if (data.won)
            guiHandler.SetState_Win();
        else
            guiHandler.SetState_Loss();
    }
}