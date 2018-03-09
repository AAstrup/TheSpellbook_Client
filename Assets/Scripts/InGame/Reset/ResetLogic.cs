using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class ResetLogic
{
    private string waitTimeMessage;
    private double timeForNextRound = Double.NegativeInfinity;
    private double timeToGetReady = 1000.0;
    private GameObject UIShop;

    public ResetLogic()
    {
        UIShop = GameObject.Find("UIShop");
    }

    internal void StartNewRound()
    {
        InGameWrapper.instance.roundActive = true;
        InGameWrapper.instance.mapWrapper.Reset();
        ResetPlayerController(InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer());
        foreach (var onlinePlayer in InGameWrapper.instance.playersWrapper.GetOnlyOnlinePlayers())
        {
            ResetPlayerController(onlinePlayer);
        }
        InGameWrapper.instance.spellsWrapper.TEMPORARYDESTROYALLSPELLS();
    }

    internal void QueueNewRound(double timeForNextRound, string waitTimeMessage)
    {
        this.waitTimeMessage = waitTimeMessage;
        this.timeForNextRound = timeForNextRound;
        Match_GUIHandler.Instance.SetState_NewRound();
        InGameWrapper.instance.roundActive = false;
    }

    public void ResetPlayerController(PlayerController player)
    {
        player.Revive();
        var newPos = InGameWrapper.instance.mapWrapper.GetPositionForPlayer(player);
        UISpellButtonWrapper.instance.Reset();
        player.Reset();
        player.SetCurrentPos(newPos);
        player.SetTargetPos(newPos);
    }

    internal void Update(float deltaTime)
    {
        if (timeForNextRound < 0)
        {
            return;
        }
        
        if((timeForNextRound - timeToGetReady) > InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds())
        {
            UIShop.SetActive(true);
            Match_GUIHandler.Instance.RoundStartText.text = waitTimeMessage + ((int)(timeForNextRound - InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds())/1000).ToString();
        }
        else if (timeForNextRound > InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds())
        {
            UIShop.SetActive(false);
            Match_GUIHandler.Instance.RoundStartText.text = waitTimeMessage + ((int)(timeForNextRound - InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds())/1000).ToString();
        }
        else if(Match_GUIHandler.Instance.RoundStartPanel.activeSelf)
        {
            Match_GUIHandler.Instance.RoundStartPanel.SetActive(false);
            StartNewRound();
        }
    }
}