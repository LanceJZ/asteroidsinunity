using UnityEngine;
using System.Collections;

public class Ships : MonoBehaviour
{
    // Use this for class variables.

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

    public void Distroy()
    {
        GameObject.Destroy(gameObject);
    }
}