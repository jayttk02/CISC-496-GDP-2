using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameManager gm;

    public GameObject pauseScreenGO;
    Animator pauseScreenAnimator;
    public GameObject mainMenuGO;
    public GameObject movesetMenuGO;
    public GameObject[] movesetMenuScreens;
    public GameObject quitMenuGO;

    [Space(10)]
    public bool isPaused;

    [Space(10)]
    public int movesetMenuIndex;

    [Space(10)]
    public bool quitMenuQuitCheck;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        pauseScreenGO.SetActive(false);
        pauseScreenAnimator = pauseScreenGO.GetComponent<Animator>();

        mainMenuGO = pauseScreenGO.transform.GetChild(1).gameObject;
        movesetMenuGO = pauseScreenGO.transform.GetChild(2).gameObject;
        quitMenuGO = pauseScreenGO.transform.GetChild(3).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)))
        {
            Pause(!isPaused);
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
            MovesetMenu(false);
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
    }

    public void MainMenuButton(int index)
    {
        switch (index)
        {
            case (0):
                Pause(false);
                break;
            case (1):
                MainMenu(false);
                MovesetMenu();
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
    /// MOVESET MENU //////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////

    void MovesetMenu(bool on = true)
    {
        movesetMenuGO.SetActive(on);

        if (on)
        {
            movesetMenuIndex = 0;

            if (movesetMenuScreens.Length == 0)
            {
                movesetMenuScreens = new GameObject[movesetMenuGO.transform.childCount - 2];
                for (int i = 0; i < movesetMenuScreens.Length; i++)
                {
                    movesetMenuScreens[i] = movesetMenuGO.transform.GetChild(i + 2).gameObject;
                }
            }

            MovesetMenuUpdate();
        }
    }

    public void MovesetMenuUpdate(int dir = 0)
    {
        movesetMenuIndex += dir;
        if (movesetMenuIndex < 0)
        {
            movesetMenuIndex = movesetMenuScreens.Length - 1;
        }
        else if (movesetMenuIndex > movesetMenuScreens.Length - 1)
        {
            movesetMenuIndex = 0;
        }

        for (int i = 0; i < movesetMenuScreens.Length; i++)
        {
            movesetMenuScreens[i].SetActive(i == movesetMenuIndex);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// QUIT MENU /////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////

    void QuitMenu(bool on = true)
    {
        quitMenuGO.SetActive(on);
    }

    public void QuitMenuButton(int index)
    {
        if (index == 1)
        {
            StartCoroutine(FadeToMainMenu());
        }
        else
        {
            QuitMenu(false);
            MainMenu(true);
        }
    }

    IEnumerator FadeToMainMenu()
    {
        MainMenu(false);
        QuitMenu(false);
        pauseScreenAnimator.SetTrigger("fadeOut");

        yield return new WaitForSecondsRealtime(0.55f);

        gm.ChangeScene("MainMenu");
    }
}
