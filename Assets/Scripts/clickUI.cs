using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickUI : MonoBehaviour {
    [SerializeField] private Camera m_Camera;
    private AudioSource shootSound;

    // Use this for initialization
    void Start () {
        initSounds();
    }
	
	// Update is called once per frame
	void Update () {
      if (Input.GetAxis("Fire1") == 1 || Input.GetMouseButtonDown(0))
        {
            onClick();
        }
    }

    void onClick()
    {
        shootSound.Play();
        RaycastHit2D hit = Physics2D.Raycast(m_Camera.transform.position, m_Camera.transform.forward);
        if (hit)
        {
            hit.transform.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void initSounds()
    {
        shootSound = gameObject.AddComponent<AudioSource>();
        shootSound.clip = (AudioClip)Resources.Load("Sounds/shot");
    }
}
