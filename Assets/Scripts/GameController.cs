using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        Debug.Log("GameOver");
    }

    public void respawnPlayer()
    {
        Vector3 spawnPoint = WrappingBox.createSpawnPointInWrappingBox();
        player.transform.position = spawnPoint;
        player.GetComponent<PlayerController>().asCollideRecently = false;
    }
}
