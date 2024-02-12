using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float startSpeed;
    public float moveSpeed = 5f;
    public float aimSpeed = 5f;
    public float jumpSpeed = 20f;
    public Rigidbody2D player_rigidbody;
    private Vector2 movement;
    private bool isGrounded;
    public GameObject gun;
    public GameObject bullet;
    public float jumpDuration = 1f;
    bool shooting = false;

    //Timer
    private float jumpCheckTimer;
    private float grabCheckTimer;
    private float conflictCheckTimer;
    private bool setTimerGrab = false;
    private bool setTimerJump = false;
    private bool setTimerJump2 = false;
    private bool setTimerConflict = false;
    private float respond;
    public float duration = 2f;

    void Start()
    {
        startSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        //Check if Jump was hit by both with small delay using V and C as temp
        if (Input.GetKey(KeyCode.V) && !setTimerJump2)
        {
            if (!setTimerJump) 
            {
                jumpCheckTimer = Time.time;
                setTimerJump = true;
            }
            if (Input.GetKey(KeyCode.C))
            {
                respond = Time.time;
                if ((respond - jumpCheckTimer) > 0 && (respond - jumpCheckTimer) < duration && isGrounded) {
                    StartCoroutine(Jump());
                }
            }
        }
        if (Input.GetKey(KeyCode.C) && !setTimerJump)
        {
            if (!setTimerJump2) 
            {
                jumpCheckTimer = Time.time;
                setTimerJump2 = true;
            }
            if (Input.GetKey(KeyCode.V))
            {
                respond = Time.time;
                if ((respond - jumpCheckTimer) > 0 && (respond - jumpCheckTimer) < duration && isGrounded) {
                    StartCoroutine(Jump());
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            setTimerJump = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            setTimerJump2 = false;
        }

        //Check if Grab was hit by both with small delay using N and M as temp
        grabCheckTimer = 0;
        if (Input.GetKey(KeyCode.N))
        {
            if (!setTimerGrab) 
            {
                grabCheckTimer = Time.time;
                setTimerGrab = true;
            }
            if (Input.GetKey(KeyCode.M))
            {
                respond = Time.time;
                if ((respond - grabCheckTimer) > 0 && (respond - grabCheckTimer) < duration && isGrounded) {
                    //grab ledge
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            setTimerGrab = false;
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
        conflictCheckTimer = 0;
        if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))) {
            if (!setTimerConflict) 
            {
                conflictCheckTimer = Time.time;
                setTimerConflict = true;
            } else if ((Time.time - conflictCheckTimer) > duration) {
                Debug.Log("Conflict");
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            setTimerConflict = false;
        }

        //Player 1

        //Punch
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Punch");
            /*Do damage to close enemies in range
            Animation here
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f && enemy.isPunchable)
            {
                Punch does damage  
                Sound effect play
                Visual indicator damage done
            }
            */
        }
        
        //Kick
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Kick");
            /*Do damage to close enemies in range
            Animation here
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f && enemy.isKickable)
            {
                Kick does damage  
                Sound effect play
                Visual indicator damage done
            }
            */
        }

        //Player 2
        //Shoot
        if (Input.GetKey(KeyCode.E) && !shooting)
        {
            Debug.Log("Shoot");
            shooting = true;
            StartCoroutine(Fire());
            /*transform.position = Vector2.MoveTowards(transform.position, enemyLoc, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f && enemy.isShootable)
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
            if (Vector2.Distance(transform.position, enemyLoc) < 0.5f && enemy.isStompable)
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

    void OnCollisionExit2D(Collision2D Collider)
    {
        if (Collider.collider.gameObject.name == "spr_floor") isGrounded = false;
    }

    void ResetObj()
    {
        //transform.position = player_rigidbody.transform.position;      
    }

    IEnumerator Fire()
    {
        float xOffset = Mathf.Cos(gun.transform.eulerAngles.z * Mathf.Deg2Rad);
        float yOffset = Mathf.Sin(gun.transform.eulerAngles.z * Mathf.Deg2Rad);
        Instantiate(bullet, new Vector2(gun.transform.position.x + xOffset, gun.transform.position.y + yOffset), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        shooting = false;
    }

    IEnumerator Jump()
    {
        GetComponent<Rigidbody2D>().gravityScale = -1;
        yield return new WaitForSeconds(jumpDuration);
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
