using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Static values are kept here
/// This will be replaced with a txt at a later point
/// </summary>
public class UnityConfig
{
    //Dynamic port https://www.speedguide.net/port.php?port=61497
    public static string InGameSceneName = "MatchScene";
    public static string OfflineSceneName = "OfflineMatchScene";

    public static PersistentDataContainer GetPersistentDataContainer()
    {
        return GameObject.Find("DontDestroyGameObject").GetComponent<PersistentDataContainer>();
    }
    public static string GetName()
    {
        //return GameObject.Find("ClientNameInputField").GetComponent<InputField>().text;
        return "Unnamed";
    }
    public static Slider GetReadyCheckSlider()
    {
        return GameObject.Find("ReadyCheckSlider").GetComponent<Slider>();
    }
}