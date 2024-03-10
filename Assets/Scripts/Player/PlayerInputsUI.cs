using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputsUI : MonoBehaviour
{
    public Transform uiTransform;
    public Animator uiAnimator;

    float shootTimer;
    GameObject shootTimerGO;
    Slider shootTimerSlider;

    Animator punchAnimator;
    Animator stepForwardAnimator;
    Animator p1JumpAnimator;
    GameObject p1JumpText;

    Animator shootAnimator;
    Animator kickAnimator;
    Animator stepBackwardAnimator;
    Animator p2JumpAnimator;
    GameObject p2JumpText;

    [Space(10)]
    public bool jumpCheck1;
    public bool jumpCheck2;

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = uiTransform.gameObject.GetComponent<Animator>();

        shootTimerGO = uiTransform.GetChild(1).gameObject;
        shootTimerSlider = shootTimerGO.GetComponent<Slider>();

        punchAnimator = uiTransform.GetChild(2).GetChild(0).gameObject.GetComponent<Animator>();
        stepForwardAnimator = uiTransform.GetChild(2).GetChild(1).gameObject.GetComponent<Animator>();
        p1JumpAnimator = uiTransform.GetChild(2).GetChild(2).gameObject.GetComponent<Animator>();
        p1JumpText = uiTransform.GetChild(2).GetChild(3).gameObject;

        shootAnimator = uiTransform.GetChild(3).GetChild(0).gameObject.GetComponent<Animator>();
        kickAnimator = uiTransform.GetChild(3).GetChild(1).gameObject.GetComponent<Animator>();
        stepBackwardAnimator = uiTransform.GetChild(3).GetChild(2).gameObject.GetComponent<Animator>();
        p2JumpAnimator = uiTransform.GetChild(3).GetChild(3).gameObject.GetComponent<Animator>();
        p2JumpText = uiTransform.GetChild(3).GetChild(4).gameObject;
    }

    void Update()
    {
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
            shootTimerSlider.value = shootTimer;
            if (shootTimer <= 0f)
            {
                shootTimerGO.SetActive(false);
            }
        }
    }

    public void Conflict()
    {
        uiAnimator.SetTrigger("shake");
    }

    public void ButtonHold(string buttonName, bool isHeld)
    {
        Animator theAnimator = null;

        switch (buttonName)
        {
            case ("Punch"): theAnimator = punchAnimator; break;
            case ("Step Forward"): theAnimator = stepForwardAnimator; break;

            case ("Shoot"): theAnimator = shootAnimator; break;
            case ("Kick"): theAnimator = kickAnimator; break;
            case ("Step Backward"): theAnimator = stepBackwardAnimator; break;

            case ("P1 Jump"):
                theAnimator = p1JumpAnimator;
                if (jumpCheck1 && !isHeld)
                {
                    jumpCheck1 = false;
                }
                //isHeld = isHeld && !jumpCheck1;
                JumpTextUpdate(1, isHeld);
                break;
            case ("P2 Jump"):
                theAnimator = p2JumpAnimator;
                if (jumpCheck2 && !isHeld)
                {
                    jumpCheck2 = false;
                }
                //isHeld = isHeld && !jumpCheck2;
                JumpTextUpdate(2, isHeld);
                break;
        }

        theAnimator.SetBool("hold", isHeld);
    }

    public void JumpTextUpdate(int playerNum, bool on)
    {
        if (playerNum == 1)
        {
            p1JumpText.SetActive(on && !jumpCheck1);
        }
        else
        {
            p2JumpText.SetActive(on && !jumpCheck2);
        }
    }

    public void JumpUpdate()        // deselects jump buttons when jump is performed
    {
        jumpCheck1 = true;
        jumpCheck2 = true;
    }

    public void ShootTimer()
    {
        shootTimer = 1f;
        shootTimerGO.SetActive(true);
        shootTimerSlider.value = 1f;
    }
}
