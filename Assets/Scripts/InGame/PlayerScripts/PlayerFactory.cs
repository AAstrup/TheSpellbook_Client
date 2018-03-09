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
    private IDeviceInput deviceInput;

    public PlayerFactory(UnityPlayerData playerData, IDeviceInput deviceInput)
    {
        this.playerData = playerData;
        this.deviceInput = deviceInput;
    }

    public PlayerController CreatePlayer(GameObject playerPrefab, Message_ServerCommand_CreateGameObject info)
    {
        GameObject gmj = GameObject.Instantiate(playerPrefab, new Vector3(info.transform.xPos, UnityStaticValues.StaticYPos, info.transform.zPos), Quaternion.identity);
        var playerController = new PlayerController(gmj, info, playerData, deviceInput);
        InGameWrapper.instance.playersWrapper.SetupPlayerPositionIfPossible(playerController);
        return playerController;
    }
}