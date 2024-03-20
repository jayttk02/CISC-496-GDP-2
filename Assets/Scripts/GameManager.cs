using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Stages")]
    public bool gameInProgress;
    public int highestLevelUnlocked;

    [Header("Volume")]
    public AudioMixer masterAM;
    public bool masterVolumeOn;
    public bool musicOn;
    public bool SFXOn;

    void Start()
    {
        SetVolume("Master");
        SetVolume("Music");
    }

    public void ChangeScene(string sceneName)
    {
        switch (sceneName)
        {
            case ("MainMenu"):
                SceneManager.LoadScene(0);
                break;
            case ("Level0"):
                SceneManager.LoadScene(1);
                break;
            case ("Level1"):
                SceneManager.LoadScene(2);
                break;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume(string type, AudioSource _audioSource = null)
    {
        if (masterAM == null)
        {
            masterAM = GameObject.Find("Audio").GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer;
        }

        switch (type)
        {
            case ("Master"):

                if (masterVolumeOn)
                {
                    masterAM.SetFloat("masterVolume", 0f);
                }
                else
                {
                    masterAM.SetFloat("masterVolume", -80f);
                }
                break;
            case ("Music"):
                if (musicOn)
                {
                    masterAM.SetFloat("musicVolume", 0f);
                }
                else
                {
                    masterAM.SetFloat("musicVolume", -80f);
                }
                break;
            case ("SFX"):
                if (SFXOn)
                {
                    masterAM.SetFloat("sfxVolume", 0f);
                }
                else
                {
                    masterAM.SetFloat("sfxVolume", -80f);
                }
                break;
        }
    }

    public void UpdateVolume(string type)
    {
        switch (type)
        {
            case ("Master"):
                masterVolumeOn = !masterVolumeOn;
                break;
            case ("Music"):
                musicOn = !musicOn;
                break;
            case ("SFX"):
                SFXOn = !SFXOn;
                break;
        }
        SetVolume(type);
    }

    public void NewGame()
    {
        gameInProgress = true;
        highestLevelUnlocked = 0;
        ChangeScene("Level0");
    }

    public void Level0Clear()
    {
        highestLevelUnlocked = 1;
        ChangeScene("Level1");
    }

    //public void Level1Clear()
    //{
    //    highestLevelUnlocked = 1;
    //    ChangeScene("Level 1");
    //}
}
