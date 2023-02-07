using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileController : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 5f;

    private Rigidbody2D projectileRigidbody;

    [SerializeField] GameObject projectileImpactWall;

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
        Destroy(gameObject);
    }
}
