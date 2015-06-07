using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Use this for class variables.

    public AudioClip explode;
    public GameObject explodeFX;

    private string size;
    private int points;
    private Score score;
    private CheckBounds checkBounds;
    private RockControl rockControl;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        score = GameObject.FindGameObjectWithTag("GameController").GetComponent<Score>(); //Give score to player when player hits.
        checkBounds = new CheckBounds(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(),
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());
        rockControl = GameObject.FindGameObjectWithTag("GameController").GetComponent<RockControl>();
    }

    // Use this for initialization when game starts.
    private void Start()
    {
    }

    // Update is called once per frame.
    private void Update()
    {
        GetComponent<Rigidbody>().MovePosition(checkBounds.CheckOffScreen(transform.position));
    }

    // Update is called by physics.
    private void FixedUpdate()
    {
    }

    // When Collider is triggered by other.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rock" || other.tag == "Respawn")
            return;

        GetComponent<Collider>().enabled = false;

        if (other.tag == "Player" || other.tag == "PlayerShot")
        {
            score.AddScore(points);
        }

        rockControl.RockHit(size, transform.position);
        Instantiate(explodeFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // When object other leaves Collider.
    private void OnTriggerExit(Collider other)
    {
    }

    public void SetTumble(float tumble, float speed)
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;

        float rockSpeed = Random.Range(speed * 0.5f, speed);
        float angle = Random.Range(0, 6.28318530718f);
        Vector3 velocity = Vector3.zero;
        float sinRot = Mathf.Sin(angle);
        float cosRot = Mathf.Cos(angle);
        velocity.x = cosRot * rockSpeed;
        velocity.y = sinRot * rockSpeed;

        GetComponent<Rigidbody>().velocity = velocity;
    }

    public void Setup(Vector3 scale, Vector3 position, int Points, string Size)
    {
        transform.localScale = scale;
        points = Points;
        size = Size;

        if (position == Vector3.zero)
        {
            if (Random.Range(1, 10) > 5)
                position.x = checkBounds.gameBounds.x;
            else
                position.x = -checkBounds.gameBounds.x;

            position.y = Random.Range(-checkBounds.gameBounds.y, checkBounds.gameBounds.y);
            position.z = 0;
        }

        transform.position = position;
    }
}