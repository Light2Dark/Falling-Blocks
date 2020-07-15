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
    public GameObject gameOverScreen, repeatPlayTxt, timeRecordText;
    Text repeatPlay;
    public Text scoreText;
    bool gameOver = false;
    private float timeRecord;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<playerMove>().OnPlayerDeath += OnGameOver;

        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP_8_1
        repeatPlay = repeatPlayTxt.GetComponent<Text>();
        repeatPlay.text = "tap with 2 fingers to play again";
        repeatPlay.fontSize = 30;
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 2) {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver() {
        gameOverScreen.SetActive(true);
        AudioSource gameOverSource = this.GetComponent<AudioSource>();
        gameOverSource.Play();
        
        float currentRecord = Mathf.Round(Time.timeSinceLevelLoad);
        scoreText.text =  currentRecord.ToString();

        if (currentRecord > PlayerPrefs.GetInt("timeRecord")) {
            timeRecord = currentRecord;
            timeRecordText.GetComponent<Text>().text = "time record: " + currentRecord + "s";
            PlayerPrefs.SetInt("timeRecord", (int)timeRecord);
        } else {
            timeRecordText.GetComponent<Text>().text = "time record: " + PlayerPrefs.GetInt("timeRecord") + "s";
        }

        gameOver = true;
    }
}
