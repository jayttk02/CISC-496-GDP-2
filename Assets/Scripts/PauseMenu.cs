using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreenGO;

    GameObject mainMenuGO;
    GameObject[] mainMenuIndicators;

    GameObject quitMenuGO;
    GameObject[] quitMenuIndicators;

    [Space(10)]
    public bool isPaused;
    public int currentMenu;

    [Space(10)]
    public int mainMenuIndex;

    [Space(10)]
    public bool quitMenuQuitCheck;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreenGO.SetActive(false);

        mainMenuGO = pauseScreenGO.transform.GetChild(1).gameObject;
        mainMenuIndicators = new GameObject[mainMenuGO.transform.GetChild(1).childCount];
        for (int i = 0; i < mainMenuGO.transform.GetChild(1).childCount; i++)
        {
            mainMenuIndicators[i] = mainMenuGO.transform.GetChild(1).GetChild(i).gameObject;
        }

        quitMenuGO = pauseScreenGO.transform.GetChild(2).gameObject;
        quitMenuIndicators = new GameObject[quitMenuGO.transform.GetChild(2).childCount];
        for (int i = 0; i < quitMenuGO.transform.GetChild(2).childCount; i++)
        {
            quitMenuIndicators[i] = quitMenuGO.transform.GetChild(2).GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            Pause(!isPaused);
        }

        if (isPaused)
        {
            if (currentMenu == 0)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    MainMenuUpdate(-1);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    MainMenuUpdate(1);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    MainMenuButton();
                }
            }
            else if (currentMenu == 1)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    QuitMenuUpdate(-1);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    QuitMenuUpdate(1);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    QuitMenuButton();
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// PAUSE /////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////

    void Pause(bool on = true)
    {
        isPaused = on;
        pauseScreenGO.SetActive(on);
        if (isPaused)
        {
            Time.timeScale = 0;
            MainMenu();
            QuitMenu(false);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// MAIN MENU /////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////

    void MainMenu(bool on = true)
    {
        mainMenuGO.SetActive(on);
        if (on)
        {
            currentMenu = 0;
            mainMenuIndex = 0;
            MainMenuUpdate();
        }
    }

    void MainMenuUpdate(int direction = 0)
    {
        mainMenuIndex += direction;
        if (mainMenuIndex < 0)
        {
            mainMenuIndex = mainMenuIndicators.Length - 1;
        }
        else if (mainMenuIndex >= mainMenuIndicators.Length)
        {
            mainMenuIndex = 0;
        }

        for (int i = 0; i < mainMenuIndicators.Length; i++)
        {
            mainMenuIndicators[i].SetActive(i == mainMenuIndex);
        }
    }

    void MainMenuButton()
    {
        switch (mainMenuIndex)
        {
            case (0):
                Pause(false);
                break;
            case (1):
                print("To do: moveset");
                break;
            case (2):
                print("To do: settings");
                break;
            case (3):
                MainMenu(false);
                QuitMenu();
                break;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// QUIT MENU /////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////

    void QuitMenu(bool on = true)
    {
        quitMenuGO.SetActive(on);

        if (on)
        {
            currentMenu = 1;
            quitMenuQuitCheck = false;
            QuitMenuUpdate();
        }
    }

    void QuitMenuUpdate(int direction = 0)
    {
        if (direction != 0)
        {
            quitMenuQuitCheck = !quitMenuQuitCheck;
        }

        quitMenuIndicators[0].SetActive(!quitMenuQuitCheck);
        quitMenuIndicators[1].SetActive(quitMenuQuitCheck);
    }

    void QuitMenuButton()
    {
        if (quitMenuQuitCheck)
        {
            print("To do: quit");
        }
        else
        {
            QuitMenu(false);
            MainMenu();
        }
    }
}
