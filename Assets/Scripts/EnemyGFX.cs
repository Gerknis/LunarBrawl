using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGFX : MonoBehaviour
{
    [HideInInspector] public float timeBtwAttack;
    private Animator anim;
    [SerializeField] private EnemyAI enemyAI;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("attack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
            enemyAI.isAttacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAI.isAttacking = false;
        }
    }
    public void EnemyAttack()
    {
        enemyAI.OnEnemyAttack();
    }
    public void Update()
    {
        if (enemyAI.mustPatrol == false)
        {
            if (transform.position.x < enemyAI.target.position.x)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (transform.position.x > enemyAI.target.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }      
        if (enemyAI.isAttacking == false)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
}