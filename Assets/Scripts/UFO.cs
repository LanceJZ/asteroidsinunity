using System.Collections;
using UnityEngine;

public class UFO : MonoBehaviour
{
    // Use this for class variables.

    public int points;
    public GameObject explodeFX;
    public float vectorWait;
    public float speed;
    public Vector3 position;
    private Score score;
    private CheckBounds checkBounds;
    private Vector3 velocity = Vector3.zero;
    private float vectorTimer;
    private bool spawnNotified;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        points = 200;
        vectorWait = 3.0f;
        speed = 0.7f;
        spawnNotified = false;
        position = Vector3.zero;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        score = GameObject.FindGameObjectWithTag("GameController").GetComponent<Score>(); //Give score to player when player hits.
        checkBounds = new CheckBounds(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(),
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());

        if (Random.Range(0, 10) > 5)
        {
            position.x = checkBounds.gameBounds.x - 0.2f;
            velocity.x = -speed;
        }
        else
        {
            position.x = -checkBounds.gameBounds.x + 0.2f;
            velocity.x = speed;
        }

        position.y = Random.Range(-checkBounds.gameBounds.y, checkBounds.gameBounds.y);
        transform.position = position;
        GetComponent<Rigidbody>().MovePosition(position);
        ChangeVector();
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 5, 0);
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Time.time > vectorTimer)
        {
            ChangeVector();
        }

        if (transform.position.x > checkBounds.gameBounds.x || transform.position.x < -checkBounds.gameBounds.x)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        GetComponent<Rigidbody>().MovePosition(checkBounds.CheckOffScreen(transform.position));

        if (!spawnNotified)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<UFOControl>().UFOSpawned();
            spawnNotified = true;
        }
    }

    // Update is called by physics.
    private void FixedUpdate()
    {
    }

    private void LastUpdate()
    {
    }

    // When Collider is triggered by other.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "UFOShot" || other.tag == "Respawn")
            return;

        GetComponent<Collider>().enabled = false;

        if (other.tag == "Player" || other.tag == "PlayerShot")
        {
            score.AddScore(points);
        }

        Instantiate(explodeFX, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }

    // When object other leaves Collider.
    private void OnTriggerExit(Collider other)
    {
    }

    private void ResetVectorTimer()
    {
        vectorTimer = vectorWait + Time.time;
    }

    private void ChangeVector()
    {
        int var = Random.Range(0, 15);

        if (var < 5)
        {
            velocity.y = speed;
        }
        else if (var < 10)
        {
            velocity.y = 0.0f;
        }
        else
        {
            velocity.y = -speed;
        }

        GetComponent<Rigidbody>().velocity = velocity;
        ResetVectorTimer();
    }

    private void OnDestroy()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<UFOControl>().UFOHit();
    }
}