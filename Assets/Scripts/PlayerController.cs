using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private bool m_IsWalking;
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_RunSpeed;
    [SerializeField] private float m_RunningSteps;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_StepInterval;
    [SerializeField]
    private AudioClip[] m_FootstepSounds;
    [SerializeField]
    private AudioClip m_JumpSound;
    [SerializeField]
    private AudioClip m_LandSound;
    [SerializeField]
    private float m_MouseSensativity;

    private Camera m_Camera;
    //private bool m_isInJump;
    private float m_YRotation;
    private Vector2 m_Input;
    //private Vector3 m_MovementDirection = Vector3.zero;
    //private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;
    //private Vector3 m_OriginalCameraPosition;
    //private AudioSource m_AudioSource;
    private float m_xRotation = 0;
    private float m_yRotation = 0;
    [SerializeField] float factor = 0.01f;
    private bool isGameActive = true;

	// Use this for initialization
	void Start () {
        //m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        //m_OriginalCameraPosition = m_Camera.transform.localPosition;
       // m_isInJump = false;
        //m_AudioSource = GetComponent<AudioSource>();
        m_MouseSensativity = 500f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isGameActive)
        {
            updateCameraAngleByMouse();
            updatePlayerLocation();
            shootHandle();
        }
	}

    private void updateCameraAngleByMouse()
    {
        m_xRotation += Input.GetAxis("Mouse X") * m_MouseSensativity * Time.deltaTime % 360;
        m_yRotation = Mathf.Clamp(m_yRotation - Input.GetAxis("Mouse Y") * (m_MouseSensativity / 2) * Time.deltaTime,-90,50);
        Camera.main.transform.rotation = Quaternion.Euler(m_yRotation, m_xRotation, 0);
    }

    private void updatePlayerLocation()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? m_RunSpeed : m_WalkSpeed;
        float v = Mathf.Sqrt(transform.GetComponent<Rigidbody>().velocity.x * transform.GetComponent<Rigidbody>().velocity.x + transform.GetComponent<Rigidbody>().velocity.y * transform.GetComponent<Rigidbody>().velocity.y + transform.GetComponent<Rigidbody>().velocity.z * transform.GetComponent<Rigidbody>().velocity.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (v > 3.5f)
            return;
       
        if (Input.GetKey(KeyCode.W))
        {
            m_Camera.transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * factor;
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Camera.transform.GetComponent<Rigidbody>().velocity -= new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime * factor;
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Camera.transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime * factor;
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Camera.transform.GetComponent<Rigidbody>().velocity -= new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * factor;
        }
    }

	private void shootHandle()
	{
		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hit;
			//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
			if (Physics.Raycast (ray, out hit))
			if (hit.rigidbody != null && hit.transform.tag.Equals("Enemy") )/*&& (hit as GameObject).tag.Equals("Enemy")*/
				//				Debug.Log ("ERR :" +	hit.rigidbody.transform.name);
				Destroy(hit.transform.gameObject);
		}
	}

    public void GameOver()
    {
        isGameActive = false;
    }
}
