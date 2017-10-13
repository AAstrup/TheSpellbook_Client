using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal class SpellController_Fireball : SpellControllers_HitDetection, ISpellController
{
    private Message_ServerResponse_CreateSpell spell;
    private Vector3 direction;

    public SpellController_Fireball(GameObject gmj, Message_ServerResponse_CreateSpell spell) : base(spell.request.spellType,gmj)
    {
        this.spell = spell;
        direction = new Vector3(spell.request.xDir, 0f, spell.request.zDir);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void LocalUpdate(float deltaTime)
    {
        gmj.transform.position += direction * deltaTime;
        if (CheckForCollisions()) {
            Vector3 direction = CalCulateDirection(playerHit);
            Hit(playerHit, direction,playerHit.GetGmj().transform.position);
            SendCollisionsMessage(direction,playerHit.GetGmj().transform.position);
        }
    }

    private Vector3 CalCulateDirection(PlayerController playerHit)
    {
        Vector3 direction = playerHit.GetGmj().transform.position - gmj.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        direction.Normalize();
        return direction;
    }

    private void SendCollisionsMessage(Vector3 hitDirection,Vector3 playerHitPos)
    {
        Message_ClientCommand_SpellHit msg = new Message_ClientCommand_SpellHit()
        {
            playerGMJHit = playerHit.GetID(),
            spellHitGmjID = spell.spellGmjID,
            hitDirectionX = hitDirection.x,
            hitDirectionZ = hitDirection.z,
            playerPosX = playerHitPos.x,
            playerPosZ = playerHitPos.z
        };
        Match_DotNetAdapter.instance.Send(msg);
    }

    public void OnlineUpdate(float deltaTime)
    {
        gmj.transform.position += direction * deltaTime;
    }

    public void Hit(PlayerController playerHit, Vector3 hitDirection, Vector3 playerUpdatedPos)
    {
        GameObject.Destroy(gmj);
        isDead = true;
        playerHit.SetCurrentPos(playerUpdatedPos);
        playerHit.ApplyPushBack(hitDirection * pushBackMultiplier * UnityStaticValues.PushBackMultiplier);
        InGameWrapper.instance.spellsWrapper.DestroySpell(this,false);
    }

    public int GetGuid()
    {
        return spell.spellGmjID;
    }
}