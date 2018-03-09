using System;
using UnityEngine;
using UnityEngine.UI;

internal class Match_GUIHandler
{
    private string newLine;
    public static Match_GUIHandler Instance;
    public GameObject ResultsPanel;
    public Text ResultsHeaderText;
    public Text ResultsContentText;
    public GameObject LoadingPanel;
    public GameObject RoundStartPanel;
    public Text RoundStartText;

    public Match_GUIHandler()
    {
        newLine = Environment.NewLine + Environment.NewLine;
        Instance = this;
        ResultsPanel = GameObject.Find("ResultsPanel");
        ResultsHeaderText = GameObject.Find("ResultsHeaderText").GetComponent<Text>();
        ResultsContentText = GameObject.Find("ResultsContentText").GetComponent<Text>();
        LoadingPanel = GameObject.Find("LoadingPanel");
        RoundStartPanel = GameObject.Find("RoundStartPanel");
        RoundStartText = GameObject.Find("RoundStartText").GetComponent<Text>();
    }

    internal void SetState_Initial()
    {
        CloseAllWindows();
        LoadingPanel.SetActive(true);
    }

    public void SetState_NewRound()
    {
        CloseAllWindows();
        RoundStartPanel.SetActive(true);
    }

    void CloseAllWindows()
    {
        ResultsPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        RoundStartPanel.SetActive(false);
    }

    public void SetState_WinLoss(Message_Update_MatchFinished data)
    {
        CloseAllWindows();
        ResultsPanel.SetActive(true);
        ResultsHeaderText.text = data.won ? "You won" : "You lost";
        var scores = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().playerScoreController;
        string contentText = String.Concat("Rounds won ", data.playerScore.roundsWon, newLine, "Kills ", scores.GetKills(), newLine, "Deaths ", scores.GetDeaths());
        ResultsContentText.text = contentText;
    }

    internal void SetState_InGame()
    {
        CloseAllWindows();
    }
}