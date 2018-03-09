using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActivateDeactivateGMJ : MonoBehaviour {

    public GameObject stateGMJ;
    public void ChangeState()
    {
        stateGMJ.SetActive(!stateGMJ.activeSelf);
    }

}
