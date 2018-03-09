using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsData 
{
    public UISettingData[] settings;
    public UISettingData GetUISettingsData(UISettingDataType type)
    {
        foreach (var item in settings)
        {
            if (item.type == type)
                return item;
        }
        throw new Exception("No such UISettingDataType of type " + type.ToString() + " was found in unity_data");
    }
}