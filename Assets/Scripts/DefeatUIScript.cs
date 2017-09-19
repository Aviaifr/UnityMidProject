using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatUIScript : MonoBehaviour {

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void Start()
    {
        menu.SetActive(false);
    }

    public void onClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}
