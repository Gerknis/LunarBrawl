using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaraGFX : MonoBehaviour
{
    public PlayerController player;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(player.moveInput > 0 && player.facingRight == false)
        {
            Flip();
        }
        if (player.moveInput < 0 && player.facingRight == true)
        {
            Flip();
        }

        if (player.isGrounded)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);

        }
        if (player.moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }
    void Flip()
    {
        player.facingRight = !player.facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        if (player.moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (player.moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
