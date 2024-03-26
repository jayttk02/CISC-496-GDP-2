using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private PlayerMovement playerMovementScript;

    [Space(10)]
    public GameObject bossObject;
    private _Boss bossScript;
    public Vector2 bossSpawnLocation;   // location where the boss spawns

    [Space(10)]
    public GameObject bossHealthbarGO;

    void Start()
    {
        bossHealthbarGO.SetActive(false);   // sets boss healthbar off until the player triggers the boss spawn
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Player enter");
            playerMovementScript = other.gameObject.GetComponent<PlayerMovement>();     // gets player movement script
            StartCoroutine(BossIntro());
        }
    }

    IEnumerator BossIntro()
    {
        playerMovementScript.waiting = true;       // prevents the player from moving
        GameObject boss = Instantiate(bossObject, bossSpawnLocation, Quaternion.identity);  // creates boss
        bossScript = boss.GetComponent<_Boss>();    // gets boss script
        bossScript.healthbarGO = bossHealthbarGO;   // sets boss healthbar GO
        StartCoroutine(bossScript.DoIntro());       // boss starts its intro animation

        yield return new WaitForSeconds(bossScript.bossIntroDuration);      // wait for the duration of the boss intro animation
        
        playerMovementScript.waiting = false;    // player can move again
        bossHealthbarGO.SetActive(true);        // turn on health bar
        Destroy(this.gameObject);               // delete gameobject
    }
}
