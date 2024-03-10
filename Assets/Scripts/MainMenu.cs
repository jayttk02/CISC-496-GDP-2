using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Transform canvas;

    Text playerCountText;
    Animator playerCountAnimator;

    GameObject mainMenuGO;
    Button newGameButton;
    GameObject continueButtonGO;

    GameObject stageSelectMenuGO;

    public int numberOfPlayers;
    public int minNumberOfPlayers;

    [Space(10)]
    public bool existingGame;   // if there is no existing game, continue button is turned off, 

    // Start is called before the first frame update
    void Start()
    {
        playerCountText = canvas.GetChild(1).GetChild(1).GetComponent<Text>();
        playerCountAnimator = canvas.GetChild(1).GetComponent<Animator>();

        mainMenuGO = canvas.GetChild(2).gameObject;
        newGameButton = mainMenuGO.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        continueButtonGO = mainMenuGO.transform.GetChild(2).GetChild(1).gameObject;
        continueButtonGO.SetActive(existingGame);

        stageSelectMenuGO = canvas.GetChild(3).gameObject;

        MainMenuOpen(true);
        StageSelectOpen(false);
    }

    public void UpdatePlayerNumber(int add = 0)
    {
        numberOfPlayers += add;     // if adding a new player, add is 1. if subtracting a player, add is -1.

        playerCountText.text = numberOfPlayers.ToString() + " / " + minNumberOfPlayers.ToString();
        playerCountAnimator.SetBool("greenEffect", numberOfPlayers >= minNumberOfPlayers);
        newGameButton.interactable = numberOfPlayers >= minNumberOfPlayers;
    }

    void MainMenuOpen(bool on = true)
    {
        mainMenuGO.SetActive(on);

        if (on)
        {
            UpdatePlayerNumber();
        }
    }

    public void MainMenuButton(int index)
    {
        switch (index)
        {
            case (0):
                print("TODO: New Game");
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

    public void StageSelectOpen(bool on = true)
    {
        stageSelectMenuGO.SetActive(on);

        if (on)
        {
            MainMenuOpen(false);
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
            case (2):
                print("TODO: Load level 2");
                break;
        }
    }
}
