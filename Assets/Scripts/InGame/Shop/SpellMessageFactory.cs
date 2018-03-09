using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

public class SpellMessageFactory
{
    /// <summary>
    /// Builds a message for a given spell
    /// </summary>
    /// <param name="spell">Definition of the spell</param>
    /// <returns>The message request to be send</returns>
    public object BuildMessage(UnitySpellDefinition spell,Vector3 spawnPos)
    {
        var timeInMiliSeconds = InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds();
        InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().spellCaster.SpellRequestSend();
        switch (spell.spellMovementType)
        {
            case UnitySpellDefinition.SpellMovementType.Direction:
                return BuildProjectileMessage(spell, spawnPos, timeInMiliSeconds);
            case UnitySpellDefinition.SpellMovementType.AOE:
                return BuildAOEMessage(spell, timeInMiliSeconds);
        }
        throw new Exception("No SpellMovementType found for the given spell");
    }

    object BuildProjectileMessage(UnitySpellDefinition spellDefinition, Vector3 spawnPos, double timeInMiliSeconds)
    {
        RaycastHit hit = new RaycastHit();
        Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(Input.mousePosition);
        var player = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer();
        var playerGmj = player.GetGmj();
        Vector3 direction;
        if (Physics.Raycast(mouseRay, out hit, 100f, InGameWrapper.instance.playersWrapper.playerData.groundMask))
        {
            direction = hit.point - playerGmj.transform.position;
        }
        else
        {
            direction = Vector3.right;
            Debug.Log("ERROR");
            Debug.Log("Raycast didnt hit anything for spell!");
        }
        direction = new Vector3(direction.x, 0f, direction.z);
        direction.Normalize();
        Debug.DrawLine(hit.point, playerGmj.transform.position);
        var msg = new Message_ClientRequest_CreateSpellWithDirection()
        {
            ownerGUID = player.GetOwnerID(),
            spellType = spellDefinition.type,
            spellXPos = spawnPos.x,
            spellZPos = spawnPos.z,
            spellXDir = direction.x,
            spellZDir = direction.z,
            playerXPos = playerGmj.transform.position.x,
            playerZPos = playerGmj.transform.position.z,
            rank = spellDefinition.rank,
            TimeStartedCasting = timeInMiliSeconds
        };
        return msg;
    }

    object BuildAOEMessage(UnitySpellDefinition spellDefinition, double timeInMiliSeconds)
    {
        RaycastHit hit = new RaycastHit();
        Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(Input.mousePosition);
        var player = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer();
        var playerGmj = player.GetGmj();
        Vector3 direction;
        if (Physics.Raycast(mouseRay, out hit, 100f, InGameWrapper.instance.playersWrapper.playerData.groundMask))
        {
            direction = hit.point - playerGmj.transform.position;
        }
        else
        {
            hit.point = playerGmj.transform.position;
            Debug.Log("ERROR");
            Debug.Log("Raycast didnt hit anything for spell!");
        }

        var msg = new Message_ClientRequest_CreateSpellInStaticPosition()
        {
            ownerGUID = player.GetOwnerID(),
            spellType = spellDefinition.type,
            spellXPos = hit.point.x,
            spellZPos = hit.point.z,
            playerXPos = playerGmj.transform.position.x,
            playerZPos = playerGmj.transform.position.z,
            rank = spellDefinition.rank,
            TimeStartedCasting = timeInMiliSeconds
        };
        return msg;
    }
}