
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapWrapper
{
    private UnityMapData mapData;
    List<GameObject> tiles;
    int currentTileNr;
    private float mapTotalDuration;
    private float tileEvaluationTime;

    //Optimization, saved field
    private RaycastHit hit;
    private int countOfAllPlayers = -1;
    private float mapspawnLength = 2f;
    private double timeRoundStarted;
    private float mapDamage = 10f;
    private bool secondTick;
    private int lastTick;

    public MapWrapper(UnityMapData mapData, PlayerController playerController)
    {
        this.mapData = mapData;
        hit = new RaycastHit();
        tiles = new List<GameObject>();
        timeRoundStarted = InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds();
        foreach (Transform item in mapData.mapGmj.transform)
        {
            tiles.Add(item.gameObject);
        }
        ResetValues();
        MarkNextTile();
    }

    internal void Setup(int count)
    {
        countOfAllPlayers = count;
    }

    internal void Reset()
    {
        timeRoundStarted = InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds();
        foreach (GameObject item in tiles)
        {
            item.SetActive(true);
            //item.GetComponent<Renderer>().material = mapData.materialForNormalTile;
        }
        ResetValues();
        MarkNextTile();
    }

    internal Vector3 GetPositionForPlayer(PlayerController player)
    {
        return GetPositionForPlayer(player.GetID(), mapspawnLength);
    }

    internal void DamageTick(PlayerHealthController healthController)
    {
        if (secondTick)
        {
            healthController.Damage(mapDamage, null);
        }
    }

    internal bool HasBeenSetup()
    {
        return countOfAllPlayers != -1;
    }

    Vector3 GetPositionForPlayer(float playerNr,float length)
    {
        float fullAngle = 360;
        float angle = fullAngle * (playerNr / countOfAllPlayers);
        var v = DegreeToVector2(angle) * length;
        return new Vector3(v.x, StaticVariables.GetYPos(), v.y);
    }

    private static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    private static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    private void ResetValues()
    {
        currentTileNr = 0;
        mapTotalDuration = 60f;
        tileEvaluationTime = mapTotalDuration / tiles.Count;
    }

    private void MarkNextTile()
    {
        //tiles[currentTileNr].GetComponent<Renderer>().material = mapData.materialForMarkedTile;
    }

    internal void Update(float deltaTime)
    {
        var currentMapTime = (InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds() - timeRoundStarted)/1000;
        if(currentMapTime > ((currentTileNr+1) * tileEvaluationTime) && currentTileNr < tiles.Count)
        {
            tiles[currentTileNr].SetActive(false);
            currentTileNr++;
            MarkNextTile();
        }

        secondTick = ((int)Time.time) > lastTick;
        lastTick = (int)Time.time;
    }
    
    /// <summary>
    /// Checks wether or not a player is on the map, thus safe
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    public bool IsPlayerSafe(Vector3 playerPosition)
    {
        Physics.Raycast(playerPosition, Vector3.down, out hit, 10f, mapData.safeGroundMask);
        if (hit.collider != null)
            return true;
        else
            return false;
    }
}