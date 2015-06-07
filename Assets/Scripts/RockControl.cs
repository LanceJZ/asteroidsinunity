using System.Collections;
using UnityEngine;

public class RockControl : MonoBehaviour
{
    // Use this for class variables.
    public GameObject rock;

    private int wave;
    private int rockCount;
    private int rocksHit;

    // Use this for initialization when scene loads.
    private void Awake()
    {
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        SpawnNewWave();
    }

    // Update is called once per frame.
    private void Update()
    {
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

    private void SpawnNewWave()
    {
        for (int i = 0; i < 4 + wave; i++)
        {
            Rock thisRock = Instantiate(rock).GetComponent<Rock>();
            thisRock.Setup(new Vector3(20, 20, 20), Vector3.zero, 20, "Large");
            thisRock.SetTumble(0.5f, 1.25f);
            rockCount++;
            rocksHit = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().pitch = 0.1f;
        }
    }

    private void SpawnMedRocks(Vector3 Position)
    {
        for (int i = 0; i < 2; i++)
        {
            Rock thisRock = Instantiate(rock).GetComponent<Rock>();
            thisRock.Setup(new Vector3(10, 10, 10), Position, 50, "Med");
            thisRock.SetTumble(0.8f, 1.75f);
            rockCount++;
        }
    }

    private void SpawnSmallRocks(Vector3 Position)
    {
        for (int i = 0; i < 2; i++)
        {
            Rock thisRock = Instantiate(rock).GetComponent<Rock>();
            thisRock.Setup(new Vector3(5, 5, 5), Position, 100, "Small");
            thisRock.SetTumble(1.1f, 2.15f);
            rockCount++;
        }
    }

    public void RockHit(string Size, Vector3 Position)
    {
        rockCount--;
        rocksHit++;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().pitch = Mathf.Clamp(Mathf.Sqrt(rocksHit + 1) * 0.15f, 0.1f, 2);

        if (Size == "Med")
        {
            SpawnSmallRocks(Position);
            return;
        }

        if (Size == "Large")
        {
            SpawnMedRocks(Position);
            return;
        }

        if (rockCount == 0)
        {
            wave++;
            SpawnNewWave();
            GetComponent<UFOControl>().wave = wave;
        }
    }
}