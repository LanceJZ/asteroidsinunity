using System.Collections;
using UnityEngine;

public class AimedUFOShooter : MonoBehaviour
{
    // Use this for class variables.

    public int wave;
    public float shotAimChance;
    public float angle;
    private Transform playerPosition;
    private float shotTimer;
    private float shotWait;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        shotWait = 2.5f;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        shotTimer = GetComponent<RandomUFOShooter>().ResetShotTimer(shotWait);

        if (GameObject.FindGameObjectWithTag("Player") != null)
            playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Time.time > shotTimer)
        {
            Shoot();
            shotTimer = GetComponent<RandomUFOShooter>().ResetShotTimer(shotWait);
        }
    }

    // Update is called by physics.
    private void FixedUpdate()
    {
    }

    // When Collider is triggered by other.
    private void OnTriggerEnter(Collider other)
    {
    }

    // When object other leaves Collider.
    private void OnTriggerExit(Collider other)
    {
    }

    private void Shoot()
    {
        shotAimChance = Mathf.Pow(0.75f, (wave + 1) * 10);

        if (Random.Range(1, 100) < shotAimChance * 100 || playerPosition == null)
        {
            GetComponent<RandomUFOShooter>().ShootRandom();
            Debug.Log("Random Shot");
        }
        else if (playerPosition != null)
        {
            AimAtPlayer();
            Debug.Log("Aimed Shot");
        }
    }

    private void AimAtPlayer()
    {
        float pY = playerPosition.position.y;
        float y = transform.position.y;
        float pX = playerPosition.position.x;
        float x = transform.position.x;

        angle = Mathf.Atan2(pY - y, pX - x);

        GetComponent<UFOShooter>().Shoot(angle);
    }
}