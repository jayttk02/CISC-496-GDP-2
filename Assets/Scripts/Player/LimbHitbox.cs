using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType { Punch, Kick };

public class LimbHitbox : MonoBehaviour
{
    public AttackType attackType;
    public bool isActive;
    public bool attackConnected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isActive || attackConnected) { }
        else if (other.gameObject.tag == "Enemy")
        {
            //print(other.gameObject.tag);
            other.gameObject.GetComponent<_Enemy>().PunchKickCollision(1, attackType);
            attackConnected = true;
        }

        //print(other.gameObject.name);
    }

    public void ToggleActive(bool on = true)
    {
        isActive = on;
        attackConnected = false;    // used so OnTriggerStay2D doesn't hit enemies every frame
    }
}
