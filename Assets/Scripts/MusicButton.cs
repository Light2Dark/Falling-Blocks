using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    int currentTrack;
    Button musicButton;
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        musicButton = gameObject.GetComponent<Button>();
        currentTrack = PlayerPrefs.GetInt("trackNum", 0);

        musicButton.onClick.AddListener(() => {
            currentTrack++;
            if (currentTrack > 2) {currentTrack = 0;}

            PlayerPrefs.SetInt("trackNum", currentTrack);
            Music musicPlayer = FindObjectOfType<Music>();
            musicPlayer.PlayNxtSong();
        });
    }


    
}
