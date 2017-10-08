using System;
using UnityEngine;

[Serializable]
public class UnityPlayerData : MonoBehaviour
{
    public float StaticYPos = 0.5f;
    public GameObject playerPrefab;
    public float MoveSpeed = 1f;
    public LayerMask groundMask;
}