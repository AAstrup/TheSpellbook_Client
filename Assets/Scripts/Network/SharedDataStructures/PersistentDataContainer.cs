using System;
using UnityEngine;

/// <summary>
/// Data that keeps being stored in unity
/// </summary>
public class PersistentDataContainer : MonoBehaviour
{
    public PersistentData persistentData;
    public DBProfile_Login profile;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        persistentData = new PersistentData();
    }

    public void SetProfile(Message_ServerResponse_Login objData)
    {
        profile = objData.profile;
    }

    public void SetProfile(Message_ServerResponse_Register objData)
    {
        profile = objData.profile;
    }
}