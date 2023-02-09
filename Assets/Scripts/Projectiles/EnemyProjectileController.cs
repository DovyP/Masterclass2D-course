using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    private Vector3 playerDirection;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        playerDirection = player.position - transform.position;
        playerDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerDirection * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
        }
        
        Destroy(gameObject);
    }
}
