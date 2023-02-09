using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [SerializeField] GameObject projectileImpactWall;
    [SerializeField] float projectileSpeed = 5f;

    [SerializeField] int damageAmount = 10;

    private Rigidbody2D projectileRigidbody;

    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        projectileRigidbody.velocity = transform.right * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(projectileImpactWall.transform, transform.position, transform.rotation);

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);
        }

        Destroy(gameObject);
    }
}
