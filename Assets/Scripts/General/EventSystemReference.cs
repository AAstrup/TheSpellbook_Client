using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemReference : MonoBehaviour {
    public static EventSystemReference instance;

    public EventSystem EventSystem;

    // Use this for initialization
    void Awake () {
        instance = this;
	}
}
