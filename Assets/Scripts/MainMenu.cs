﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class MainMenu : MonoBehaviour
{
    GameManager gm;
    public Transform canvas;

    private GameObject playerText;
    TextMeshProUGUI playerCountText;
    Animator playerCountAnimator;

    GameObject mainMenuGO;
    Button newGameButton;
    GameObject continueButtonGO;

    GameObject stageSelectMenuGO;
    Button[] stageSelectMenuButtons;

    GameObject settingsMenuGO;
    public TextMeshProUGUI[] settingsMenuButtonTexts;

    private Transform ipConnect;
    private GameObject connectionSuccessful;
    private GameObject connectionFailed;
    
    private Button mobileControlsButton;
    private TextMeshProUGUI mobileControlsSettingText;

    Animator fadeOutAnimator;

    [Space(10)]
    public int numberOfPlayers;
    public int minNumberOfPlayers;

    private bool ready;
    private bool connected;
    private bool attemptedConnection;
    private WebSocket wss;
    private IDictionary<string, bool> socketMap = new Dictionary<string, bool>();
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Time.timeScale = 1;         // when loading from level pause screen

        playerText = canvas.GetChild(2).gameObject;
        playerCountText = canvas.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        playerCountAnimator = canvas.GetChild(2).GetComponent<Animator>();

        mainMenuGO = canvas.GetChild(3).gameObject;
        newGameButton = mainMenuGO.transform.GetChild(1).GetChild(0).GetComponent<Button>();
        continueButtonGO = mainMenuGO.transform.GetChild(1).GetChild(1).gameObject;
        continueButtonGO.SetActive(gm.gameInProgress);

        stageSelectMenuGO = canvas.GetChild(4).gameObject;

        settingsMenuGO = canvas.GetChild(5).gameObject;

        fadeOutAnimator = canvas.GetChild(6).gameObject.GetComponent<Animator>();
        
        ipConnect = canvas.GetChild(3).GetChild(3);
        connectionSuccessful = ipConnect.GetChild(3).gameObject;
        connectionFailed = ipConnect.GetChild(4).gameObject;

        mobileControlsButton = canvas.GetChild(3).GetChild(4).GetChild(1).GetComponent<Button>();
        mobileControlsSettingText = canvas.GetChild(3).GetChild(4).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        MainMenuOpen(true);
        StageSelectOpen(false);
        SettingsOpen(false);
    }

    void SetConnection(bool connected)
    {
        connectionSuccessful.SetActive(connected);
        connectionFailed.SetActive(!connected);
    }

    IEnumerator CheckForPlayers()
    {
        try
        {
            wss.Send("want # players");
        }
        catch
        {
            connected = false;
            ready = false;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine(CheckForPlayers());
    }

    void ConnectToWebSocket()
    {
        attemptedConnection = true;
        string ip = ipConnect.GetChild(1).GetComponent<TMP_InputField>().text;
        gm.IP = ip;
        try
        {
            wss = new WebSocket("wss://" + ip + ":8443");
            connected = true;
            wss.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            wss.Connect();
            wss.OnMessage += (sender, e) =>
            {
                if (e.Data.Substring(0, 9) == "players: ")
                {
                    numberOfPlayers = int.Parse(e.Data.Substring(9), System.Globalization.CultureInfo.InvariantCulture);
                }
            };
            StartCoroutine(CheckForPlayers());
        }
        catch (Exception e)
        {
            connected = false;
        }
    }

    void Update()
    {
        if (attemptedConnection)
        {
            SetConnection(connected);
        }
        ipConnect.gameObject.SetActive(gm.mobileControls);
        ready = (numberOfPlayers >= minNumberOfPlayers && connected) || !gm.mobileControls;
        playerCountText.text = numberOfPlayers.ToString() + " / " + minNumberOfPlayers.ToString();
        playerCountAnimator.SetBool("greenEffect", ready);
        newGameButton.interactable = ready;
        playerText.SetActive(connected && gm.mobileControls); 
        
    }
    
    void MainMenuOpen(bool on = true)
    {
        mainMenuGO.SetActive(on);
    }

    public void MainMenuButton(int index)
    {
        switch (index)
        {
            case (0):
                StartCoroutine(NewGameButton());
                break;
            case (1):
                StageSelectOpen();
                break;
            case (2):
                SettingsOpen();
                break;
            case (3):
                Application.Quit();
                break;
            case (4):
                ConnectToWebSocket();
                break;
            case (5):
                if (mobileControlsSettingText.text == "x")
                {
                    gm.mobileControls = false;
                    mobileControlsSettingText.text = "";
                }
                else
                {
                    gm.mobileControls = true;
                    mobileControlsSettingText.text = "x";
                }
                break;
        }
    }

    IEnumerator NewGameButton()
    {
        fadeOutAnimator.SetTrigger("fadeOut");

        yield return new WaitForSecondsRealtime(0.55f);

        gm.NewGame();
    }

    public void StageSelectOpen(bool on = true)
    {
        stageSelectMenuGO.SetActive(on);

        if (on)
        {
            MainMenuOpen(false);

            if (stageSelectMenuButtons == null)
            {
                stageSelectMenuButtons = new Button[stageSelectMenuGO.transform.GetChild(2).childCount];
                for (int i = 0; i < stageSelectMenuButtons.Length; i++)
                {
                    stageSelectMenuButtons[i] = stageSelectMenuGO.transform.GetChild(2).GetChild(i).GetComponent<Button>();
                }
            }

            for (int i = 0; i < stageSelectMenuButtons.Length; i++)
            {
                stageSelectMenuButtons[i].interactable = i <= gm.highestLevelUnlocked;

                if (i > gm.highestLevelUnlocked)
                {
                    stageSelectMenuButtons[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = "???";
                }
            }
        }
        else
        {
            MainMenuOpen(true);
        }
    }

    public void StageSelectButton(int index)
    {
        StartCoroutine(StageSelectButtonSelected(index));
    }

    IEnumerator StageSelectButtonSelected(int index)
    {
        fadeOutAnimator.SetTrigger("fadeOut");

        yield return new WaitForSecondsRealtime(0.55f);

        gm.ChangeScene("Level" + index.ToString());
    }

    public void SettingsOpen(bool on = true)
    {
        settingsMenuGO.SetActive(on);
        MainMenuOpen(!on);

        if (on)
        {
            if (settingsMenuButtonTexts.Length == 0)
            {
                settingsMenuButtonTexts = new TextMeshProUGUI[settingsMenuGO.transform.GetChild(0).childCount];
                for (int i = 0; i < settingsMenuButtonTexts.Length; i++)
                {
                    settingsMenuButtonTexts[i] = settingsMenuGO.transform.GetChild(0).GetChild(i).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
                }
            }

            SettingsTextsUpdate();
        }
    }

    public void SettingsTextsUpdate(int index = -1)
    {
        switch (index)
        {
            case (0):
                gm.UpdateVolume("Master");
                if (gm.masterVolumeOn)
                {
                    settingsMenuButtonTexts[0].text = "x";
                }
                else
                {
                    settingsMenuButtonTexts[0].text = "";
                }
                break;
            case (1):
                gm.UpdateVolume("Music");
                if (gm.musicOn)
                {
                    settingsMenuButtonTexts[1].text = "x";
                }
                else
                {
                    settingsMenuButtonTexts[1].text = "";
                }
                break;
            case (2):
                gm.UpdateVolume("SFX");
                if (gm.SFXOn)
                {
                    settingsMenuButtonTexts[2].text = "x";
                }
                else
                {
                    settingsMenuButtonTexts[2].text = "";
                }
                break;
            default:
                gm.UpdateVolume("Master");
                SettingsTextsUpdate(0);
                gm.UpdateVolume("Music");
                SettingsTextsUpdate(1);
                gm.UpdateVolume("SFX");
                SettingsTextsUpdate(2);
                break;
        }
    }
}
