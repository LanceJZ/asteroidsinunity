using System.Collections;
using UnityEngine;

public class Shot : MonoBehaviour
{
    // Use this for class variables.

    public float shotLife;
    public float shotSpeed;

    private float shotTimer;
    private CheckBounds checkBounds;
    private Player player;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        shotLife = 1.35f;
        shotSpeed = 300f;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        checkBounds = new CheckBounds(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(),
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());

        shotTimer = Time.time + shotLife;
    }

    // Update is called once per frame.
    private void Update()
    {
        GetComponent<Rigidbody>().MovePosition(checkBounds.CheckOffScreen(transform.position));

        if (Time.time > shotTimer)
        {
            if (player != null)
                player.ShotDone();

            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    // Update is called by physics.
    private void FixedUpdate()
    {
    }

    // When Collider is triggered by other.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Respawn")
            return;

        if (tag == "PlayerShot" && other.tag == "Player")
            return;

        if (tag == "UFOShot" && other.tag == "UFO")
            return;

        if (tag == "UFOShot" && other.tag == "PlayerShot")
            return;

        GetComponent<Collider>().enabled = false;

        if (player != null)
            player.ShotDone();

        Destroy(gameObject.transform.parent.gameObject);
    }

    // When object other leaves Collider.
    private void OnTriggerExit(Collider other)
    {
    }

    public void SetUp(Vector3 Position, Quaternion Rotation, Player playerReference)
    {
        player = playerReference;
        transform.position = Position;
        transform.rotation = Rotation;
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shotSpeed);
    }

    public void SetUp(Vector3 Position, Vector3 Velocity)
    {
        transform.position = Position;
        GetComponent<Rigidbody>().velocity = Velocity;
    }
}