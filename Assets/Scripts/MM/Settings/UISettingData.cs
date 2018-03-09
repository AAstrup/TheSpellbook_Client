using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UISettingData
{
    public UISettingDataType type;
    public Button[] buttons;
    public Button exitButton;
}
public enum UISettingDataType { StartScreen, SettingsGraphics, SettingsBuildTest };