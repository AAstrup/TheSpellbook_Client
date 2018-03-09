using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal class SpellController_Explode : SpellController_Base, ISpellController
{
    private Message_ServerResponse_CreateSpellInStaticPosition spell;
    private UnitySpellDefinition unityDefinition;
    private float fireTime;
    private bool isDead;
    private PlayerController caster;

    public SpellController_Explode(Message_ServerResponse_CreateSpellInStaticPosition spell, UnitySpellDefinition definition)
        : base(spell.request.TimeStartedCasting,definition.GetCastTimeInSeconds(spell.request.rank))
    {
        this.spell = spell;
        this.unityDefinition = definition;
        InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID).spellCaster.CastSpell(this, spell.request.playerXPos, spell.request.playerZPos);
    }

    private void Explode()
    {
        Debug.Log("Exploded");
        isDead = true;
        caster = InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID);
        GameObject.Instantiate(unityDefinition.castEffectPrefab,caster.GetGmj().transform.position,Quaternion.identity);

        foreach (var player in InGameWrapper.instance.playersWrapper.GetOnlyOnlinePlayers())
        {
            if (player == caster)
                continue;
            PushAPlayer(caster.GetGmj().transform.position, player);
        }
        var localPlayer = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer();
        if (localPlayer != caster)
        {
            PushAPlayer(caster.GetGmj().transform.position, localPlayer);
        }
    }

    public void PushAPlayer(Vector3 castTargetPosition,PlayerController player)
    {
        if (Vector3.Distance(castTargetPosition, player.GetGmj().transform.position) < unityDefinition.GetHitRange(spell.request.rank))
        {
            Vector3 delta = player.GetGmj().transform.position - castTargetPosition;
            float range = delta.magnitude;
            delta.Normalize();
            delta = delta * unityDefinition.GetHitRange(spell.request.rank) / range;
            player.ApplyPushBack(delta * unityDefinition.GetPushBackMultiplier(spell.request.rank) * UnityStaticValues.PushBackMultiplier);
        }
    }

    public void Destroy()
    {
    }

    public int GetGuid()
    {
        return spell.spellID;
    }

    public void Hit(PlayerController playerHit, Vector3 hitDirection, Vector3 playerUpdatedPos, bool localHit)
    {
        if (localHit)
        {
            playerHit.ApplyPushBack(hitDirection * unityDefinition.GetPushBackMultiplier(spell.request.rank) * UnityStaticValues.PushBackMultiplier);
        }

        playerHit.healthController.Damage(unityDefinition.upgrades[spell.request.rank].damage, caster.GetID());

        GameObject effectGmj = GameObject.Instantiate(unityDefinition.hitEffectPrefab, caster.GetGmj().transform.position, Quaternion.identity);
        effectGmj.transform.rotation = Quaternion.LookRotation(hitDirection);
        Debug.Log("Players new pushback is " + playerHit.DEBUGGetPushback() + " " + playerHit);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LocalUpdate(float deltaTime)
    {
        if(base.FinishedCasting() && !IsDead())
        {
            Explode();
        }
    }

    public void OnlineUpdate(float deltaTime)
    {
        if (base.FinishedCasting() && !IsDead())
        {
            Explode();
        }
    }

    public double GetFinishCastTimeInMiliseconds()
    {
        return spell.request.TimeStartedCasting + unityDefinition.GetCastTimeInMiliSeconds(spell.request.rank);
    }
}