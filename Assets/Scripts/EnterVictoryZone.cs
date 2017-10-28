using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterVictoryZone : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		GameObject.Find ("CapsuleMainBody").SendMessage ("NextLevel");
	}
}
