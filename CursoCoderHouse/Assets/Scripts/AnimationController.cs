using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }
    private void Update()
    {
        CheckRunning();
        CheckAiming();
    }

    void CheckRunning()
    {
        if (player.isRunning)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void CheckAiming()
    {
        if (player.isAiming)
        {
            anim.SetBool("isAiming", true);
        }
        else
        {
            anim.SetBool("isAiming", false);
        }
    }
}
