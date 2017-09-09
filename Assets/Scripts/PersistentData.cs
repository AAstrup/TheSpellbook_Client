using UnityEngine;

/// <summary>
/// Data that keps being stored in unity
/// </summary>
public class PersistentData : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int port;
    public string ip;
    public Shared_PlayerInfo PlayerInfo;
}