using UnityEngine;

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
    /// <summary>
    /// When a spell is deemed dead it is removed
    /// Notice that this is checked after update is called
    /// </summary>
    /// <returns></returns>
    bool IsDead();
    /// <summary>
    /// Applies the spell on a player locally!
    /// This should be called through a message handler
    /// </summary>
    /// <param name="playerHit">Player hit</param>
    /// <param name="hitDirection">Hit direction from the spell to the player</param>c
    void Hit(PlayerController playerHit,Vector3 hitDirection, Vector3 updatePlayerPos, bool localHit);
    int GetGuid();
    /// <summary>
    /// Destroy any gameobjects that this controller is using
    /// </summary>
    void Destroy();
    /// <summary>
    /// Returns the time the spell has completed its casting state in mili seconds
    /// This is calculated by storing when a spell was casted and the time it takes to cast the spell
    /// </summary>
    double GetFinishCastTimeInMiliseconds();
}