using System;
using UnityEngine;

internal class InGame_GUIHandler
{
    public static InGame_GUIHandler Instance;
    public InGame_GUIContainer GUI_Me;
    public InGame_GUIContainer GUI_Opp;
    public GameObject WonPanel;
    public GameObject LostPanel;


    public InGame_GUIHandler()
    {
        Instance = this;
        GUI_Me = new InGame_GUIContainer("Me_");
        GUI_Opp = new InGame_GUIContainer("Opponent_");
        WonPanel = GameObject.Find("WonPanel");
        WonPanel.SetActive(false);
        LostPanel = GameObject.Find("LostPanel");
        LostPanel.SetActive(false);
    }

    public void SetNames(Shared_InGame_PlayerInfo me, Shared_InGame_PlayerInfo opp)
    {
        GUI_Me.nameText.text = me.name;
        GUI_Opp.nameText.text = opp.name;
    }

    public void Won()
    {
        WonPanel.SetActive(true);
    }

    public void Lost()
    {
        LostPanel.SetActive(true);
    }
}