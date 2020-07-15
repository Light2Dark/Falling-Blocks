using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    AudioSource music;
    public AudioClip[] tracks = new AudioClip[3];
    int currentTrack = 0;
    int startTrack;

    // Start is called before the first frame update
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();

        currentTrack = PlayerPrefs.GetInt("trackNum", 0);
        music.clip = tracks[currentTrack];
        music.Play();
    }

    public void PlayNxtSong() {
        currentTrack++;
        if (currentTrack > tracks.Length - 1) {currentTrack = 0;}

        music.clip =  tracks[currentTrack];
        music.Play();
        PlayerPrefs.SetInt("trackNum", currentTrack);
    }
}
