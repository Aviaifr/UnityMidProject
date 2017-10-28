using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CapsuleMainBody")
        {
            GameObject.Find("CapsuleMainBody").SendMessage("CollectAmmo");
            Destroy(transform.gameObject);
        }
    }
}
