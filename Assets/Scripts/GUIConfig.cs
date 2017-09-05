using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A list of getters to the UI
/// Note: Properties are not allowed in Unity
/// </summary>
public class GUIConfig
{
    public static string GetIp()
    {
        return GameObject.Find("InputField_Ip").GetComponent<InputField>().text;
    }
}