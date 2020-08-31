using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public static float graterTime;
    public string dir;
    
    private TextMeshProUGUI score;
    private Vector3 startScale;
    private Vector3 endScale;
    private bool reverseFlag;

    void Start()
    {
        score = GameObject.Find(dir).GetComponent<TextMeshProUGUI>();
        Debug.Log(score);
        score.text = "longest flight: " + graterTime.ToString("0.00") + " seconds";

        startScale = score.transform.localScale;
        endScale = startScale * 1.5f;
        reverseFlag = false;

    }

    void Update()
    {
        animateScale(score.transform.localScale);
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void animateScale(Vector3 newScale)
    {
        if (!reverseFlag)
        {
            newScale = score.transform.localScale;
            newScale *= 1.005f;
            score.transform.localScale = newScale;
            if (newScale.x > endScale.x)
                reverseFlag = true;
        }
        else
        {
            newScale = score.transform.localScale;
            newScale *= 0.995f;
            score.transform.localScale = newScale;
            if (newScale.x < startScale.x)
                reverseFlag = false;
        }
    }
}
