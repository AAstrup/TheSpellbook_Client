using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Match_EventHandler : IMatchEventHandler
{
    private Match_GUIHandler guiHandler;

    public Match_EventHandler(Match_GUIHandler guiHandler)
    {
        this.guiHandler = guiHandler;
        guiHandler.SetState_Initial();
    }

    public void SetUIState_Loading()
    {
    }

    public void SetUIState_JoiningGame()
    {
    }

    public void JoinedGame(Message_Response_GameAllConnected data)
    {
        guiHandler.SetState_InGame();
        InGameWrapper.instance.mapWrapper.Setup(data.AllPlayers.Count);
        InGameWrapper.instance.playersWrapper.SetupPlayersPositions();
        InGameWrapper.instance.clockWrapper.SetClockTime(data.gameClockTime);
    }

    public void MatchFinished(Message_Update_MatchFinished data)
    {
        guiHandler.SetState_WinLoss(data);
    }

    public void ConnectedSuccesful()
    {
    }

    public void ConnectedFailed()
    {
    }

    public void ConnectedAttempt(int connectionAttempts)
    {
    }
}