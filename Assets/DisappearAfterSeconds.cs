using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    public float secs;
    void Start()
    {
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(secs);
        Destroy(this.gameObject);
    }
}
