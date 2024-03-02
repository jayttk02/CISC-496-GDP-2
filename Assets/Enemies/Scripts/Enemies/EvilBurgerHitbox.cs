using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBurgerHitbox : MonoBehaviour
{
    EvilBurger evilBurgerScript;

    void Start()
    {
        evilBurgerScript = transform.parent.GetComponent<EvilBurger>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Bullet(Clone)" || col.gameObject.tag == "Player")
        {
            print("bruh");
            evilBurgerScript._animator.SetBool("inRadius", true);
        }
    }
}
