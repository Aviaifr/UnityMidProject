using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public Text timerText;
    public Text fpsText;

    private float m_StartTime;
	private bool m_IsFinnished = false;
    private float deltaTime;
    

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

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        fpsText.text = "FPS: " + Mathf.Ceil(1.0f / deltaTime).ToString();
	}

	public void Finnished()
	{
		m_IsFinnished = true;
		timerText.color = Color.yellow;
	}

    public void StopTime()
    {
        Debug.Log("Stopped");
        m_IsFinnished = true;
    }
}
