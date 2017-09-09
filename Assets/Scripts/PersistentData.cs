using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int port;
    public string ip;
}