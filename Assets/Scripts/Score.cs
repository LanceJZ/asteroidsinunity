using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Use this for class variables.

    public int newShip;
    public AudioClip newShipClip;

    private int score;
    private int highScore;
    private int nextNewShip;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        newShip = 10000;
        nextNewShip = newShip;
    }

    // Use this for initialization when game starts.
    private void Start()
    {
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

    private void OnTriggerStay(Collider other)
    {
    }

    public void AddScore(int Points)
    {
        score += Points;
        Text scroeText = gameObject.GetComponent<Text>();
        scroeText.text = "Score: " + score + "  Arrow keys to move and rotate, Left CTRL to fire.";

        if (score > highScore)
            highScore = score;

        if (score > nextNewShip)
        {
            nextNewShip = score + newShip;
            gameObject.GetComponent<Game>().AddPlayerShip();
            GetComponent<AudioSource>().clip = newShipClip;
            GetComponent<AudioSource>().Play();
        }
    }
}