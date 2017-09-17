using System;

public class Handler_Update_MatchFinished
{
    public void Handle(Message_Update_MatchFinished data)
    {
        if(data.won)
            InGame_GUIHandler.Instance.Won();
        else
            InGame_GUIHandler.Instance.Lost();
    }
}