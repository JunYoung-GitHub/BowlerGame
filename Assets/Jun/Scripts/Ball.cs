using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Auto adds whichever object you assign this to always requires the component
[RequireComponent(typeof(Rigidbody))]
//Movement of the ball
public class Ball : MonoBehaviour {
    Vector3 lastPosition;
    Quaternion lastRotation;//Represents rotations
    int framesWithoutMoving;

    AudioSource ballAudioSource;
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Pin")) {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.PlayStrikeSoundEffect();
            Debug.Log("Pin hit!");
            //var means it could be any type


        }


        if (collision.gameObject.CompareTag("BowlingTrack")) {
            ballAudioSource.Play();
            Debug.Log("Ball hit bowling track");
            return;

        }

        Debug.Log("Started Colliding with: " + collision.gameObject.name);
    }

    private void Awake() {
        ballAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pit")) {
            Debug.Log("Gutter BALL!");
            var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gameManager.BallKnockedDown();
            Destroy(this.gameObject, 1f);
        }
    }
    public bool DidBallMove() {
        var didBallMove = (transform.position - lastPosition).magnitude > 0.0001f || Quaternion.Angle(transform.rotation, lastRotation) > 0.0001f;
        lastPosition = transform.position;
        lastRotation = transform.rotation;

        /*if(didBallMove) {
            framesWithoutMoving = 0;

        }
        else {
            framesWithoutMoving += 1;
        }*/

        framesWithoutMoving = didBallMove ? 0 : framesWithoutMoving + 1;


        return framesWithoutMoving <= 10;
    }
}
