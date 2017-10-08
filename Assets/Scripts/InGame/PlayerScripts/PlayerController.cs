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

    public PlayerController(GameObject playerGmj, UnityPlayerData generalPlayerData)
    {
        this.playerGmj = playerGmj;
        this.generalPlayerData = generalPlayerData;
        Debug.Log("Final pos not set implemented");
    }

    internal void SetTargetPos(Vector3 vector3)
    {
        targetPos = vector3;
    }

    internal void SetCurrentPos(Vector3 vector3)
    {
        playerGmj.transform.position = vector3;
    }

    public void Update(float deltaTime)
    {
        var pos = playerGmj.transform.position;
        Vector3 direction = targetPos - playerGmj.transform.position;
        direction.Normalize();
        var proposedPosition = direction * Time.deltaTime;
        var finalPos = proposedPosition;
        //Vector3 finalPos = InGameWrapper.instance.mapWrapper.ChangeVectorToBeInbounds(proposedPosition);

        playerGmj.transform.position = finalPos;
    }
}