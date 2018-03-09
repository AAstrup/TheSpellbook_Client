using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {
    public float duration = 1f;
    private Counter counter;

    // Use this for initialization
    void Start () {
        counter = new Counter(duration, new List<Counter.timesOut>() { new Counter.timesOut(DestroyNow) });
    }
	
	// Update is called once per frame
	void Update () {
        counter.Update(Time.deltaTime);
    }

    // Update is called once per frame
    void DestroyNow()
    {
        Destroy(gameObject);
    }
}
