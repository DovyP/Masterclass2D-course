using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    [SerializeField] float playerKeepChaseRange;

    private bool isChasing;

    private Vector3 directionToMoveIn;

    private Transform playerToChase;


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        playerToChase = FindObjectOfType<PlayerController>().transform;
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerKeepChaseRange);
    }
}
