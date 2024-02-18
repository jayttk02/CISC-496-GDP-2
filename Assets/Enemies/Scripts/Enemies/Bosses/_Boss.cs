using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Boss : _Enemy
{
    [Header("Boss")]
    public GameObject healthbarGO;
    public Text healthbarName;
    public Slider healthbarSlider;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        healthbarName = healthbarGO.transform.GetChild(0).GetComponent<Text>();
        healthbarName.text = enemyName;
        healthbarSlider = healthbarGO.transform.GetChild(1).GetComponent<Slider>();
        healthbarSlider.maxValue = health;
        healthbarSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //////////////////////////////////////////////////////////////////////////////
    // ENEMY FUNCTIONS ///////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////

    public override void OnCollisionTrigger2D(Collision2D other)
    {
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);

        healthbarSlider.value = health;
    }

    public override void Death()
    {
        base.Death();

        healthbarGO.SetActive(false);
    }
}
