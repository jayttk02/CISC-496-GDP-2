using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreenGO;
    Animator pauseScreenAnimator;
    public GameObject mainMenuGO;
    GameObject quitMenuGO;

    [Space(10)]
    public bool isPaused;

    [Space(10)]
    public bool quitMenuQuitCheck;

    // Start is called before the first frame update
    void Start()
    {
        pauseScreenGO.SetActive(false);

        pauseScreenAnimator = pauseScreenGO.GetComponent<Animator>();

        mainMenuGO = pauseScreenGO.transform.GetChild(1).gameObject;

        quitMenuGO = pauseScreenGO.transform.GetChild(2).gameObject;
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

        print("bruv");
        SceneManager.LoadScene(0);
    }
}
