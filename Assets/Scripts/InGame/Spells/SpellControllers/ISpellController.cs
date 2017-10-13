﻿using UnityEngine;

public interface ISpellController
{
    /// <summary>
    /// Called on spells that is controlled locally
    /// This is the case for spells spawned by the local player
    /// </summary>
    /// <param name="deltaTime"></param>
    void LocalUpdate(float deltaTime);
    /// <summary>
    /// Called on spells that is controlled online
    /// </summary>
    /// <param name="deltaTime"></param>
    void OnlineUpdate(float deltaTime);
    bool IsDead();
    /// <summary>
    /// Applies the spell effect on a player hit
    /// </summary>
    /// <param name="playerHit">Player hit</param>
    /// <param name="hitDirection">Hit direction from the spell to the player</param>
    void Hit(PlayerController playerHit,Vector3 hitDirection, Vector3 updatePlayerPos);
    int GetGuid();
}