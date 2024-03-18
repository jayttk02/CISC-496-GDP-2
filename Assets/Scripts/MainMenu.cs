using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class MainMenu : MonoBehaviour
{
    GameManager gm;
    public Transform canvas;

    Text playerCountText;
    Animator playerCountAnimator;

    GameObject mainMenuGO;
    Button newGameButton;
    GameObject continueButtonGO;

    GameObject stageSelectMenuGO;
    Button[] stageSelectMenuButtons;

    Animator fadeOutAnimator;

    [Space(10)]
    public string ip;
    private WebSocket wss;
    private IDictionary<string, bool> socketMap = new Dictionary<string, bool>();
    public int numberOfPlayers;
    public int minNumberOfPlayers;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Time.timeScale = 1;         // when loading from level pause screen

        playerCountText = canvas.GetChild(1).GetChild(1).GetComponent<Text>();
        playerCountAnimator = canvas.GetChild(1).GetComponent<Animator>();

        mainMenuGO = canvas.GetChild(2).gameObject;
        newGameButton = mainMenuGO.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        continueButtonGO = mainMenuGO.transform.GetChild(2).GetChild(1).gameObject;
        continueButtonGO.SetActive(gm.gameInProgress);

        stageSelectMenuGO = canvas.GetChild(3).gameObject;

        fadeOutAnimator = canvas.GetChild(4).gameObject.GetComponent<Animator>();

        MainMenuOpen(true);
        StageSelectOpen(false);

        wss = new WebSocket("wss://" + ip + ":8443");
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

    IEnumerator CheckForPlayers()
    {
        wss.Send("want # players");
        yield return new WaitForSeconds(3);
        StartCoroutine(CheckForPlayers());
    }

    void Update()
    {
        playerCountText.text = numberOfPlayers.ToString() + " / " + minNumberOfPlayers.ToString();
        playerCountAnimator.SetBool("greenEffect", numberOfPlayers >= minNumberOfPlayers);
        //newGameButton.interactable = numberOfPlayers >= minNumberOfPlayers;
        newGameButton.interactable = true;
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
                print("TODO: Settings");
                break;
            case (3):
                Application.Quit();
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
        switch (index)
        {
            case (0):
                print("TODO: Load level 0");
                break;
            case (1):
                print("TODO: Load level 1");
                break;
        }
    }
}
