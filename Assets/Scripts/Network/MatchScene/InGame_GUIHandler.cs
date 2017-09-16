using System;
using Match;
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

    internal void SetNames(Message_Response_GameState data)
    {
        GUI_Me.nameText.text = data.me.name;
        GUI_Opp.nameText.text = data.opp.name;
    }

    internal void Won()
    {
        WonPanel.SetActive(true);
    }

    internal void Lost()
    {
        LostPanel.SetActive(true);
    }
}