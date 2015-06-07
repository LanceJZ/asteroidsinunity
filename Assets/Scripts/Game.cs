using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Use this for class variables.

    public Camera mainCamera;
    public GameObject player;
    public GameObject checkClearObj;
    public GameObject playerShip;

    private CheckClear checkClear;
    private GameObject checkClearInt;
    private List<GameObject> ships = new List<GameObject>();
    private bool gameOver;
    private bool gameStart;
    private bool spawnPlayer;

    // Use this for initialization when scene loads.
    private void Awake()
    {
        gameOver = false;
        spawnPlayer = true;

        for (int i = 0; i < 4; i++)
            AddPlayerShip();
    }

    // Use this for initialization when game starts.
    private void Start()
    {
    }

    // Update is called once per frame.
    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else
        {
            if (spawnPlayer)
                SpawnPlayer();
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

    private void SpawnClear()
    {
        checkClearInt = Instantiate(checkClearObj);
        checkClear = checkClearInt.GetComponent<CheckClear>();
    }

    private void SpawnPlayer()
    {
        if (checkClearInt != null)
        {
            if (checkClear.isClear)
            {
                Instantiate(player);
                spawnPlayer = false;
                DestroyObject(checkClearInt);
            }
        }
        else
        {
            Instantiate(player);
            spawnPlayer = false;
        }
    }

    public void AddPlayerShip()
    {
        ships.Add(Instantiate(playerShip));
        Vector3 position = Vector3.zero;
        position.y = 5.15f;
        position.x = -7.35f + ((ships.Count - 1) * 0.42f);
        ships[ships.Count - 1].transform.position = position;
    }

    private void RemovePlayerShip()
    {
        ships[ships.Count - 1].GetComponent<Ships>().Distroy();
        ships.RemoveAt(ships.Count - 1);
    }

    public void PlayerHit()
    {
        RemovePlayerShip();
        SpawnClear();

        if (ships.Count > 0)
        {
            spawnPlayer = true;
        }
        else
        {
            gameOver = true;
            Text scroeText = gameObject.GetComponent<Text>();
            scroeText.text = scroeText.text + "     Game Over 'R' to restart game.";
        }
    }
}