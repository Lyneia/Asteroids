using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI previousScoreText;
    private string maxScoreKey = "maxScore";
    private string scoreKey = "score";

    // Start is called before the first frame update
    void Start()
    {
        bestScoreText.text = "Best score : " + PlayerPrefs.GetInt(maxScoreKey);
        previousScoreText.text = "Previous score : " + PlayerPrefs.GetInt(scoreKey);
    }

    public void play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
