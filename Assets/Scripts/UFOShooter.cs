using UnityEngine;
using System.Collections;

public class UFOShooter : MonoBehaviour
{
    // Use this for class variables.

    public AudioClip shotClip;
    public GameObject shot;
    public float angle;

    // Use this for initialization when scene loads.
    void Awake()
    {
    }

    // Use this for initialization when game starts.
    void Start()
    {
    }

    // Update is called once per frame.
    void Update()
    {
    }

    // Update is called by physics.
    void FixedUpdate()
    {
    }

    // When Collider is triggered by other.
    void OnTriggerEnter(Collider other)
    {
    }

    // When object other leaves Collider.
    void OnTriggerExit(Collider other)
    {
    }

    public void Shoot(float Angle)
    {
        angle = Angle;
        GameObject thisShotObj = Instantiate(shot);
        thisShotObj.GetComponentInChildren<AudioSource>().clip = shotClip;
        thisShotObj.GetComponentInChildren<AudioSource>().loop = false;
        thisShotObj.GetComponentInChildren<AudioSource>().Play();
        thisShotObj.transform.GetChild(0).tag = "UFOShot";
        Vector3 velocity = Vector3.zero;
        float sinRot = Mathf.Sin(Angle);
        float cosRot = Mathf.Cos(Angle);
        velocity.x = cosRot * 5;
        velocity.y = sinRot * 5;
        thisShotObj.GetComponentInChildren<Shot>().SetUp(transform.position, velocity);
    }
}