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
    void Hit(PlayerController playerHit);
    int GetGuid();
}