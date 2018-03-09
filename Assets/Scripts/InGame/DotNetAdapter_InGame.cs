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
    public UISpellButtonWrapper uiShop;

    void Start () {
        UnityData unityData = GetUnityData();
        inGameWrapper = new InGameWrapper(unityData, DeviceInputFactory.Create(unityData.unityDeviceInputData.inputType));
        uiShop.StartShop();
        Match_DotNetAdapter.instance.StartOnlineClient(inGameWrapper.logger);
    }

    void Update()
    {
        inGameWrapper.Update(Time.deltaTime);
    }

    void FixedUpdate()
    {
        inGameWrapper.FixedUpdate(Time.deltaTime);
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
        string cursorDataGmjName = "UnityData_Prefab_Cursor";
        string currencyDataGmjName = "UnityData_Prefab_Currency";
        string aimDataGmjName = "UnityData_Prefab_Aim";
        string inputDataGmjName = "UnityData_Prefab_Device";

        return new UnityData()
        {
            playerData = GameObject.Find(playerDataGmjName).GetComponent<UnityPlayerData>(),
            unityCarData = GameObject.Find(carDataGmjName).GetComponent<UnityCarData>(),
            mapData = GameObject.Find(mapDataGmjName).GetComponent<UnityMapData>(),
            spellData = GameObject.Find(spellDataGmjName).GetComponent<UnitySpellData>(),
            cursorData = GameObject.Find(cursorDataGmjName).GetComponent<UnityCursorData>(),
            currencyWrapper = GameObject.Find(currencyDataGmjName).GetComponent<UnityCurrencyData>(),
            aimData = GameObject.Find(aimDataGmjName).GetComponent<UnityAimData>(),
            unityDeviceInputData = GameObject.Find(inputDataGmjName).GetComponent<UnityDeviceInputData>()
        };
    }
}
