using System.Collections;
using UnityEngine;

public class UFOControl : MonoBehaviour
{
    // Use this for class variables.

    public GameObject theLargeUFO;
    public GameObject theSmallUFO;
    public int wave;

    private GameObject spawnedUFO;
    private float spawnTimer;
    private float spawnWait;
    public bool isSpawnedUFO;
    public int spawnCounter;
    public float spawnPercent;
    public float spawnSeed;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        spawnWait = 25.15f;
        spawnSeed = 0.985f;
        isSpawnedUFO = false;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Time.time > spawnTimer && !isSpawnedUFO)
        {
            SpawnUFO();
            ResetTimer();
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

    private void ResetTimer()
    {
        spawnTimer = Time.time + Random.Range(Mathf.Clamp((spawnWait - (wave * 0.25f)), 5.75f, 15.15f),
            Mathf.Clamp((spawnWait - (wave * 0.25f)), 15.75f, 25.15f));
    }

    private void SpawnUFO()
    {
        spawnPercent = Mathf.Pow(spawnSeed, (spawnCounter * 15) / (wave + 1));

        if (Random.Range(1, 100) < spawnPercent * 100)
        {
            SpawnLarge();
        }
        else
        {
            SpawnSmall();
        }
    }

    private void SpawnLarge()
    {
        spawnedUFO = Instantiate(theLargeUFO);

        Debug.Log("Large UFO spawned.");
    }

    private void SpawnSmall()
    {
        spawnedUFO = Instantiate(theSmallUFO);
        spawnedUFO.GetComponentInChildren<AimedUFOShooter>().wave = wave;
        spawnedUFO.GetComponentInChildren<UFO>().points = 1000;
        spawnedUFO.GetComponentInChildren<UFO>().vectorWait = 2.5f;
        spawnedUFO.GetComponentInChildren<UFO>().speed = 1.0f;
        spawnedUFO.GetComponentInChildren<RandomUFOShooter>().aimed = true;

        Debug.Log("Small UFO spawned.");
    }

    public void UFOSpawned()
    {
        spawnCounter++;
        isSpawnedUFO = true;
    }

    public void UFOHit()
    {
        ResetTimer();
        isSpawnedUFO = false;
    }
}