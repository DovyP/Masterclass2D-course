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

        //projectile rotation
        float rot = Mathf.Atan2(-playerDirection.y, -playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
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
