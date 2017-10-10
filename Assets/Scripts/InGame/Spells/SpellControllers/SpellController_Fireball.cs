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
            Hit(playerHit);
            SendCollisionsMessage();
        }
    }

    private void SendCollisionsMessage()
    {
        Message_ClientCommand_SpellHit msg = new Message_ClientCommand_SpellHit()
        {
            playerGMJHit = playerHit.GetID(),
            spellHitGmjID = spell.spellGmjID
        };
        Match_DotNetAdapter.instance.Send(msg);
    }

    public void OnlineUpdate(float deltaTime)
    {
        gmj.transform.position += direction * deltaTime;
    }

    public void Hit(PlayerController playerHit)
    {
        GameObject.Destroy(gmj);
        isDead = true;
        InGameWrapper.instance.spellsWrapper.DestroySpell(this,false);
    }

    public int GetGuid()
    {
        return spell.spellGmjID;
    }
}