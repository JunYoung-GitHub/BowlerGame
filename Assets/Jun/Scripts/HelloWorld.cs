using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    public string message = "Error Flag";
    public Transform ballTransform;
    public bool hasGameStarted;
    public int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth > 0 && hasGameStarted) {
            Debug.Log("Game Started");
        }

        else if (playerHealth > 0) {
            Debug.Log("Alive but game has not started");
        }

        else
            Debug.Log("Please Start game");

    }

    // Update is called once per frame
    void Update()
    {
        ballTransform.Rotate(0, 0, 0);
    }
}
