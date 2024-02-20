using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    private PlayerMovement playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Aim with mouse
        // Vector3 mousePos = Input.mousePosition;
        // mousePos.z = 0;
        // Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        // mousePos.x = mousePos.x - objectPos.x;
        // mousePos.y = mousePos.y - objectPos.y;
        // float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        // Debug.Log(angle);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, playerMovement.aimAngle));
    }
}
