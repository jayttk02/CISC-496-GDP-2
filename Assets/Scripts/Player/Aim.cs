using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private bool mobileControls;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        mobileControls = playerMovement.mobileControls;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mobileControls)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerMovement.aimAngle));
        }
        else
        {
            //Aim with mouse
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle_forward") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("walk_forward") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("punch_forward") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("kick_forward"))
            {
                if (angle <= 100 && angle >= -100)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
            }
            else
            {
                if (angle >= 80 || angle <= -80)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
            }
        }
    }
}
