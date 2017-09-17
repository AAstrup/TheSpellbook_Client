using System;

internal class Handler_Response_GameState
{
    internal void Handle(Message_Response_GameState data)
    {
        InGame_GUIHandler.Instance.SetNames(data.me,data.opp);
    }
}