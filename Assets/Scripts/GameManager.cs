using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject player;
    public GameObject playerPrefab;

    [Header("Stages")]
    public bool gameInProgress;
    public int highestLevelUnlocked;

    [Space(10)]

    public int currentCheckpoint;
    public bool startingInLevel;

    [Header("Volume")]
    public AudioMixer masterAM;
    public bool masterVolumeOn;
    public bool musicOn;
    public bool SFXOn;

    void Start()
    {
        SetVolume("Master");
        SetVolume("Music");

        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            LoadLevelObjects();
        }
    }

    void OnLevelWasLoaded()
    {
        if (!startingInLevel && SceneManager.GetActiveScene().name != "MainMenu")
        {
            LoadLevelObjects();
        }
    }

    void LoadLevelObjects()
    {
        GameObject playerGO = Instantiate(playerPrefab, GameObject.Find("Checkpoints").transform.GetChild(currentCheckpoint).position, Quaternion.identity);    // spawn prefab
        GameObject.Find("Main Camera").GetComponent<CameraFollow>().player = playerGO.transform;    // add new player gameobject to CameraFollow script in Main Camera

        PlayerMovement playerMovementScript = playerGO.GetComponent<PlayerMovement>();      // PlayerMovement script sound effects
        playerMovementScript.se_walk = GameObject.Find("aud_walk").GetComponent<AudioSource>();
        playerMovementScript.se_jump = GameObject.Find("aud_jump").GetComponent<AudioSource>();
        playerMovementScript.se_shoot = GameObject.Find("aud_bullet").GetComponent<AudioSource>();

        HealthSystem healthSystemScript = playerGO.GetComponent<HealthSystem>();    // HealthSystem script sound effects
        healthSystemScript.se_hit = GameObject.Find("aud_hit").GetComponent<AudioSource>();
        healthSystemScript.se_heart = GameObject.Find("aud_heart").GetComponent<AudioSource>();
        Transform healthPointUITransform = GameObject.Find("Health").transform;                     // get Health UI gameobject
        healthSystemScript.healthPointAnimators = new Animator[healthPointUITransform.childCount];  // set HP animators in HealthSystem script
        for (int i = 0; i < healthPointUITransform.childCount; i++)
        {
            healthSystemScript.healthPointAnimators[i] = healthPointUITransform.GetChild(i).GetComponent<Animator>();
        }

        PlayerInputsUI playerInputsUIScript = playerGO.GetComponent<PlayerInputsUI>();      // playerInputsUIScript script sound effects
        playerInputsUIScript.uiTransform = GameObject.Find("PlayerInputs").transform;
        playerInputsUIScript.uiAnimator = GameObject.Find("PlayerInputs").GetComponent<Animator>();

        Transform checkpointsTransform = GameObject.Find("Checkpoints").transform;
        for (int i = 0; i < checkpointsTransform.childCount; i++)
        {
            checkpointsTransform.GetChild(i).GetComponent<Checkpoint>().Setup(this, i);
        }

        // OTHER //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        GameObject.Find("Main Camera").GetComponent<PauseMenu>().SetGM(this);
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

        currentCheckpoint = 0;

        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (!startingInLevel)
        {
            DontDestroyOnLoad(this.gameObject);
        }
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

    public void UpdateCurrentCheckpoint(int index)
    {
        if (index > currentCheckpoint)
        {
            currentCheckpoint = index;
        }
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
