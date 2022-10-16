using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public GameObject player;

    public int score = 0;
    private string maxScoreKey = "maxScore";
    private string scoreKey = "score";

    private int scoreForMajor = 200;
    private int scoreForMedium = 100;
    private int scoreForMinor = 50;

    public int maxiumLife = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text=score.ToString();
        string textLives = "";
        if (player != null && player.GetComponent<PlayerController>()!=null)
        {
            for (int i = 0; i < player.GetComponent<PlayerController>().life; i++)
            {
                textLives += "<sprite=0> ";
            }
        }
        for (int i = 0; i < maxiumLife-(player != null ? player.GetComponent<PlayerController>().life : 0); i++)
        {
            textLives += "<sprite=1> ";
        }
        livesText.text = textLives;
    }

    public void gameOver()
    {
        Debug.Log("GameOver");
        if (PlayerPrefs.GetInt(maxScoreKey) < score)
        {
            PlayerPrefs.SetInt(maxScoreKey, score);
        }
        PlayerPrefs.SetInt(scoreKey, score);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void respawnPlayer()
    {
        Vector3 spawnPoint = WrappingBox.createSpawnPointInWrappingBox();
        player.transform.position = spawnPoint;
        player.GetComponent<PlayerController>().asCollideRecently = false;
    }

    public void increaseScore(EnumList.AsteroidsType asteroidType)
    {
        switch (asteroidType)
        {
            case EnumList.AsteroidsType.Major:
                score += scoreForMajor;
                break;
            case EnumList.AsteroidsType.Medium:
                score += scoreForMedium;
                break;
            case EnumList.AsteroidsType.Minor:
                score += scoreForMinor;
                break;
            default:
                break;
        }
    }
}
