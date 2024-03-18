using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Health
    public GameObject player;
    public static int healthCount = 3;
    public GameObject[] playerLivesUI;
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount == 3)
        {
            playerLivesUI[0].SetActive(true);
            playerLivesUI[1].SetActive(true);
            playerLivesUI[2].SetActive(true);
        }
        else if (healthCount == 2)
        {
            playerLivesUI[0].SetActive(true);
            playerLivesUI[1].SetActive(true);
            playerLivesUI[2].SetActive(false);
        }
        else if (healthCount == 1)
        {
            playerLivesUI[0].SetActive(true);
            playerLivesUI[1].SetActive(false);
            playerLivesUI[2].SetActive(false);
        }
        if (healthCount == 0)
        {
            playerLivesUI[0].SetActive(false);
            playerLivesUI[1].SetActive(false);
            playerLivesUI[2].SetActive(false);

            gameOver.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
