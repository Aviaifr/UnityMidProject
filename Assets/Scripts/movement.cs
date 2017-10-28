using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    [SerializeField] private Camera m_Camera;
    private int speed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime);

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vertical) > 0.01 )
        {
            //move in the direction of the camera
            transform.Translate(new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * vertical);
        }
        if (Mathf.Abs(horizontal) > 0.01)
        {
            //strafe sideways
            transform.Translate(new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime * horizontal);
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime*-1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * vertical;
            transform.Translate(new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime );
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * -1);
        }
    }
}
