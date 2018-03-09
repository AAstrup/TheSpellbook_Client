using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class SpellController_Teleport : SpellController_Base, ISpellController
{
    private Message_ServerResponse_CreateSpellInStaticPosition spell;
    private UnitySpellDefinition definition;
    bool isDead;

    public SpellController_Teleport(Message_ServerResponse_CreateSpellInStaticPosition spell, UnitySpellDefinition definition)
        : base(spell.request.TimeStartedCasting, definition.GetCastTimeInSeconds(spell.request.rank))
    {
        this.spell = spell;
        this.definition = definition;
        InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID).spellCaster.CastSpell(this, spell.request.playerXPos, spell.request.playerZPos);

    }

    public void Destroy()
    {
    }

    public double GetFinishCastTimeInMiliseconds()
    {
        return spell.request.TimeStartedCasting + definition.GetCastTimeInMiliSeconds(spell.request.rank);
    }

    public int GetGuid()
    {
        return spell.spellID;
    }

    public void Hit(PlayerController playerHit, Vector3 hitDirection, Vector3 updatePlayerPos, bool localHit)
    {
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LocalUpdate(float deltaTime)
    {
        if (base.FinishedCasting() && !IsDead())
        {
            Teleport();
        }
    }

    public void OnlineUpdate(float deltaTime)
    {
        if (base.FinishedCasting() && !IsDead())
        {
            Teleport();
        }
    }

    private void Teleport()
    {
        isDead = true;
        var player = InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID);
        player.SetCurrentPos(new Vector3(spell.request.spellXPos, StaticVariables.GetYPos(), spell.request.spellZPos));
        player.SetTargetPos(new Vector3(spell.request.spellXPos, StaticVariables.GetYPos(), spell.request.spellZPos));
    }
}