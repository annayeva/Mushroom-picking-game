using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {

    public float timeLeft = 15;
    public Text timerText;
    public static int introSceneNumber;
    public string activeSceneName;

    // Use this for initialization
    void Start () {
        SceneNumbers.Scenes++;
        introSceneNumber = SceneNumbers.Scenes;
        timerText.text = "Game start: " + Mathf.RoundToInt(timeLeft) + " sec";
        activeSceneName = SceneManager.GetActiveScene().name;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape")) // quit the game on escape press
        {
            Application.Quit();
        }
        timeLeft -= Time.deltaTime;
        timerText.text = "Game start: " + Mathf.RoundToInt(timeLeft) + " sec";
        if (timeLeft <= 0)
        {
            ManagingScenes();
        }
    }

    public void ManagingScenes()
    {
        if (activeSceneName == "IntroScene 1")
        {
            SceneManager.LoadScene("MainScene 1");
        }
        else if (activeSceneName == "IntroScene 2")
        {
            SceneManager.LoadScene("MainScene 2");
        }
        else if (activeSceneName == "IntroScene 3")
        {
            SceneManager.LoadScene("MainScene 3");
        }
        else if (activeSceneName == "IntroScene 4")
        {
            SceneManager.LoadScene("MainScene 4");
        }
        else if (activeSceneName == "IntroScene 5")
        {
            SceneManager.LoadScene("MainScene 5");
        }
    }
}
