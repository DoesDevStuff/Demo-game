using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharAnimation : MonoBehaviour
{
    protected Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        
    }

    public void SetWalkAnim(bool animVal)
    {
        playerAnimator.SetBool("Walk", animVal);
    }

    public void AnimatePlayer(float speed)
    {
        SetWalkAnim(speed > 0);
    }
}
