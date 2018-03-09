using System;

/// <summary>
/// Contains all data from unity that the InGameWrapper will need to run
/// </summary>
[Serializable]
public class UnityData
{
    public UnityPlayerData playerData;
    public UnityCarData unityCarData;
    public UnityMapData mapData;
    public UnitySpellData spellData;
    public UnityCursorData cursorData;
    public UnityCurrencyData currencyWrapper;
    public UnityAimData aimData;
    public UnityDeviceInputData unityDeviceInputData;
}