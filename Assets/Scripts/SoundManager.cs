using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public static SoundManager _instance;

    public bool isMusic;

    public Image musicImage;
    public Sprite musicOn, musicOff;
    public AudioSource musicSource;

    private void Awake() {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("MUSIC") == 0) {
            isMusic = true;
        } else {
            isMusic = false;
        }

        TurnMusic(true);
    }
	
	public void TurnMusic(bool noChange) {
        if (!noChange)
            isMusic = !isMusic;

        if (isMusic) {
            PlayerPrefs.SetInt("MUSIC", 0);
            musicImage.sprite = musicOn;
            musicSource.mute = false;
        } else {
            PlayerPrefs.SetInt("MUSIC", 1);
            musicImage.sprite = musicOff;
            musicSource.mute = true;
        }
    }
}
