using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdapterOfflineMatch : MonoBehaviour {
    private GameEngine gameEngine;
    private InGame_GUIHandler guiHandler;

    void Start () {
        var me = AppConfig.GetPersistentData().PlayerInfo;
        me.GUID = 0;
        var bot = new Shared_PlayerInfo() { name = "Bot" };
        bot.GUID = 1;
        var sender = new GameEngineSender_LocalSender();
        sender.playerWon += Win;
        gameEngine = new GameEngine(sender, me, bot);

        guiHandler = new InGame_GUIHandler();
        guiHandler.SetNames(gameEngine.p1, gameEngine.p2);
    }

    public void TestWinCardThrown()
    {
        int cardId = 0;
        int GUID = gameEngine.turnManager.GetCurrentPlayersTurn().GUID;
        gameEngine.PlayCard(cardId,GUID);
    }

    private void Win(int playerGUID)
    {
        if (playerGUID == gameEngine.p1.GUID)
            InGame_GUIHandler.Instance.Won();
        else
            InGame_GUIHandler.Instance.Lost();
    }
}
