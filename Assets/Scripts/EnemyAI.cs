using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    public Transform target;
    public float nextWaypointDistance = 3f;
    [SerializeField] private float startTimeBtwAttack;
    private bool canRun;
    [HideInInspector] public bool isAttacking;
    [SerializeField] private LayerMask whoIsPlayer;
    [SerializeField] private float checkRadius;
    [SerializeField] private GameObject bullet;
    public Transform shotPoint;
    public bool movingRight;
    public Transform groundCheck;
    [HideInInspector] public bool mustPatrol;
    [SerializeField] private float rayToGroundDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float patrolSpeed;

    public int damage;

    public Transform enemyGFX;
    private PlayerController player;
    [SerializeField] private EnemyGFX gFX;
    public Animator anim;

    private Path path;
    private int currentWaypoint = 0;
    public bool canFly;

    public Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .1f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        player = FindObjectOfType<PlayerController>();
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if (mustPatrol)
        {
            if(movingRight)
            {
                rb.velocity = new Vector2(+patrolSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-patrolSpeed * Time.deltaTime, rb.velocity.y);
            }
        }
        if (canRun)
        {
            mustPatrol = false;
        }
        else
        {
            mustPatrol = true;
        }
    }
    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        else
        {

        }
        Vector2 direction = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
        Vector2 force = direction * speed * Time.deltaTime;

        Vector2 velocity = rb.velocity;
        if (isAttacking == false)
        {
            if (canRun)
            {
                if (canFly)
                {
                    velocity = force;
                    rb.velocity = velocity;
                }
                else
                {
                    velocity.x = force.x;
                    rb.velocity = velocity;
                }
            }
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        canRun = Physics2D.OverlapCircle(transform.position, checkRadius, whoIsPlayer);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, rayToGroundDistance, whatIsGround);
        if(mustPatrol)
        {
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = true;
                }
            }
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void OnEnemyAttack()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        gFX.timeBtwAttack = startTimeBtwAttack;
    }
}
