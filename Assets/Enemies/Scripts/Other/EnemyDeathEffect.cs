using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    public Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();  // sets the animator to the animator of the gameobject
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyDeathEffect1") || _animator.GetCurrentAnimatorStateInfo(0).IsName("BossDeathEffect1"))
        {
            Destroy(this.gameObject);   // once death animation has finished, object is destroyed
        }
    }
}
