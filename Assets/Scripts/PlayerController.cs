using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

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
    private bool isAllowMovement = true;
    [SerializeField]
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
        m_MouseSensativity = 400;
        CrossPlatformInputManager.SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod.Hardware);
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
        if (isGameActive && isAllowMovement)
        {
            updateCameraAngleByMouse();
            updatePlayerLocation();
            shootHandle();
        }
	}

    private void updateCameraAngleByMouse()
    {
        //m_xRotation += Input.GetAxis("Mouse X") * m_MouseSensativity * Time.deltaTime % 360;
        //m_yRotation = Mathf.Clamp(m_yRotation - Input.GetAxis("Mouse Y") * (m_MouseSensativity / 2) * Time.deltaTime, -90, 50);
        //m_Camera.transform.rotation = Quaternion.Euler(m_yRotation, m_xRotation, 0);
    }

    private void updatePlayerLocation()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? m_RunSpeed : m_WalkSpeed;
        float v = Mathf.Sqrt(transform.GetComponent<Rigidbody>().velocity.x * transform.GetComponent<Rigidbody>().velocity.x + transform.GetComponent<Rigidbody>().velocity.y * transform.GetComponent<Rigidbody>().velocity.y + transform.GetComponent<Rigidbody>().velocity.z * transform.GetComponent<Rigidbody>().velocity.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
        if (v > 10f)
            return;


        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vertical) > 0.01)
        {
            //move in the direction of the camera
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * vertical;
            //transform.Translate(new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * vertical);
        }
        if (Mathf.Abs(horizontal) > 0.01)
        {
            //strafe sideways
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime * horizontal;
        }


        if (Input.GetKey(KeyCode.W))
        {
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.right.x, 0f, m_Camera.transform.right.z) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.GetComponent<Rigidbody>().velocity += new Vector3(m_Camera.transform.forward.x, 0f, m_Camera.transform.forward.z) * speed * Time.deltaTime * -1;
        }
    }

    private void shootHandle()
    {
        Debug.Log(isGameActive.ToString());
        if (!shootSound.isPlaying && (Input.GetMouseButtonDown(0) || Input.GetAxis("Fire1") == 1))//|| Input.GetAxis("Fire3") == 1 || Input.GetAxis("Cancel") == 1)
        {
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
        Ray ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
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
        Debug.Log("Dead");
        isGameActive = false;
        isAllowMovement = false;
        Debug.Log(isGameActive.ToString());
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
