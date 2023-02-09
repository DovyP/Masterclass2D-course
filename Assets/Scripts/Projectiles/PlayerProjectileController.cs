using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [SerializeField] GameObject projectileImpactWall;
    [SerializeField] GameObject[] damageEffects;

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
        if (collision.CompareTag("Enemy"))
        {
            int selectedSplatter = Random.Range(0, damageEffects.Length);

            Instantiate(damageEffects[selectedSplatter], transform.position, transform.rotation);
            collision.GetComponent<EnemyController>().DamageEnemy(damageAmount);
        }
        else
        {
            Instantiate(projectileImpactWall.transform, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
