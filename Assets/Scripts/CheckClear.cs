using System.Collections;
using UnityEngine;

public class CheckClear : MonoBehaviour
{
    // Use this for class variables.
    public bool isClear;

    public int objCount;
    private float timer;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        objCount = 0;
        isClear = false;
        timer = Time.time + 0.25f;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
    }

    // Update is called once per frame.
    private void Update()
    {
        if (Time.time > timer)
        {
            isClear = true;
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

    private void OnTriggerStay(Collider other)
    {
        timer = Time.time + 0.1f;
    }
}