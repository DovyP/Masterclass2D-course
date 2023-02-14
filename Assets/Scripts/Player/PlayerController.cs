using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;

    [SerializeField] Transform weaponsArm;
    private Camera mainCamera;

    [SerializeField] int movementSpeed;

    private Vector2 movementInput;

    private Animator playerAnimator;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [SerializeField] bool isWeaponAutomatic;
    [SerializeField] float timeBetweenShots;
    private float shotCounter = 0f;

    // Dashing
    private float currentMovementSpeed;
    private bool canDash;

    [SerializeField] float dashSpeed = 10f, dashLength = .25f, dashCooldown = 1f;
    bool isDashing;

    // Dash trail
    [SerializeField] TrailRenderer trailRenderer;

    // Test stuff

    List<GameObject> trailParts = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        playerAnimator = GetComponent<Animator>();

        currentMovementSpeed = movementSpeed;
        canDash = true;
        isDashing = false;
    }

    void Update()
    {
        PlayerMoving();
        PointingGunAtMouse();
        AnimatingThePlayer();
        PlayerShooting();

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            currentMovementSpeed = dashSpeed;
            canDash = false;
            isDashing = true;
            //trailRenderer.emitting = true;
            // counters
            StartCoroutine(DashCooldownCounter());
            StartCoroutine(DashLengthCounter());
            StartCoroutine(GhostingEffect());
        }
    }

    private IEnumerator DashCooldownCounter()
    {
        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
    }

    private IEnumerator DashLengthCounter()
    {
        yield return new WaitForSeconds(dashLength);

        currentMovementSpeed = movementSpeed;
        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void AnimatingThePlayer()
    {
        //animation for idling/running
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    private void PointingGunAtMouse()
    {
        //weapon rotation
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        //flip player and weapon sprites depending on the mouse position
        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponsArm.localScale = Vector3.one;
        }
    }

    private void PlayerShooting()
    {
        //shooting
        if (Input.GetMouseButtonDown(0) && !isWeaponAutomatic)
        {
            Instantiate(projectile, firePoint.position, firePoint.rotation);
        }

        if (Input.GetMouseButton(0) && isWeaponAutomatic)
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void PlayerMoving()
    {
        // inputs
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        //movement
        movementInput.Normalize();
        playerRigidbody.velocity = movementInput * currentMovementSpeed;
    }


    // test stuff
    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        trailPartRenderer.sortingLayerName = "Player";
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale; // We forgot about this line!!!
        trailParts.Add(trailPart);

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        Destroy(trailPart, 0.5f); // replace 0.5f with needed lifeTime
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color = Color.black;
        color.a -= 0.6f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }

    IEnumerator GhostingEffect()
    {
        //SpawnTrailPart();
        InvokeRepeating("SpawnTrailPart", 0, 0.05f); // replace 0.2f with needed repeatRate
        yield return new WaitForSeconds(dashLength);
        CancelInvoke("SpawnTrailPart");
    }
}
