using System;
using UnityEngine;

internal class InGame_GUIHandler
{
    public static InGame_GUIHandler Instance;
    public InGame_GUIContainer GUI_Me;
    public InGame_GUIContainer GUI_Opp;

    public InGame_GUIHandler()
    {
        Instance = this;
        GUI_Me = new InGame_GUIContainer("Me_");
        GUI_Opp = new InGame_GUIContainer("Opponent_");
    }
}