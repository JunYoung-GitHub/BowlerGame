using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
    Vector3 lastPosition;
    Quaternion lastRotation;//Represents rotations
    int framesWithoutMoving;

    Vector3 startingPosition;
    Quaternion startingRotation;
    Rigidbody pinRB;

    private void Awake() {
        //sets starting position/rotation at the very start
        startingPosition= transform.position;
        startingRotation= transform.rotation;
        pinRB = this.GetComponent<Rigidbody>();
    }

    int[] pins = new int[3] {3,5,5};

    //getter and setter
    public bool DidPinFall { get; private set; }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pit")) {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.PinKnockedDown();
            Debug.Log("Pin has entered the pit");
            Destroy(this.gameObject);
            return;
        }
        else if(other.gameObject.CompareTag("BowlingTrack")) {
            DidPinFall = true;
            Debug.Log("Pin has Fallen");
        }
    }
    public bool DidPinMove() {
        var didPinMove = (transform.position - lastPosition).magnitude > 0.0001f || Quaternion.Angle(transform.rotation, lastRotation) > 0.0001f;
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        framesWithoutMoving = didPinMove ? 0 : framesWithoutMoving + 1;


        return framesWithoutMoving <= 10;
    }

    //Reset the pins after they are knocked down taken from awake mothod
    public void ResetPosition() {
        //Uses RB as it has been affected by physics
        pinRB.position = startingPosition;
        pinRB.rotation = startingRotation;

        //so the pins dont go flying off again
        pinRB.velocity = Vector3.zero;
        pinRB.angularVelocity = Vector3.zero;

        //Just to make sure that the position is as accurate as possible as floats are super weird
        lastPosition = startingPosition;
        lastRotation = startingRotation;
    }
}
