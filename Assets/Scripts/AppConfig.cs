using UnityEngine;
/// <summary>
/// Static values are kept here
/// This will be replaced with a txt at a later point
/// </summary>
public class AppConfig
{
    //Free port https://www.speedguide.net/port.php?port=61497
    public static int Port = 61497;
    public static string Ip = "127.0.0.1";
    public static string InGameSceneName = "InGame";

    public static PersistentData GetPersistentData()
    {
        return GameObject.Find("DontDestroyGameObject").GetComponent<PersistentData>();
    }
}