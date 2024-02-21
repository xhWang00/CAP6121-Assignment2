using System.Collections;
using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float speed = 2.0f;
    public float moveTime = 3.0f;
    public float waitTime = 1.0f;
    public float stopDistance = 20.0f; // The distance at which the object should stop moving
    public GameObject projectilePrefab; // The prefab to shoot
    public float projectileSpeed = 10.0f; // The speed of the projectile
    public float projectileLifetime = 5.0f; // The lifetime of the projectile

    private Vector3 targetPosition;
    private bool isMoving = true;
    private float shootTimer = 0f; // Timer for shooting
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Patrol());
    }

    void Update()
    {
        // Decrease the shoot timer
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            // Check the distance to the main camera
            float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            if (distanceToCamera <= stopDistance)
            {
                isMoving = false;
                transform.LookAt(Camera.main.transform);
                if (shootTimer <= 0f)
                {
                    ShootProjectile(); // Shoot the projectile when the enemy stops moving
                    shootTimer = 3f; // Reset the timer
                }
            }
            else
            {
                isMoving = true;
            }

            if (isMoving)
            {
                // Generate a new target position
                targetPosition = GetRandomPosition();

                // Calculate the move direction
                Vector3 direction = (targetPosition - transform.position).normalized;

                // Rotate the object to face the target direction
                transform.rotation = Quaternion.LookRotation(direction);

                // Move for a certain amount of time
                for (float t = 0; t < moveTime; t += Time.deltaTime)
                {
                    transform.position += direction * speed * Time.deltaTime;
                    yield return null;
                }

                // Wait for a certain amount of time
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                // Wait for a certain amount of time before checking the distance to the camera again
                yield return new WaitForSeconds(waitTime);
            }
        }
    }


    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    private void ShootProjectile()
    {
        // Instantiate the projectile at the enemy's position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        audioSource.Play();

        // Calculate the direction towards the camera
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        // Start the coroutine to move the projectile
        StartCoroutine(MoveProjectile(projectile, direction));
    }

    private IEnumerator MoveProjectile(GameObject projectile, Vector3 direction)
    {
        float startTime = Time.time;

        while (Time.time - startTime < projectileLifetime)
        {
            if (projectile == null)
            {
                yield return null;
            }

            else
            {
                // Move the projectile in the direction of the camera
                projectile.transform.position += direction * projectileSpeed * Time.deltaTime;
                yield return null;
            }

        }

        // Destroy the projectile after its lifetime has expired
        Destroy(projectile);
    }
}