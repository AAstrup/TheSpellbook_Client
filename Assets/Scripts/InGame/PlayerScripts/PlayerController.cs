using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

/// <summary>
/// Controls a single player
/// </summary>
public class PlayerController
{
    private GameObject playerGmj;
    private UnityPlayerData generalPlayerData;
    private UnityPlayerData playerData;
    private Vector3 targetPos;
    private Vector3 pushBack = Vector3.zero;
    int PlayerControllerGUID;
    int OwnerID;

    public PlayerController(GameObject playerGmj, Message_ServerCommand_CreateGameObject info, UnityPlayerData generalPlayerData)
    {
        this.playerGmj = playerGmj;
        this.generalPlayerData = generalPlayerData;
        PlayerControllerGUID = info.GmjGUID;
        OwnerID = info.OwnerGUID;
        playerGmj.transform.position = new Vector3(info.transform.xPos, info.transform.yPos, info.transform.zPos);
    }

    internal GameObject GetGmj()
    {
        return playerGmj;
    }

    internal void SetTargetPos(Vector3 vector3)
    {
        targetPos = vector3;
    }

    internal int GetID()
    {
        return PlayerControllerGUID;
    }

    internal void SetCurrentPos(Vector3 vector3)
    {
        playerGmj.transform.position = vector3;
    }

    internal void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit = new RaycastHit();
            Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit, 100f, generalPlayerData.groundMask))
            {
                Vector3 direction = hit.point - playerGmj.transform.position;
                direction.Normalize();
                Debug.DrawLine(hit.point, playerGmj.transform.position);
                var msg = new Message_ClientRequest_CreateSpell()
                {
                    ownerGUID = OwnerID,
                    spellType = SpellType.Fireball,
                    xPos = playerGmj.transform.position.x,
                    zPos = playerGmj.transform.position.z,
                    xDir = direction.x,
                    zDir = direction.z
                };
                Match_DotNetAdapter.instance.Send(msg);
            }
        }

        CheckMovement();
    }

    internal void ApplyPushBack(Vector3 hitDirection)
    {
        pushBack += hitDirection;
    }

    /// <summary>
    /// Checks input for a change in target position
    /// </summary>
    private void CheckMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit, 100f, generalPlayerData.groundMask))
            {
                Debug.DrawLine(playerGmj.transform.position, hit.point);
                targetPos = new Vector3(hit.point.x, playerGmj.transform.position.y, hit.point.z);
                var msg = new Message_Command_PlayerMovementUpdate()
                {
                    currentXPos = playerGmj.transform.position.x,
                    currentZPos = playerGmj.transform.position.z,
                    GMJGUID = PlayerControllerGUID,
                    moveTargetXPos = targetPos.x,
                    moveTargetZPos = targetPos.z
                };
                Match_DotNetAdapter.instance.Send(msg);
            }
        }
    }

    public void Update(float deltaTime)
    {
        var pos = playerGmj.transform.position;
        Vector3 direction = targetPos - playerGmj.transform.position;
        direction.Normalize();
        var proposedPosition = pos + pushBack + direction * Time.deltaTime;
        var finalPos = proposedPosition;
        playerGmj.transform.position = finalPos;
        pushBack = pushBack * (1 - deltaTime);
    }
}