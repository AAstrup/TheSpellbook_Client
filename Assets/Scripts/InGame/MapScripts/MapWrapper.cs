
using System;
using UnityEngine;

public class MapWrapper
{
    private UnityMapData mapData;
    private float mapTime;
    // If the gameobject with a scale of (1,1,1) does not take up (1,1,1) space in unity
    // Then this setting must be set to convert the gameobject scale to match it
    private float gmjModelScale = 5f;

    public MapWrapper(UnityMapData mapData, PlayerController playerController)
    {
        this.mapData = mapData;
        mapTime = 60f;
    }

    internal void Update(float deltaTime)
    {
        var s = mapData.mapGmj.transform.localScale.x;
        s -= deltaTime / mapTime;
        mapData.mapGmj.transform.localScale = new UnityEngine.Vector3(s,1,s);
    }

    /// <summary>
    /// Checks if a Vector3 is within bounds, and changes it to be if needed
    /// </summary>
    /// <param name="proposedPosition">Position to check</param>
    /// <returns></returns>
    //public Vector3 ChangeVectorToBeInbounds(Vector3 proposedPosition)
    //{
    //    float size = mapData.mapGmj.transform.localScale.x * gmjModelScale;


    //    foreach (var item in collection)
    //    {

    //    }


    //}
}