using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    [SerializeField] int enemyHealth = 100;

    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerKeepChaseRange;

    private bool isChasing;

    private Vector3 directionToMoveIn;

    private Transform playerToChase;

    private Animator enemyAnimator;

    //attack
    [SerializeField] bool meleeAttacker;

    [SerializeField] GameObject enemyProjectile;
    [SerializeField] Transform firePosition;
    [SerializeField] float shootingRange;

    [SerializeField] float timeBetweenShots;
    private bool readyToShoot;


    [SerializeField] GameObject deathSplatter;
    [SerializeField] GameObject damageEffect;


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<PlayerController>().transform;
        enemyAnimator = GetComponentInChildren<Animator>();

        readyToShoot = true;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            directionToMoveIn = playerToChase.position - transform.position;
            isChasing = true;
        }else if (isChasing && Vector3.Distance(transform.position, playerToChase.position) < playerKeepChaseRange)
        {
            directionToMoveIn = playerToChase.position - transform.position;
        }
        else
        {
            directionToMoveIn = Vector3.zero;
            isChasing = false;
        }

        directionToMoveIn.Normalize();
        enemyRigidbody.velocity = directionToMoveIn * enemySpeed;

        if(directionToMoveIn != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }

        if(playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if(Vector3.Distance(transform.position, playerToChase.position) < shootingRange && !meleeAttacker && readyToShoot)
        {
            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());
        }
    }

    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
        readyToShoot = true;
    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        //Instantiate(damageEffect, transform.position, transform.rotation);

        if(enemyHealth <= 0)
        {
            Instantiate(deathSplatter, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerKeepChaseRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
