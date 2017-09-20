using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    [SerializeField]
    private float rotationsPerMinute = 10f;
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 6f * rotationsPerMinute *  Time.deltaTime, 0);
	}
}
