using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayersWrapper
{
    private List<PlayerController> players;
    private PlayerController localPlayer;
    Dictionary<int, PlayerController> GUIDToPlayer;

    public PlayersWrapper(UnityPlayerData playerData)
    {
        GUIDToPlayer = new Dictionary<int, PlayerController>();
        var playerFactory = new PlayerFactory();
        players = playerFactory.CreatePlayers(playerData);
    }

    internal void UpdateEnemy(Message_Command_PlayerMovementUpdate data)
    {
        GUIDToPlayer[data.GMJGUID].SetTargetPos(new Vector3(data.moveTargetXPos, StaticVariables.GetYPos(), data.moveTargetZPos));
        GUIDToPlayer[data.GMJGUID].SetCurrentPos(new Vector3(data.currentXPos, StaticVariables.GetYPos(), data.currentZPos));
    }

    internal void Update(float deltaTime)
    {
        foreach (var item in players)
        {
            item.Update(deltaTime);
        }
    }

    public List<PlayerController> GetPlayers() { return players; }

    internal PlayerController GetLocalPlayer()
    {
        return localPlayer;
    }
}