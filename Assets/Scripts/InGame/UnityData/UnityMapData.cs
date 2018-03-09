using System;
using UnityEngine;

[Serializable]
public class UnityMapData : MonoBehaviour
{
    public GameObject mapGmj;
    public LayerMask safeGroundMask;
    public Material materialForMarkedTile;
    public Material materialForNormalTile;
}