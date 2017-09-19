using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public Text timerText;

	private float m_StartTime;
	private bool m_IsFinnished = false;

	// Use this for initialization
	void Start () {
		m_StartTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (m_IsFinnished)
			return;
		float t = Time.time - m_StartTime;

		string minutes = ((int)t / 60).ToString();
		string secondes = (t % 60).ToString ("f0");

		timerText.text = minutes + ":" + secondes;
	}

	public void Finnished()
	{
		m_IsFinnished = true;
		timerText.color = Color.yellow;
	}
}
