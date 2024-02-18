using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HealthSystem : MonoBehaviour
{
    public int health = 3;
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
                health = health -1;
                Debug.Log(health);
            }
            else if (other.gameObject.tag == "Spike"){
                health = 0;
            }

            if (health == 0){
                ResetScene();
            }
        }
    public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
