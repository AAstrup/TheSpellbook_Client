using System;
using System.Collections.Generic;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class PlayersWrapper
{
    private List<PlayerController> onlinePlayers;
    private PlayerController localPlayer;
    Dictionary<int, PlayerController> idToPlayerController;
    Dictionary<int, PlayerController> ownerToPlayerController;
    private PlayerFactory playerFactory;
    public UnityPlayerData playerData;

    public PlayersWrapper(UnityPlayerData playerData, IDeviceInput deviceInput)
    {
        idToPlayerController = new Dictionary<int, PlayerController>();
        ownerToPlayerController = new Dictionary<int, PlayerController>();
        playerFactory = new PlayerFactory(playerData, deviceInput);
        this.playerData = playerData;
        onlinePlayers = new List<PlayerController>();
    }

    internal void SetupPlayersPositions()
    {
        if (localPlayer != null)
        {
            localPlayer.GetGmj().transform.position = InGameWrapper.instance.mapWrapper.GetPositionForPlayer(localPlayer);
            localPlayer.SetTargetPos(localPlayer.GetGmj().transform.position);
        }
        foreach (var item in onlinePlayers)
        {
            item.GetGmj().transform.position = InGameWrapper.instance.mapWrapper.GetPositionForPlayer(item);
            item.SetTargetPos(item.GetGmj().transform.position);
        }
    }

    /// <summary>
    /// If setup has been run setup playercontroller at position
    /// </summary>
    /// <param name="playerController"></param>
    internal void SetupPlayerPositionIfPossible(PlayerController playerController)
    {
        if (InGameWrapper.instance.mapWrapper.HasBeenSetup())
        {
            playerController.GetGmj().transform.position = InGameWrapper.instance.mapWrapper.GetPositionForPlayer(playerController);
            playerController.SetTargetPos(playerController.GetGmj().transform.position);
        }
    }

    public void Update(float deltaTime)
    {
        if (localPlayer != null)
        {
            if (localPlayer.IsAlive())
            {
                localPlayer.LocalUpdate(deltaTime);
            }
        }
        foreach (var item in onlinePlayers)
        {
            if(item.IsAlive())
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
        ownerToPlayerController.Add(data.OwnerGUID, player);
        if (isMine)
            localPlayer = player;
        else
            onlinePlayers.Add(player);
    }

    internal void PlayerLeft(int playerLeftGUID)
    {
        for (int i = 0; i < onlinePlayers.Count; i++)
        {
            if (onlinePlayers[i].GetOwnerID() == playerLeftGUID) {
                GameObject.Destroy(onlinePlayers[i].GetGmj());
                onlinePlayers.Remove(onlinePlayers[i]);
                break;
            }
        }
    }

    public List<PlayerController> GetOnlyOnlinePlayers() { return onlinePlayers; }
    public PlayerController GetPlayerByGUID(int GMJGUID) {
        return idToPlayerController[GMJGUID];
    }
    public PlayerController GetPlayerByOwnerGUID(int ownerGUID)
    {
        return ownerToPlayerController[ownerGUID];
    }
    public PlayerController GetOnlyLocalPlayer() { return localPlayer; }
}