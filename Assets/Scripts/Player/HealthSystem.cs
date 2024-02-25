using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthSystem : MonoBehaviour
{
    public int health = 3;

    public Animator[] healthPointAnimators;

    // Start is called before the first frame update
    void Start()
    {

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
        else if (other.gameObject.tag == "Spike"){
            health = 0;
            UpdateHealth();
        }
    }

    public void UpdateHealth(int healthLost = 0)
    {
        health -= healthLost;
        if (health <= 0)
        {
            ResetScene();
        }
        else
        {
            for (int i = 0; i < healthLost; i++)
            {
                healthPointAnimators[health + i].SetTrigger("dead");
            }
        }
    }

    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
