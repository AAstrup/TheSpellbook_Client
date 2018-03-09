using System;
using UnityEngine;
/// <summary>
/// Controls a playercontroller casting of spells
/// </summary>
public class SpellCasterController
{
    private ISpellController spellBeingCasted;
    public bool spellSpawnRequestIssuedLocally; //Spell request send to server, should not be allowed to send more requests at once
    private PlayerController playerController;

    public SpellCasterController(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    /// <summary>
    /// Local spell request is send
    /// </summary>
    internal void SpellRequestSend()
    {
        spellSpawnRequestIssuedLocally = true;
    }

    /// <summary>
    /// Returns whether or not the playercontroller is casting a spell 
    /// or if a request to cast a spell has been send
    /// </summary>
    /// <returns>If a spell is being casted</returns>
    public bool IsCasting()
    {
        if (spellSpawnRequestIssuedLocally)
            return true;
        if (spellBeingCasted == null)
            return false;
        return spellBeingCasted.GetFinishCastTimeInMiliseconds() > InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds();
    }

    /// <summary>
    /// A response from the server stating a player is casting a spell has been recieved
    /// This message states that a spell is being casted.
    /// </summary>
    /// <param name="spell">Spell being casted</param>
    /// <param name="playerXPos">Casting X position of the spell</param>
    /// <param name="playerZPos">Casting Z position of the spell</param>
    public void CastSpell(ISpellController spell, float playerXPos, float playerZPos)
    {
        var playerPos = new Vector3(playerXPos, UnityStaticValues.StaticYPos, playerZPos);
        this.spellBeingCasted = spell;
        playerController.SetCurrentPos(playerPos);
        playerController.SetTargetPos(playerPos);
        playerController.playerAnimator.CastSpell();
        if (InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer() == playerController)
            spellSpawnRequestIssuedLocally = false;
    }
}