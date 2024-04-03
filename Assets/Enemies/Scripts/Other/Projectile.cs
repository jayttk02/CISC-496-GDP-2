using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("The speed at which the projectile travels horizontally.")] public float speedX;
    [Tooltip("The speed at which the projectile travels vertically.")] public float speedY;

    [Tooltip("The length of time that the projectile exists uninterupted.")] public int duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speedX, transform.position.y + speedY);
        
        duration--;
        if (duration <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<HealthSystem>().UpdateHealth(1);
            Destroy(this.gameObject);
        }
    }
}
