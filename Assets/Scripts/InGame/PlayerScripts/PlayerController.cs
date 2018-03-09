using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

/// <summary>
/// Controls a single player
/// </summary>
public class PlayerController
{
    public PlayerAnimatorController playerAnimator;
    public SpellCasterController spellCaster;
    public PlayerHealthController healthController;
    public PlayerScoreController playerScoreController;
    private GameObject playerGmj;
    private UnityPlayerData generalPlayerData;
    private UnityPlayerData playerData;
    private Vector3 targetPos;
    private IDeviceInput deviceInput;
    private Vector3 pushBack = Vector3.zero;

    public int PlayerControllerGUID;
    int OwnerID;
    private bool alive;
    private float minimumMovementDistance = .05f;
    public float health = 100f;

    float lastMovementUpdateTime = 0f;
    static readonly float timeBetweenMovementUpdates = 1f / 30f;

    public PlayerController(GameObject playerGmj, Message_ServerCommand_CreateGameObject info, UnityPlayerData generalPlayerData, IDeviceInput deviceInput)
    {
        alive = true;
        playerScoreController = new PlayerScoreController();
        spellCaster = new SpellCasterController(this);
        playerAnimator = new PlayerAnimatorController(playerGmj);
        healthController = new PlayerHealthController(this, generalPlayerData);
        this.playerGmj = playerGmj;
        this.generalPlayerData = generalPlayerData;
        PlayerControllerGUID = info.GmjGUID;
        OwnerID = info.OwnerGUID;
        playerGmj.transform.position = new Vector3(info.transform.xPos, StaticVariables.GetYPos(), info.transform.zPos);
        targetPos = playerGmj.transform.position;
        this.deviceInput = deviceInput;
    }

    public void Die()
    {
        alive = false;
        playerGmj.SetActive(false);
    }

    internal void Revive()
    {
        alive = true;
        playerGmj.SetActive(true);
    }

    internal bool IsAlive()
    {
        return alive;
    }

    internal int GetOwnerID()
    {
        return OwnerID;
    }

    internal GameObject GetGmj()
    {
        return playerGmj;
    }

    internal void SetTargetPos(Vector3 vector3)
    {
        playerGmj.transform.LookAt(vector3);
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
        CheckMovementInput();
    }

    internal void ApplyPushBack(Vector3 hitVelocity)
    {
        pushBack += hitVelocity;
    }

    internal string DEBUGGetPushback()
    {
        return pushBack.ToString();
    }

    /// <summary>
    /// Checks input for a change in target position
    /// </summary>
    private void CheckMovementInput()
    {
        if (spellCaster.IsCasting() || InGameWrapper.instance.aimWrapper.IsAiming())
        {
            return;
        }
        if (deviceInput.MouseButtonHeldDown())
        {
            if (EventSystemReference.instance.EventSystem.IsPointerOverGameObject(deviceInput.GetMousePointerId()))
                return;
            RaycastHit hit = new RaycastHit();
            Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out hit, 100f, generalPlayerData.groundMask))
            {
                Debug.DrawLine(playerGmj.transform.position, hit.point);
                var newTargetPos = new Vector3(hit.point.x, playerGmj.transform.position.y, hit.point.z);
                if (newTargetPos == targetPos)
                    return;
                InGameWrapper.instance.cursorWrapper.SetPosition(newTargetPos);
                SetTargetPos(new Vector3(hit.point.x, StaticVariables.GetYPos(), hit.point.z));
            }
        }
    }

    public void Update(float deltaTime)
    {
        if (spellCaster.IsCasting())
        {
            return;
        }
        var pos = playerGmj.transform.position;
        Vector3 movementDirection = targetPos - playerGmj.transform.position;
        if (movementDirection.magnitude < minimumMovementDistance)
        {
            playerAnimator.Idle();
            movementDirection = Vector3.zero;
        }
        else
        {
            playerAnimator.Walk();
        }
        movementDirection.Normalize(); //THIS IS WHAT I CHANGED IF YOU GOT ERROR
        var proposedPosition = pos + pushBack * deltaTime + movementDirection * deltaTime;
        var finalPos = proposedPosition;
        playerGmj.transform.position = finalPos;
        pushBack = pushBack * (1 - deltaTime);
    }

    /// <summary>
    /// Only called on the owners machine
    /// </summary>
    /// <param name="deltaTime"></param>
    public void LocalUpdate(float deltaTime)
    {
        CheckInput();
        healthController.CheckInsideMap();
        Update(deltaTime);
        if (NeedToSendNewMovementUpdate())
            SendMovementUpdate();
    }

    /// <summary>
    /// Checks if the client should send a new movement update to the server
    /// </summary>
    /// <returns></returns>
    private bool NeedToSendNewMovementUpdate()
    {
        return Time.realtimeSinceStartup > (lastMovementUpdateTime + timeBetweenMovementUpdates);
    }

    private void SendMovementUpdate()
    {
        lastMovementUpdateTime = Time.realtimeSinceStartup;
        var msg = new Message_ClientRequest_PlayerMovementUpdate()
        {
            currentXPos = playerGmj.transform.position.x,
            currentZPos = playerGmj.transform.position.z,
            GMJGUID = PlayerControllerGUID,
            moveTargetXPos = playerGmj.transform.position.x, //No client prediction implemented yet as this is undergoing a change from RPC to frequency update
            moveTargetZPos = playerGmj.transform.position.y, //Same as above
            TimeStartedMoving = InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds()
        };

        Match_DotNetAdapter.instance.Send(msg);
    }

    internal void Reset()
    {
        pushBack = Vector3.zero;
        healthController.Reset();
    }
}