using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class FollowTarget : MonoBehaviour {
    public Transform playerPosition;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (playerPosition == null) {
            return;
        }

        transform.position = playerPosition.position + offset;
        //Allows camera to continuously look at the 
        transform.LookAt(playerPosition);
    }
}
