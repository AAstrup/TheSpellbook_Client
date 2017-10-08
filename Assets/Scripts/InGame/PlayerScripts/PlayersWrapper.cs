using System;
using System.Collections.Generic;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class PlayersWrapper
{
    private List<PlayerController> onlinePlayers;
    private PlayerController localPlayer;
    Dictionary<int, PlayerController> idToPlayerController;
    private PlayerFactory playerFactory;
    private UnityPlayerData playerData;

    public PlayersWrapper(UnityPlayerData playerData)
    {
        idToPlayerController = new Dictionary<int, PlayerController>();
        playerFactory = new PlayerFactory(playerData);
        this.playerData = playerData;
        onlinePlayers = new List<PlayerController>();
    }

    public void Update(float deltaTime)
    {
        if (localPlayer != null)
        {
            localPlayer.CheckInput();
            localPlayer.Update(deltaTime);
        }
        foreach (var item in onlinePlayers)
        {
            item.Update(deltaTime);
        }
    }

    /// <summary>
    /// Spawns a player and creates a playercontroller
    /// Also adds it to list of online players or sets it as the local player
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isMine">Whther or not it is owned by the local player</param>
    internal void SpawnPlayer(Message_ServerCommand_CreateGameObject data, bool isMine)
    {
        var player = playerFactory.CreatePlayer(playerData.playerPrefab, data);
        idToPlayerController.Add(data.GmjGUID, player);
        if (isMine)
            localPlayer = player;
        else
            onlinePlayers.Add(player);
    }

    public List<PlayerController> GetOnlyOnlinePlayers() { return onlinePlayers; }
    public PlayerController GetPlayerByGUID(int GMJGUID) {
        return idToPlayerController[GMJGUID];
    }
    public PlayerController GetOnlyLocalPlayer() { return localPlayer; }
}