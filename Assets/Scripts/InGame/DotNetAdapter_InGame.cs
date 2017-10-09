using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adapter to classes that does not inherit MonoBehavior
/// Used in Scene InGame
/// </summary>
public class DotNetAdapter_InGame : MonoBehaviour {

    InGameWrapper inGameWrapper;

    void Start () {
        UnityData unityData = GetUnityData();
        inGameWrapper = new InGameWrapper(unityData);
    }

    void Update()
    {
        inGameWrapper.Update(Time.deltaTime);
    }

    /// <summary>
    /// Setup the data from unity to be injected into the InGame wrapper
    /// </summary>
    /// <returns></returns>
    private UnityData GetUnityData()
    {
        string playerDataGmjName = "UnityData_Prefab_Player";
        string carDataGmjName = "UnityData_Prefab_Car";
        string mapDataGmjName = "UnityData_Prefab_Map";
        string spellDataGmjName = "UnityData_Prefab_Spell";

        return new UnityData()
        {
            playerData = GameObject.Find(playerDataGmjName).GetComponent<UnityPlayerData>(),
            unityCarData = GameObject.Find(carDataGmjName).GetComponent<UnityCarData>(),
            mapData = GameObject.Find(mapDataGmjName).GetComponent<UnityMapData>(),
            spellData = GameObject.Find(spellDataGmjName).GetComponent<UnitySpellData>()
        };
    }
}
