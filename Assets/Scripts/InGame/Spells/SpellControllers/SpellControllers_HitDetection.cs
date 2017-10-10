using ClientServerSharedGameObjectMessages;
using UnityEngine;

public abstract class SpellControllers_HitDetection
{
    protected GameObject gmj;
    private float hitRange;
    protected float pushBackMultiplier;

    //Will be changed to a uniform implementation for spells that can hit multiple and so on
    //PlayerController is null until someone is hit
    protected PlayerController playerHit;
    protected bool isDead;

    public SpellControllers_HitDetection(SpellType spellType, GameObject gmj)
    {
        this.gmj = gmj;
        hitRange = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(spellType).hitRange;
        pushBackMultiplier = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(spellType).pushBackMultiplier;
    }
    
    /// <summary>
    /// Temporary implementation
    /// Checks for collision against other player
    /// Dont call this if it has hit something already (Temporary implementation until a more broad is designed for different types of behavior on hit)
    /// </summary>
    protected bool CheckForCollisions() {
        foreach (var player in InGameWrapper.instance.playersWrapper.GetOnlyOnlinePlayers())
        {
            if(Vector3.Distance(player.GetGmj().transform.position, gmj.transform.position) <= hitRange)
            {
                playerHit = player;
                isDead = true;
                return true;
            }
        }
        return false;
    }

    protected PlayerController GetPlayerHit()
    {
        return playerHit;
    }
}