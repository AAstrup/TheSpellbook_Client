using System;

public class PlayerScoreController
{
    int kills;
    int deaths;

    /// <summary>
    /// Adds when the player kills someone else
    /// </summary>
    /// <param name="dyingPlayerGUID">Player it killed</param>
    internal void AddKill(int dyingPlayerGUID)
    {
        kills++;
    }

    /// <summary>
    /// Adds when the player dies
    /// </summary>
    /// <param name="killerPlayerGUID">If killed by a player, the playercontroller guid is given</param>
    internal void AddDeath(int? killerPlayerGUID)
    {
        deaths++;
    }

    internal int GetKills()
    {
        return kills;
    }

    internal int GetDeaths()
    {
        return deaths;
    }
}