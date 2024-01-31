using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [Header("Stats")]
    [Tooltip("The amount of health the enemy currently has. At 0 or lower, it will be deleted.")] public int health;

    [Space(10)]
    [Tooltip("The speed at which the enemy travels.")] public float speed;

    [Header("GameObjects")]
    public Animator _animator;

    [Space(10)]
    [Tooltip("The effect that plays when the enemy is destroyed.")] public GameObject deathEffect;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Application.targetFrameRate = 60;           // should be moved to a GameManager/scene script in the future

        _animator = this.GetComponent<Animator>();  // sets the animator to the animator of the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();    // contains the unique behavior of each individual enemy
    }

    public virtual void Behaviour()
    {
        // Called in Update function. The unique behaviour of each individual enemy.
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;  // subtracts the current health value by the parameter
        if (health <= 0)
        {
            Instantiate(deathEffect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);     // spawn death effect when health < 0
            Destroy(this.gameObject);   // destroys gameobject
        }
    }
}
