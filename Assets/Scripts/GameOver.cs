using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* 
    1. Make screen appear at death.
    2. Record time.
    3. Space to play again.
*/

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text scoreText;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<playerMove>().OnPlayerDeath += OnGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver() {
        gameOverScreen.SetActive(true);
        scoreText.text =  Mathf.Round(Time.timeSinceLevelLoad).ToString();
        gameOver = true;
    }
}
