using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasah : MonoBehaviour {
	[SerializeField] float speed = 0;
	[SerializeField] bool testBtn = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.up , speed);

		if (testBtn) 
		{
			if (Input.GetKeyDown (KeyCode.Escape))
				Debug.Log ("E");

			if (Input.GetMouseButtonDown(0))
				Debug.Log ("E55");

			if (Input.GetMouseButtonDown(1))
				Debug.Log ("E1");

			if (Input.GetMouseButtonDown(2))
				Debug.Log ("E2");
			if (Input.anyKeyDown) {
				Debug.Log ("input");
				Debug.Log("-1 to 1 V: "+Input.GetAxis("Vertical"));
				Debug.Log("-1 to 1 H: "+Input.GetAxis("Horizontal") );
			}
		}
	}
}
