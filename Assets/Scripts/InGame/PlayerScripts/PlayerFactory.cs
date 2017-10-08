using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Responsible for creating the players
/// </summary>
public class PlayerFactory
{
    private UnityPlayerData playerData;

    public PlayerFactory(UnityPlayerData playerData)
    {
        this.playerData = playerData;
    }

    public PlayerController CreatePlayer(GameObject playerPrefab, Message_ServerCommand_CreateGameObject info)
    {
        GameObject gmj = GameObject.Instantiate(playerPrefab, new Vector3(info.transform.xPos,playerData.StaticYPos, info.transform.zPos), Quaternion.identity);
        var playerController = new PlayerController(gmj, info,playerData);
        Debug.Log("Spawning gmj with name " + gmj.name);
        return playerController;
    }
}