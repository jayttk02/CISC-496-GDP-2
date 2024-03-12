using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using WebSocketSharp;

public class PlayerMovement : MonoBehaviour
{
    public string ip;
    public bool mobileControls;
    public bool canMove;        // set to false if player is not supposed to be allowed to move (boss intros, etc.)

    [Space(10)]
    private float startSpeed;
    public float moveSpeed = 5f;
    public float aimSpeed = 5f;
    public float jumpSpeed = 20f;
    public Rigidbody2D player_rigidbody;
    private Vector2 movement;
    private bool isGrounded;
    public GameObject gun;
    public float gunOffset = 0.75f;
    public GameObject leg;
    public float legOffset = 0.5f;
    public LimbHitbox legHitbox;
    public GameObject arm;
    public float armOffset = 0.75f;
    public LimbHitbox armHitbox;
    public GameObject bullet;
    public float jumpDuration = 1f;
    bool shooting = false;
    bool kicking = false;
    bool punching = false;
    bool grabbing = false;


    //Timer
    private float jumpCheckTimer;
    private float grabCheckTimer;
    private float conflictCheckTimer;

    private WebSocket wss;
    private IDictionary<string, bool> socketMap = new Dictionary<string,bool>
    {
        ["sf"] = false,
        ["sb"] = false,
        ["1j"] = false,
        ["2j"] = false,
        ["p"] = false,
        ["s"] = false,
        ["k"] = false,
    };
    private bool setTimerGrab = false;
    private bool setTimerGrab2 = false;
    private bool jump1Occurring = false;
    private bool jump2Occurring = false;
    private bool setTimerConflict = false;
    private float respond;
    public float duration = 2f;
    float grabDuration = 0.5f;

    //Audio
    public AudioSource se_walk;
    public AudioSource se_jump;
    public AudioSource se_shoot;
   

    public PlayerInputsUI playerInputsUI;       // script that effects the UI button display

    public float aimAngle;
    
    void Start()
    {
        if (mobileControls)
        {
            wss = new WebSocket("wss://"+ ip + ":8443");
            wss.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            wss.Connect();
            wss.OnMessage += (sender, e) =>
            {
                // Debug.Log("Message Received from "+((WebSocket)sender).Url+", Data : "+e.Data);
                if (socketMap.ContainsKey(e.Data))
                {
                    socketMap[e.Data] = true;
                }
                else if(e.Data.Substring(0, 2) == "a:")
                {
                    aimAngle = float.Parse(e.Data.Substring(2), System.Globalization.CultureInfo.InvariantCulture);
                }
            };
        }
        startSpeed = moveSpeed;
        canMove = true;     // canMove value starts true
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)       // if canMove is false, no other inputs go through
        {
            movement = new Vector2(0, 0);
            return;
        }

        ProcessInputs();

        bool activateJump1;
        float timeBetweenJumps;

        //Check if Jump was hit by both with small delay using V and C as temp
        if (mobileControls)
        {
            activateJump1 = socketMap["1j"] && !jump2Occurring && !jump1Occurring;
            
        }
        else
        {
            activateJump1 = Input.GetKey(KeyCode.V) && !jump2Occurring && !jump1Occurring;
        }

        if (activateJump1)
        {
            if (!jump1Occurring)
            {
                jumpCheckTimer = Time.time;
                jump1Occurring = true;
            }

        }
        else if (jump1Occurring)
        {
                timeBetweenJumps = Time.time - jumpCheckTimer;
                if (timeBetweenJumps >= 0 && timeBetweenJumps < duration)
                {
                    bool player2Jump;
                    if(mobileControls){
                        player2Jump = socketMap["2j"];
                    }
                    else{
                        player2Jump = Input.GetKey(KeyCode.C);
                    }
                    if(player2Jump)
                    {
                        if (isGrounded) {
                            StartCoroutine(Jump());
                            playerInputsUI.JumpUpdate();
                        }
                    }
                }
                else
                {
                    if (mobileControls)
                    {
                        socketMap["1j"] = false;
                    }
                    jump1Occurring = false;
                }
        }

        bool activateJump2;
        if (mobileControls)
        {
            activateJump2 = socketMap["2j"] && !jump1Occurring && !jump2Occurring;
        }
        else
        {
            activateJump2 = Input.GetKey(KeyCode.C) && !jump1Occurring && !jump2Occurring;
        }

        if (activateJump2)
        {
            if (!jump2Occurring)
            {
                jumpCheckTimer = Time.time;
                jump2Occurring = true;
            }
        }
        else if (jump2Occurring)
        {
            timeBetweenJumps = Time.time - jumpCheckTimer;
            if (timeBetweenJumps >= 0 && timeBetweenJumps < duration)
            {
                bool player1Jump;
                if(mobileControls){
                    player1Jump = socketMap["1j"];
                }
                else{
                    player1Jump = Input.GetKey(KeyCode.V);
                }
                if(player1Jump)
                {
                    if (isGrounded) {
                        StartCoroutine(Jump());
                        playerInputsUI.JumpUpdate();
                    }
                }
            }
            else
            {
                if (mobileControls)
                {
                    socketMap["2j"] = false;
                }
                jump2Occurring = false;
            }
        }  
        
        playerInputsUI.ButtonHold("P1 Jump", jump1Occurring);      // player input ui checks if player 1's jump is held down
        playerInputsUI.ButtonHold("P2 Jump", jump2Occurring);      // player input ui checks if player 2's jump is held down

        // NOTE: GRAB NOT IMPLEMENTED YET
        if (!mobileControls){
            //Check if Grab was hit by both with small delay using N and M as temp
            if (Input.GetKey(KeyCode.N) && !setTimerGrab2)
            {
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
                        if ((respond - grabCheckTimer) >= 0 && (respond - grabCheckTimer) < duration && isGrounded) {
                            //grab ledge
                        }
                    }
                }
                if (Input.GetKeyUp(KeyCode.N))
                {
                    setTimerGrab = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    IEnumerator Smooth()
    {
        float offset;
        if (movement.x > 0)
        {
            offset = -0.1f;
        }
        else
        {
            offset = 0.1f;
        }

        while (Math.Abs(movement.x) > 0.1)
        {
            movement.x += offset;
            yield return new WaitForSeconds(0.2f);
        }

        movement.x = 0;
    }

    void ProcessInputs()
    {
        //X and Y axis movement arrow or wasd works
        //or websocket data from mobile

        if (mobileControls)
        {
            if (socketMap["sf"])
            {
                movement.x = 1;
            }
            else if (socketMap["sb"])
            {
                movement.x = -1;
            }
            else if(movement.x != 0)
            {
                StartCoroutine(Smooth());
            }
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        //movement.y = Input.GetAxisRaw("Vertical");
        // TODO: update w/ mobile controls
        conflictCheckTimer = 0;
        bool activateConflict;

        if (mobileControls)
        {
            activateConflict = socketMap["sf"] && socketMap["sb"];
        }
        else
        {
            activateConflict = (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D));
        }
        if (activateConflict) {
            if (!setTimerConflict) 
            {
                conflictCheckTimer = Time.time;
                setTimerConflict = true;
                playerInputsUI.Conflict();  // triggers the player input ui to shake, indicating a conflict is occuring
            } 
            else if ((Time.time - conflictCheckTimer) > duration) {
                Debug.Log("Conflict");
            }
        }

        if (activateConflict)
        {
            if (mobileControls)
            {
                if (!socketMap["sf"] || !socketMap["sb"])
                {
                    setTimerConflict = false;
                }
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    setTimerConflict = false;
                }
            }
        }
        
        playerInputsUI.ButtonHold("Step Forward",movement.x > 0);      // player input ui checks if step forward is held down
        playerInputsUI.ButtonHold("Step Backward", movement.x < 0);    // player input ui checks if step backward is held down

        socketMap["sf"] = false;
        socketMap["sb"] = false;

        
        //Player 1

        //Punch
        bool activatePunch;
        if (mobileControls)
        {
            activatePunch = socketMap["p"] && !punching;
        }
        else
        {
            activatePunch = Input.GetKey(KeyCode.Q) && !punching;
        }
        if(activatePunch)
        {
            //Debug.Log("Punch");
            punching = true;
            StartCoroutine(Punch());
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
        playerInputsUI.ButtonHold("Punch", activatePunch);    // player input ui checks if punch is held down

        //Kick
        bool activateKick;
        if (mobileControls)
        {
            activateKick = socketMap["k"] && !kicking;
        }
        else
        {
            activateKick = Input.GetKey(KeyCode.T) && !kicking;
        }
        if(activateKick)
        {
            //Debug.Log("Kick");
            kicking = true;
            StartCoroutine(Kick());
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
        playerInputsUI.ButtonHold("Kick", activateKick);     // player input ui checks if kick is held down

        //Player 2
        //Shoot
        bool activateShoot;
        if (mobileControls)
        {
            activateShoot = socketMap["s"] && !shooting;
        }
        else
        {
            activateShoot = Input.GetKey(KeyCode.E) && !shooting;
        }
        if(activateShoot)
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
        playerInputsUI.ButtonHold("Shoot", activateShoot);    // player input ui checks if shoot is held down

        //Stomp
        // NOTE: NOT IMPLEMENTED YET
        if (!mobileControls)
        {
            if(Input.GetKey(KeyCode.Keypad1))
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
        }

        //Shield - Should it be held? Able to move while held? Should buttons only be one at a time?
        // NOTE: NOT IMPLEMENTED YET
        if (!mobileControls){
            if(Input.GetKeyDown(KeyCode.Keypad2))
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
    }

    void Move()
    {
        player_rigidbody.MovePosition(player_rigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement.x > 0) 
        {
            if (!kicking) {
                leg.GetComponent<Follow>().xOffset = legOffset;
            } else {
                leg.GetComponent<Follow>().xOffset = legOffset + 0.5f;
                if (leg.transform.eulerAngles.z > 300)
                {
                    leg.transform.Rotate (new Vector3 (0, 0, 60));
                }
            }
            if (!punching) {
                arm.GetComponent<Follow>().xOffset = armOffset;
            } else {
                arm.GetComponent<Follow>().xOffset = armOffset + 0.5f;
            }
            gun.GetComponent<Follow>().xOffset = gunOffset;
        }
        else if (movement.x < 0)
        {
            //Debug.Log(leg.transform.eulerAngles.z);
            if (!kicking) {
                leg.GetComponent<Follow>().xOffset = -legOffset;
            } else {
                leg.GetComponent<Follow>().xOffset = -legOffset - 0.5f;
                if (leg.transform.eulerAngles.z > 0 && leg.transform.eulerAngles.z < 300)
                {
                    leg.transform.Rotate (new Vector3 (0, 0, -60));
                }
            }
            if (!punching) {
                arm.GetComponent<Follow>().xOffset = -armOffset;
            } else {
                arm.GetComponent<Follow>().xOffset = -armOffset - 0.5f;
            }
            gun.GetComponent<Follow>().xOffset = -gunOffset;
        }
    }

    void OnCollisionEnter2D(Collision2D Collider)
    {
        se_jump.Play();
        if (Collider.collider.tag == "Ground") isGrounded = true;
        //if (Collider.collider.gameObject.name == "Tilemap") isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D Collider)
    {
        if (Collider.collider.tag == "Ground") isGrounded = false;
        //if (Collider.collider.gameObject.name == "Tilemap") isGrounded = false;
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
        playerInputsUI.ShootTimer();
        se_shoot.Play();
        yield return new WaitForSeconds(1f);
        shooting = false;
        socketMap["s"] = false;
    }

    IEnumerator Kick()
    {
        if (leg.GetComponent<Follow>().xOffset > 0) 
        {
            leg.transform.Rotate (new Vector3 (0, 0, 30));
            leg.GetComponent<Follow>().xOffset += 0.5f;
        }
        else if (leg.GetComponent<Follow>().xOffset < 0)
        {
            leg.transform.Rotate (new Vector3 (0, 0, -30));
            leg.GetComponent<Follow>().xOffset -= 0.5f;
        }
        //leg.GetComponent<BoxCollider2D>().enabled = true;         // commented out when new leg hitbox code was implemented
        if (legHitbox == null)
        {
            legHitbox = leg.transform.GetChild(0).GetComponent<LimbHitbox>();
        }
        legHitbox.ToggleActive();
        yield return new WaitForSeconds(0.01f);
        //leg.GetComponent<BoxCollider2D>().enabled = false;        // commented out when new leg hitbox code was implemented
        yield return new WaitForSeconds(0.49f);
        legHitbox.ToggleActive(false);
        if (leg.GetComponent<Follow>().xOffset > 0) 
        {
            leg.transform.Rotate (new Vector3 (0, 0, -30));
            leg.GetComponent<Follow>().xOffset -= 0.5f;
        }
        else if (leg.GetComponent<Follow>().xOffset < 0)
        {
            leg.transform.Rotate (new Vector3 (0, 0, 30));
            leg.GetComponent<Follow>().xOffset += 0.5f;
        }
        kicking = false;
        socketMap["k"] = false;
    }

    IEnumerator Punch()
    {
        if(arm.GetComponent<Follow>().xOffset > 0) 
        {
            arm.GetComponent<Follow>().xOffset += 0.5f;
        }
        else if (arm.GetComponent<Follow>().xOffset < 0)
        {
            arm.GetComponent<Follow>().xOffset -= 0.5f;
        }
        //arm.GetComponent<BoxCollider2D>().enabled = true;         // commented out when new arm hitbox code was implemented
        if (armHitbox == null)
        {
            armHitbox = arm.transform.GetChild(0).GetComponent<LimbHitbox>();
        }
        armHitbox.ToggleActive();
        yield return new WaitForSeconds(0.01f);
        //arm.GetComponent<BoxCollider2D>().enabled = false;        // commented out when new arm hitbox code was implemented
        yield return new WaitForSeconds(0.49f);
        armHitbox.ToggleActive(false);
        if (arm.GetComponent<Follow>().xOffset > 0) 
        {
            arm.GetComponent<Follow>().xOffset -= 0.5f;
        }
        else if (arm.GetComponent<Follow>().xOffset < 0)
        {
            arm.GetComponent<Follow>().xOffset += 0.5f;
        }
        punching = false;
        socketMap["p"] = false;
    }

    IEnumerator Jump()
    {
        GetComponent<Rigidbody2D>().gravityScale = -17;
        yield return new WaitForSeconds(jumpDuration);
        GetComponent<Rigidbody2D>().gravityScale = 20;
    }

    public bool iskicking() {
        return kicking;
    }

    public bool ispunching() {
        return punching;
    }
    public bool isgrabbing() {
        return grabbing;
    }
}
