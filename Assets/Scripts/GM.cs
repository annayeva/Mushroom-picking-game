using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

    public static GM instance = null;
    public static int sceneNumber;
    public string activeSceneName;
    public float score = 0;
    public float totalGood;
    public int errors = 0;
    private float pickedPercentage;
    public float timeLeft = 60;
    public Text scoreText;
    public Text finalScoreText;
    public Text erorText;
    public Text timerText;
    public GameObject TimeoutScreen;
    public GameObject BadPickScreen;
    public GameObject IngameScreen;
    public GameObject WinningScreen;
    public GameObject ScoreAndRestartScreen;
    public bool gameIsOver;
    public bool timeIsOut;
    public bool allIsPicked;
    private AudioSource audSource;
    public AudioClip pickupSound;
    public AudioClip errorSound;
    public AudioClip winSound;
    public GameObject Player;
    private FirstPersonController Controller;
    public List<float> kindsToPick = new List<float>();
    //Add game state buleans

    // Use this for initialization
    void Start() {
        GMsetup();
        audSource = GetComponent<AudioSource>();
        sceneNumber = SceneNumbers.Scenes;
        activeSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(SceneNumbers.Scenes);

        for (float i = 0; i < totalGood; i++)
        {
            kindsToPick.Add(i);
        }

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
        Debug.Log("CURSOR WAS LOCKED");
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Mushrooms: " + score + " of " + totalGood; //update score text
        CheckEscPress();
        CheckTimer();
        CheckIfAllPicked();
    }

    public void VisualKindCheck(float visualKind)
    {
        //foreach (float kind in kindsToPick)
        foreach (float kind in kindsToPick.ToArray())
        {
            //Debug.Log("checked kind: " + visualKind + "kind from array: "+ kind);
            if (visualKind == kind) {
                Debug.Log("MATCH");
                kindsToPick.Remove(kind);
                AddPoint();
                Debug.Log("MushroomImage" + kind);
                GameObject mushroomImage = GameObject.Find("MushroomCover"+kind);
                mushroomImage.SetActive(false);
                //print(mushroomImage.activeSelf ? "Active" : "Inactive");
            }
        }

        foreach (float kind in kindsToPick)
        {
            Debug.Log("kind: " + kind);
        }

    }

    public void CheckEscPress()
    {
        if (Input.GetKey("escape")) // quit the game on escape press
        {
            Application.Quit();
        }
    }

    public void CheckTimer()
    {
        timeLeft -= Time.deltaTime; //update timer
        if ((timeLeft < 0) && !allIsPicked) // "time is out" condition
        {
            OnTimeOut();
        }
        else if ((timeLeft >= 0) && !allIsPicked) // if time is not out and game is not over
        {
            timerText.text = "Time left: " + Mathf.RoundToInt(timeLeft) + " sec"; //show how much time left
        }
    }

    public void CheckIfAllPicked()
    {
        if (score == totalGood && !allIsPicked)
        {
            allIsPicked = true;
            OnGameWin();
        }
    }

    public void AddPoint() // adding a point to picked mushroom score
    {
        audSource.PlayOneShot(pickupSound); //play pick up sound
        if (!timeIsOut) // increment the points only if time is not over
        {
            score++;
        }
    }

    public void OnBadMushroomPick()
    {
        audSource.PlayOneShot(errorSound); //play error sound
        errors++; //increment error points
        StartCoroutine(WrongMushroomWarning()); //show and hide warning screen
    }

    IEnumerator WrongMushroomWarning()
    {
        BadPickScreen.SetActive(true); //show the warning screen     
        yield return new WaitForSeconds(1); //yield on a new YieldInstruction that waits for some seconds.
        BadPickScreen.SetActive(false); //After we have waited some seconds - hide the warning
    }

    public void DisablePlayerMovement()
    {
        Controller = Player.GetComponent<FirstPersonController>();
        Controller.m_WalkSpeed = (0);
        Controller.m_RunSpeed = (0);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("CURSOR UNLOCKEDD");
    }

    public void OnGameRestart()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor back to the center
        Cursor.visible = false;
        //restarting the scene depending on what is the current scene
        if (activeSceneName == "MainScene 1")
        {
            SceneManager.LoadScene("IntroScene 1");
        }
        else if (activeSceneName == "MainScene 2")
        {
            SceneManager.LoadScene("IntroScene 2");
        }
        else if (activeSceneName == "MainScene 3")
        {
            SceneManager.LoadScene("IntroScene 3");
        }
        else if (activeSceneName == "MainScene 4")
        {
            SceneManager.LoadScene("IntroScene 4");
        }
        else if (activeSceneName == "MainScene 5")
        {
            SceneManager.LoadScene("IntroScene 5");
        }        
    }

    public void OnGameContinue()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor back to the center
        //loading the next scene depending on what is the current scene
        if (activeSceneName == "MainScene 1")
        {
            SceneManager.LoadScene("IntroScene 2");
        }
        else if (activeSceneName == "MainScene 2")
        {
            SceneManager.LoadScene("IntroScene 3");
        }
        else if (activeSceneName == "MainScene 3")
        {
            SceneManager.LoadScene("IntroScene 4");
        }
        else if (activeSceneName == "MainScene 4")
        {
            SceneManager.LoadScene("IntroScene 5");
        }
    }

    public void OnTimeOut()
    {
        timeIsOut = true;
        ShowScore();
        IngameScreen.SetActive(false);
        TimeoutScreen.SetActive(true);
        DisablePlayerMovement();
        
    }

    public void ShowScore()
    {
        if (errors == 1)
        {
            erorText.text = "You made " + errors + " error.";
        }
        else {
            erorText.text = "You made " + errors + " errors.";
        }
        ScoreAndRestartScreen.SetActive(true);
        //finalScoreText.text = "Your score is: " + score + " out of " + totalGood;
        pickedPercentage = score/totalGood * 100;
        finalScoreText.text = "You picked " + Mathf.RoundToInt(pickedPercentage) + "%";
        if (errors > 0) {
            erorText.color = Color.red;
        }


    }

    public void OnGameWin()
    {
        audSource.PlayOneShot(winSound); //play winning sound
        IngameScreen.SetActive(false);
        WinningScreen.SetActive(true);
        ShowScore();
        DisablePlayerMovement();
    }

    public void GMsetup()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

}
