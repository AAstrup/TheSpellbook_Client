using UnityEngine;

/// <summary>
/// Data that keps being stored in unity
/// </summary>
public class PersistentDataContainer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        persistentData = new PersistentData();
    }
    public PersistentData persistentData;

}