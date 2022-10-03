using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerController playerController = hitInfo.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.health -= damage;
        }
    }
}
