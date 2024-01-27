using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float startSpeed;
    public float moveSpeed = 5f;
    public float jumpSpeed = 10f;
    public Rigidbody2D player_rigidbody;
    private Vector2 movement;
    private bool isGrounded;

    //Timer
    private float jumpCheckTimer;
    private float grabCheckTimer;

    void Start()
    {
        startSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        //Check if Jump was hit by both with small delay using V and C as temp
        jumpCheckTimer = 0;
        if (Input.GetKey(KeyCode.V))
        {
            jumpCheckTimer += Time.deltaTime;
            if (Input.GetKey(KeyCode.C) && jumpCheckTimer < jumpCheckTimer + 1)
            {
                player_rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
            }
        }
        jumpCheckTimer = 0;
        if (Input.GetKey(KeyCode.C))
        {
            jumpCheckTimer += Time.deltaTime;
            if (Input.GetKey(KeyCode.V) && jumpCheckTimer < jumpCheckTimer + 1)
            {
                player_rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);
            }
        }

        //Check if Grab was hit by both with small delay using N and M as temp
        jumpCheckTimer = 0;
        if (Input.GetKey(KeyCode.N))
        {
            jumpCheckTimer += Time.deltaTime;
            if (Input.GetKey(KeyCode.M) && grabCheckTimer < grabCheckTimer + 1)
            {
                //grab ledge
            }
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        //X and Y axis movement arrow or wasd works
        movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        //Player 1

        //Kick
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Kick");
            /*Do damage to close enemies in range
            Animation here
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f)
            {
                Kick does damage  
                Sound effect play
                Visual indicator damage done
            }
            */
        }

        //Player 2
        //Shoot
        if (Input.GetKey(KeyCode.Keypad0))
        {
            Debug.Log("Shoot");
            /*transform.position = Vector2.MoveTowards(transform.position, enemyLoc, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f)
            {
                Bullet hit enemy so do effect here
                Sound effect play
                Visual indicator damage done
                ResetObj(); to reset the bullet back
            }

            */
        }
        //Stomp
        if (Input.GetKey(KeyCode.Keypad1))
        {
            Debug.Log("Stomp");
            /*Do damage to close enemies in range
            Animation here
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f)
            {
                Stomp does damage  
                Sound effect play
                Visual indicator damage done
            }
            */
        }
        //Shield - Should it be held? Able to move while held? Should buttons only be one at a time?
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("Shield");
            /*Do damage to close enemies in range
            Animation here
            if (Vector2.Distance(transform.position, enemyAtk) < 0.5f)
            {
                Shield block damage  
                Sound effect play
                Visual indicator damage blocked
            }
            */
        }

    }

    void Move()
    {
        player_rigidbody.MovePosition(player_rigidbody.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D Collider)
    {
        if (Collider.collider.gameObject.name == "spr_floor") isGrounded = true;
    }

    void ResetObj()
    {
        //transform.position = player_rigidbody.transform.position;      
    }
}
