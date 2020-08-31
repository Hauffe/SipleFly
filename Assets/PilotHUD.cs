using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PilotHUD : MonoBehaviour
{

    public string dir;
    public string dirResults;
    public GameObject[] cubesList;
    public float startTime;
    public float finishTime;

    private TextMeshProUGUI cubes;
    private TextMeshProUGUI results;
    private TextMeshProUGUI timeResult;
    private GameObject ResultsHud;
    private bool endFlag;
    private int count;

    void Start()
    {
        startTime = Time.time;
        cubes = GameObject.Find(dir).GetComponent<TextMeshProUGUI>();
        results = GameObject.Find(dirResults+"Results/Text").GetComponent<TextMeshProUGUI>();
        timeResult = GameObject.Find("TimeResult/Text").GetComponent<TextMeshProUGUI>();
        ResultsHud = GameObject.Find(dirResults);
        ResultsHud.SetActive(false);
        endFlag = false;
        updateCubesCount();

    }

    void Update()
    {
        if (!endFlag) { 

            if (count == 0)
            {
                endGame("Success!", finishTime - (Time.time - startTime));
            }
            else if(finishTime - (Time.time - startTime) <= 0)
            {
                endGame("Too late :(", finishTime - (Time.time - startTime));
            }

            timeResult.text = "Time Left: " + (finishTime - (Time.time - startTime)).ToString("0.00");
            updateCubesCount();
        }
    }

    void updateCubesCount()
    {
        cubesList = GameObject.FindGameObjectsWithTag("Cubes");
        count = cubesList.Length;
        cubes.text = "Cubes left: " + count;
    }

    public void playAgain()
    {
        ShowRewardedAd();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void endGame(string message, float time)
    {
        endFlag = true;
        timeResult.text = time.ToString("0.00");
        results.text = message;
        ResultsHud.SetActive(true);
        count = -1;
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady())
        {
            Time.timeScale = 0;
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Time.timeScale = 1;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                Time.timeScale = 1;
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                Time.timeScale = 1;
                break;
        }
    }
}
