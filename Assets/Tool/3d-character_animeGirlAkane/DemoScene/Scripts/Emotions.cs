using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Emotions : MonoBehaviour
{
    public Animator animator;

    void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("emotionsInt", 0);
    }
    public void emotionsChange() {
        animator.SetInteger("emotionsInt", 0);
        var clickedButton = EventSystem.current.currentSelectedGameObject.name;
        if (clickedButton == "neutral") {
            animator.SetInteger("emotionsInt", 0);
        }
        else if (clickedButton == "smiling") {
            animator.SetInteger("emotionsInt", 1);
        }
        else if (clickedButton == "happy") {
            animator.SetInteger("emotionsInt", 2);
        }
        else if (clickedButton == "angry") {
            animator.SetInteger("emotionsInt", 3);
        }
        else if (clickedButton == "sad") {
            animator.SetInteger("emotionsInt", 4);
        }
        else if (clickedButton == "surprised") {
            animator.SetInteger("emotionsInt", 5);
        }
    }
}