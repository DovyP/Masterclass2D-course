using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float enemySpeed;
    private Rigidbody2D enemyRigidbody;

    [SerializeField] float playerChaseRange;
    private Vector3 directionToMoveIn;

    private Transform playerToChase;

    void Start()
    {
        playerToChase = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange)
        {
            Debug.Log("Player is in chase range");
        }
        else
        {
            Debug.Log("Player is out of chase range");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
    }
}
