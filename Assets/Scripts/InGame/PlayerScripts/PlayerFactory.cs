using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Responsible for creating the players
/// </summary>
public class PlayerFactory
{
    /// <summary>
    /// Creates player controllers and gameobjects
    /// </summary>
    /// <param name="playerData">Data for players</param>
    /// <returns></returns>
    public List<PlayerController> CreatePlayers(UnityPlayerData playerData)
    {
        //var playerList = new List<PlayerController>();
        //foreach (var setup in playerData.unitySinglePlayerSetup)
        //{
        //    playerList.Add(CreatePlayer(playerData.playerPrefab,setup, playerData));
        //}
        //return playerList;
        throw new NotImplementedException("This should be done networked, the server sending a message for each client");
    }

    private PlayerController CreatePlayer(GameObject playerPrefab,Vector3 pos,UnityPlayerData playerData)
    {
        GameObject gmj = GameObject.Instantiate(playerPrefab, pos, Quaternion.identity);
        var playerController = new PlayerController(gmj, playerData);
        return playerController;
    }
}