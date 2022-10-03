using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public Transform card;
    private bool isColliding;
    public float checkRadius;
    public LayerMask whatIsSolid;
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
        isColliding = Physics2D.OverlapCircle(card.position, checkRadius, whatIsSolid);
        if (isColliding)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }
}
