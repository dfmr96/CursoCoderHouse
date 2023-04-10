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
        anim.SetBool("isRunning", player.isRunning);
    }

    void CheckAiming()
    {
        anim.SetBool("isAiming", player.isAiming);
        anim.SetInteger("WeaponH",1);
    }

}
