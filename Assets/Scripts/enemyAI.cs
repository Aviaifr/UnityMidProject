using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyAI : MonoBehaviour {
    public Transform player;
    public float playerDistance;
    public float rotationDamping;
    public float moveSpeed;
    public GameObject menu;
    public bool isActiveGame = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isActiveGame)
        {
            playerDistance = Vector3.Distance(player.position, transform.position);

            if (playerDistance < 15f)
            {
                lookAtPlayer();
            }

            if (playerDistance < 12f)
            {
                chase();
            }
        }
	}

    private void chase()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private void lookAtPlayer()
    {
        Vector3 v = new Vector3(transform.position.x, player.position.y , transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(player.position - v);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    private void OnTriggerEnter(Collider other)
    {
        menu.SetActive(true);
        GameObject.Find("FirstPersonCamera").SendMessage("GameOver");
        isActiveGame = false;
    }
}
