using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Use this for class variables.

    public AudioClip shotClip;
    public AudioClip thrustClip;
    public GameObject shot;
    public GameObject explodeFX;
    public float shotDelay = 0.25f;
    public float maxSpeed = 15f;
    public float thrustAmount = 4;
    public float rotateAmount = 4;

    private float rotation;
    private float timer;
    private float shotTimer;
    private int shotNumber;
    private CheckBounds checkBounds;
    private Game game;

    // Use this for initialization when scene loads.
    private void Awake()
    {
    }

    // Use this for initialization when game starts.
    private void Start()
    {
        checkBounds = new CheckBounds(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(),
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>());

        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (shotNumber < 4)
            {
                GameObject thisShotObj = Instantiate(shot);
                thisShotObj.GetComponentInChildren<AudioSource>().clip = shotClip;
                thisShotObj.GetComponentInChildren<AudioSource>().loop = false;
                thisShotObj.GetComponentInChildren<AudioSource>().Play();
                thisShotObj.transform.GetChild(0).tag = "PlayerShot";
                Vector3 gun = gameObject.transform.GetChild(0).transform.position;
                thisShotObj.GetComponentInChildren<Shot>().SetUp(gun, transform.rotation, this);
                shotNumber++;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -rotateAmount, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, rotateAmount, 0));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thrustAmount, ForceMode.Acceleration);

            GetComponentInChildren<ParticleSystem>().Play();

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = thrustClip;
                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            GetComponent<AudioSource>().Stop();
            GetComponentInChildren<ParticleSystem>().Stop();
        }

        if (GetComponent<Rigidbody>().velocity.x > maxSpeed)
            GetComponent<Rigidbody>().velocity = new Vector3(maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);

        if (GetComponent<Rigidbody>().velocity.x < -maxSpeed)
            GetComponent<Rigidbody>().velocity = new Vector3(-maxSpeed, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);

        if (GetComponent<Rigidbody>().velocity.y > maxSpeed)
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, maxSpeed, GetComponent<Rigidbody>().velocity.z);

        if (GetComponent<Rigidbody>().velocity.y < -maxSpeed)
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -maxSpeed, GetComponent<Rigidbody>().velocity.z);

        GetComponent<Rigidbody>().MovePosition(checkBounds.CheckOffScreen(transform.position));
    }

    // Update is called by physics.
    private void FixedUpdate()
    {
    }

    // When Collider is triggered by other.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot" || other.tag == "Respawn")
            return;

        GetComponent<Collider>().enabled = false;
        GetComponent<AudioSource>().Stop();
        game.PlayerHit();

        Instantiate(explodeFX, transform.position, Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }

    // When object other leaves Collider.
    private void OnTriggerExit(Collider other)
    {
    }

    public void ShotDone()
    {
        shotNumber--;
    }
}