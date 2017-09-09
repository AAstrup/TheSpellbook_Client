using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Static values are kept here
/// This will be replaced with a txt at a later point
/// </summary>
public class AppConfig
{
    //Dynamic port https://www.speedguide.net/port.php?port=61497
    public static int PortOfMatchMaker = 61497;
    public static string IpOfMatchMaker = "127.0.0.1";
    public static string InGameSceneName = "MatchScene";

    public static PersistentData GetPersistentData()
    {
        return GameObject.Find("DontDestroyGameObject").GetComponent<PersistentData>();
    }
    public static string GetName()
    {
        return GameObject.Find("ClientNameInputField").GetComponent<InputField>().name;
    }
}