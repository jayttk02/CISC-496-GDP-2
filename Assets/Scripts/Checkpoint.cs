using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gm;
    public int checkPointIndex;
    
    public void Setup(GameManager newGM, int newCheckPointIndex)
    {
        gm = newGM;
        checkPointIndex = newCheckPointIndex;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gm.UpdateCurrentCheckpoint(checkPointIndex);
        }
    }
}
