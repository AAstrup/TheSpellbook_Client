using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UnityPlayerData : MonoBehaviour
{
    public GameObject playerPrefab;
    public float MoveSpeed = 1f;
    public LayerMask groundMask;
    public Image playerHealthImage;
    public Text playerHealthText;
}