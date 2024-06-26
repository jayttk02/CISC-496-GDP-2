using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthSystem : MonoBehaviour
{
    public static int health = 3;
    public AudioSource se_hit;
    public Animator[] healthPointAnimators;
    private PlayerMovement _playerMovement;
    public AudioSource se_heart;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        health = 3;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy"){
            UpdateHealth(1);
            Debug.Log(health);
        }
        else if (other.gameObject.name == "spr_drop")
        {
            UpdateHealth(1);
            Debug.Log(health);
        }
        else if (other.gameObject.tag == "Spike"){
            health = 0;
            UpdateHealth();
        }
        else if (other.gameObject.tag == "Bonus_Heart")
        {       
            health++;
            if (health > healthPointAnimators.Length)
            {
                health = healthPointAnimators.Length;
            }
            se_heart.Play();
            UpdateHealth(-1);
            Destroy(other.gameObject);
        }
    }

    public void UpdateHealth(int healthLost = 0)
    {
        //print("bruh");
        health -= healthLost;
        se_hit.Play();
        if (health <= 0)
        {
            ResetScene();
        }
        else
        {
            if (healthLost > 0)
            {
                _playerMovement.StartHitstun();
            }

            for (int i = 0; i < healthPointAnimators.Length; i++)
            {
                healthPointAnimators[i].SetBool("dead", i >= health);
            }
        }
    }

    public void ResetScene(){
        GameObject.Find("GameManager").GetComponent<GameManager>().ResetScene();    // done from gamemaanager script so it is not destroyed
    }


}
