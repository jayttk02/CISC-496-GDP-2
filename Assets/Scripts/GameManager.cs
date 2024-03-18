using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Stages")]
    public bool gameInProgress;
    public int highestLevelUnlocked;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

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
        }

        DontDestroyOnLoad(this.gameObject);
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
