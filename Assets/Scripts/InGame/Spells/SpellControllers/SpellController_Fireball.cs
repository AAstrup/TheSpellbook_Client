using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal class SpellController_Fireball : SpellControllers_HitDetection, ISpellController
{
    private Message_ServerResponse_CreateSpellWithDirection spell;
    private Vector3 direction;
    private UnitySpellDefinition unityDefinition;

    public SpellController_Fireball(Message_ServerResponse_CreateSpellWithDirection spell,UnitySpellDefinition definition) 
        : base(spell.request.spellType, definition, new Vector3(spell.request.spellXPos, UnityStaticValues.StaticYPos, spell.request.playerZPos), spell.request.TimeStartedCasting,
            new Vector3(spell.request.playerXPos, UnityStaticValues.StaticYPos,spell.request.playerZPos),spell.request.ownerGUID, spell.request.rank)
    {
        this.spell = spell;
        direction = new Vector3(spell.request.spellXDir, 0f, spell.request.spellZDir);
        unityDefinition = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(SpellType.Fireball);
        InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID).spellCaster.CastSpell(this, spell.request.playerXPos, spell.request.playerZPos);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LocalUpdate(float deltaTime)
    {
        if (!base.FinishedCasting())
            return;
        projectile.transform.position += direction * deltaTime * unityDefinition.GetMoveSpeed(spell.request.rank);
        if (CheckForCollisions()) {
            Vector3 direction = CalCulateDirection(playerHit);
            Hit(playerHit, direction,playerHit.GetGmj().transform.position,true);
            SendCollisionsMessage(direction,playerHit.GetGmj().transform.position);
        }
    }

    private Vector3 CalCulateDirection(PlayerController playerHit)
    {
        Vector3 direction = playerHit.GetGmj().transform.position - projectile.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        direction.Normalize();
        return direction;
    }

    private void SendCollisionsMessage(Vector3 hitDirection,Vector3 playerHitPos)
    {
        Message_ClientCommand_SpellHit msg = new Message_ClientCommand_SpellHit()
        {
            playerGMJHit = playerHit.GetID(),
            spellHitGmjID = spell.spellID,
            hitDirectionX = hitDirection.x,
            hitDirectionZ = hitDirection.z,
            playerPosX = playerHitPos.x,
            playerPosZ = playerHitPos.z
        };
        Match_DotNetAdapter.instance.Send(msg);
    }

    public void OnlineUpdate(float deltaTime)
    {
        if (!FinishedCasting())
            return;
        projectile.transform.position += direction * deltaTime * unityDefinition.GetMoveSpeed(spell.request.rank);
    }

    public void Hit(PlayerController playerHit, Vector3 hitDirection, Vector3 playerUpdatedPos,bool isLocal)
    {
        isDead = true;
        if (isLocal)
        {
            playerHit.ApplyPushBack(hitDirection * pushBackMultiplier * UnityStaticValues.PushBackMultiplier);
        }

        var caster = InGameWrapper.instance.playersWrapper.GetPlayerByOwnerGUID(spell.request.ownerGUID);
        playerHit.healthController.Damage(unityDefinition.upgrades[spell.request.rank].damage, caster.GetID());

        GameObject effectGmj = GameObject.Instantiate(unityDefinition.hitEffectPrefab, projectile.transform.position, Quaternion.identity);
        effectGmj.transform.rotation = Quaternion.LookRotation(hitDirection);
        GameObject.Destroy(projectile);
    }

    public int GetGuid()
    {
        return spell.spellID;
    }

    public double GetFinishCastTimeInMiliseconds()
    {
        return spell.request.TimeStartedCasting + unityDefinition.GetCastTimeInMiliSeconds(spell.request.rank);
    }
}