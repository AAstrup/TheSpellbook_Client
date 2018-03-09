using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysStartActivateGameobject : MonoBehaviour {

    public List<GameObject> gmjs;
	void Awake () {
        foreach (var item in gmjs)
        {
            if(!item.activeSelf)
                item.SetActive(true);
        }
	}
}
