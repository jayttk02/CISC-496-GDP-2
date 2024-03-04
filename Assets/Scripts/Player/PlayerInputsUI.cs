using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputsUI : MonoBehaviour
{
    public Transform uiTransform;
    public Animator uiAnimator;

    [Header("P1 Inputs")]
    public Animator punchAnimator;
    public Animator stepForwardAnimator;
    public Animator p1JumpAnimator;
    public GameObject p1JumpText;

    [Header("P1 Inputs")]
    public Animator shootAnimator;
    public Animator kickAnimator;
    public Animator stepBackwardAnimator;
    public Animator p2JumpAnimator;
    public GameObject p2JumpText;

    [Space(10)]
    public bool jumpCheck1;
    public bool jumpCheck2;

    // Start is called before the first frame update
    void Start()
    {
        uiAnimator = uiTransform.gameObject.GetComponent<Animator>();

        punchAnimator = uiTransform.GetChild(1).GetChild(0).gameObject.GetComponent<Animator>();
        stepForwardAnimator = uiTransform.GetChild(1).GetChild(1).gameObject.GetComponent<Animator>();
        p1JumpAnimator = uiTransform.GetChild(1).GetChild(2).gameObject.GetComponent<Animator>();
        p1JumpText = uiTransform.GetChild(1).GetChild(3).gameObject;

        shootAnimator = uiTransform.GetChild(2).GetChild(0).gameObject.GetComponent<Animator>();
        kickAnimator = uiTransform.GetChild(2).GetChild(1).gameObject.GetComponent<Animator>();
        stepBackwardAnimator = uiTransform.GetChild(2).GetChild(2).gameObject.GetComponent<Animator>();
        p2JumpAnimator = uiTransform.GetChild(2).GetChild(3).gameObject.GetComponent<Animator>();
        p2JumpText = uiTransform.GetChild(2).GetChild(4).gameObject;
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
}
