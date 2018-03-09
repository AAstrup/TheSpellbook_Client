using ClientServerSharedGameObjectMessages;
using UnityEngine;

public abstract class SpellControllers_HitDetection : SpellController_Base
{
    protected GameObject projectile;
    private float hitRange;
    protected float pushBackMultiplier;

    //Will be changed to a uniform implementation for spells that can hit multiple and so on
    //PlayerController is null until someone is hit
    protected PlayerController playerHit;
    protected bool isDead;

    public SpellControllers_HitDetection(SpellType spellType, UnitySpellDefinition unitySpellDefinition,Vector3 spawnPos, double timeStartedCasting,Vector3 playerCastPos, int ownerGUID, int rank) 
        : base(timeStartedCasting, unitySpellDefinition.GetCastTimeInSeconds(rank))
    {
        projectile = (GameObject)GameObject.Instantiate(unitySpellDefinition.projectilePrefab, new Vector3(spawnPos.x, spawnPos.y, spawnPos.z), Quaternion.identity);

        hitRange = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(spellType).GetHitRange(rank);
        pushBackMultiplier = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(spellType).GetPushBackMultiplier(rank);
    }
    
    /// <summary>
    /// Temporary implementation
    /// Checks for collision against other player
    /// Dont call this if it has hit something already (Temporary implementation until a more broad is designed for different types of behavior on hit)
    /// </summary>
    protected bool CheckForCollisions() {
        foreach (var player in InGameWrapper.instance.playersWrapper.GetOnlyOnlinePlayers())
        {
            var dist = Vector2.Distance(new Vector2(player.GetGmj().transform.position.x, player.GetGmj().transform.position.z), new Vector2(projectile.transform.position.x, projectile.transform.position.z));

            if (dist <= hitRange)
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

    public void Destroy()
    {
        GameObject.Destroy(projectile);
    }
}