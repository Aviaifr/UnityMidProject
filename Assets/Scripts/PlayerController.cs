using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private float m_YRotation;
    private Vector2 m_Input;
    private CollisionFlags m_CollisionFlags;
    private float m_xRotation = 0;
    private float m_yRotation = 0;
    [SerializeField] float factor = 0.01f;
    private bool isGameActive = true;
    private AudioSource shootSound;
    private AudioSource noAmmoSound;
    private AudioSource DeathSound;
    private AudioSource KilledEnemySound;
    private AudioSource ammoPickedSound;
    [SerializeField]
    private string nextLevelScene = "Level002";
    [SerializeField]
    private string nextSceneText = "Good Job, Moving to the next level!";

    private int Ammunition = 0;
    [SerializeField]
    public Text AmmounitionText;
    [SerializeField]
    public Text NoAmmoText;

	void Start () {
        m_Camera = Camera.main;
        m_MouseSensativity = 400;
        initSounds();
	}

    private void initSounds()
    {
        shootSound = gameObject.AddComponent<AudioSource>();
        shootSound.clip = (AudioClip)Resources.Load("Sounds/shot");
        noAmmoSound = gameObject.AddComponent<AudioSource>();
        noAmmoSound.clip = (AudioClip)Resources.Load("Sounds/noEmmo");
        DeathSound = gameObject.AddComponent<AudioSource>();
        DeathSound.clip = (AudioClip)Resources.Load("Sounds/Death");
        KilledEnemySound = gameObject.AddComponent<AudioSource>();
        KilledEnemySound.clip = (AudioClip)Resources.Load("Sounds/ohyeaSound");
        ammoPickedSound = gameObject.AddComponent<AudioSource>();
        ammoPickedSound.clip = (AudioClip)Resources.Load("Sounds/pickAmmo");
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
            if (Ammunition > 0)
            {
                takeAShot();
            }
            else
            {
                noAmmoSound.Play();
            }
		}
	}

    private void takeAShot()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody != null && hit.transform.tag.Equals("Enemy"))
            {
                Destroy(hit.transform.gameObject);
                KilledEnemySound.Play();
            }
        }
        shootSound.Play();
        Ammunition--;
        updateAmmo();
    }

    public void GameOver()
    {
        DeathSound.Play();
        isGameActive = false;
        this.SendMessage("StopTime");
    }

    public void CollectAmmo()
    {
        Ammunition += 5;
        ammoPickedSound.Play();
        updateAmmo();
    }

    public void NextLevel()
    {
        this.SendMessage("StopTime");
        NoAmmoText.color = Color.green;
        NoAmmoText.text = nextSceneText;
        StartCoroutine("Wait");
    }
  
  IEnumerator Wait()
  {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextLevelScene);
  }
    private void updateAmmo()
    {
        AmmounitionText.text = Ammunition.ToString();
        NoAmmoText.enabled = Ammunition == 0;
    }
}
