using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Boss : _Enemy
{
    [Header("Boss")]
    public float bossIntroDuration;     // length of the boss intro (includes falling to the stage)
    public bool isInIntro;              // if the boss is in their intro cutscene

    [Space(10)]
    public GameObject healthbarGO;      // the parent gameobject of the boss name healthbar
    public Text healthbarName;          // text object for boss name
    public Slider healthbarSlider;      // slider for boss health

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

    public IEnumerator DoIntro()
    {
        isInIntro = true;       //
        if (_animator == null)
        {
            _animator = this.GetComponent<Animator>();
        }
        _animator.SetTrigger("Intro");      // triggers intro animation

        yield return new WaitForSeconds(bossIntroDuration);     // waits for the duration of boss intro

        isInIntro = false;
    }

    //////////////////////////////////////////////////////////////////////////////
    // ENEMY FUNCTIONS ///////////////////////////////////////////////////////////
    //////////////////////////////////////////////////////////////////////////////

    public override void OnCollisionTrigger2D(Collision2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30 * other.transform.localScale.x, 0));
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30 * other.transform.localScale.x, 0));      // (supposed to) push player back on contact
        }
    }

    public override void TakeDamage(int damageTaken)
    {
        base.TakeDamage(damageTaken);

        healthbarSlider.value = health;     // updates healthbar
    }

    public override void Death()
    {
        base.Death();

        healthbarGO.SetActive(false);       // when defeated, deactivates healthbar

        GameObject.Find("Player").GetComponent<PlayerMovement>().SetVictoryAnimationTrigger(1);
    }
}
