using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [Header("Stats")]
    public string enemyName;
    [Tooltip("The amount of health the enemy currently has. At 0 or lower, it will be deleted.")] public int health;

    [Space(10)]
    [Tooltip("The speed at which the enemy travels.")] public float speed;

    [Header("GameObjects")]
    public Animator _animator;
    public Rigidbody2D _rigidbody;
    [Tooltip("The enemy's GroundCheck, used to determine if character is grounded.")] public Transform groundCheck;
    [Tooltip("The ground layer in the scene, used to determine if character is grounded.")] public LayerMask groundLayer;

    [Space(10)]
    [Tooltip("The effect that plays when the enemy is destroyed.")] public GameObject deathEffect;
    //Bonus Heart
    public GameObject heartDrop;

    // Start is called before the first frame update
    public virtual void Start()
    {
        Application.targetFrameRate = 60;           // should be moved to a GameManager/scene script in the future

        _animator = this.GetComponent<Animator>();  // sets the animator to the animator of the gameobject
        _rigidbody = this.GetComponent<Rigidbody2D>();  // sets the rigidbody to the rigidbody2D of the gameobject
        groundLayer = LayerMask.GetMask("Ground");      // find ground layer
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Behaviour();    // contains the unique behavior of each individual enemy
    }

    public virtual void Behaviour()
    {
        // Called in Update function. The unique behaviour of each individual enemy.
    }

    public RaycastHit2D GroundLinecast()
    {
        // calling this object requires the object to have a 
        return Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
    }

    public virtual void OnCollisionTrigger2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TakeDamage(1);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            TakeDamage(1);
        }
    }

    public virtual void PunchKickCollision(int strength, AttackType attackType)
    {
        if (attackType == AttackType.Punch)
        {
            TakeDamage(strength);
        }
        else
        {
            TakeDamage(strength);
        }
    }

    public virtual void BulletCollision(int strength)
    {
        TakeDamage(strength);
    }

    public virtual void TakeDamage(int damageTaken)
    {
        health -= damageTaken;  // subtracts the current health value by the parameter
        if (health <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(DamageFlash());
        }
    }

    IEnumerator DamageFlash()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.4901961f, 0.4901961f, 0.4901961f);

        yield return new WaitForSeconds(0.05f);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public IEnumerator NoDamageFlash()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 1f);

        yield return new WaitForSeconds(0.05f);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public virtual void Death()
    {
        float randValue = Random.value;
        //Player Health -1 then 50% Chance of Heart Drop
        if(HealthSystem.health <= 3 && HealthSystem.health >= 2 && randValue < .50f) Instantiate(heartDrop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        //Player Health -2
        if (HealthSystem.health < 2) Instantiate(heartDrop, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

        Instantiate(deathEffect, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);     // spawn death effect when health < 0
        Destroy(this.gameObject);   // destroys gameobject
    }
}
