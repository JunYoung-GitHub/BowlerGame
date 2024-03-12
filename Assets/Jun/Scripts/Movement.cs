using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Movement: MonoBehaviour {
    [SerializeField] Vector3 vector;
    [SerializeField] private float _cubeSpeed;
    [SerializeField] private float _normalSpeed = 100f;
    [SerializeField] private float _sprintSpeed = 500f;
    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private Rigidbody _cubeRb;

    private void Awake() {
        Debug.Log("Awake method flag");
        _cubeRb= GetComponent<Rigidbody>(); 
    }
    // Start is called before the first frame update
    void Start() {

    }

    private void OnEnable() {


    }

    // Update is called once per frame
    //Fixed update has predefined time (check Project settings -> time)
    void FixedUpdate() {
        MovePlayer();
    }

    private void MovePlayer() {
        //Movement
        //transform.Translate(1 * Input.GetAxis("Horizontal"), 0 ,0); <- bad way
        //Better way for movement
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _cubeSpeed = _sprintSpeed;
        }
        else
            _cubeSpeed = _normalSpeed;

       /* if (Input.GetKeyDown(KeyCode.D)) {
            _cubeRb.AddForce(Vector3.right * _normalSpeed, ForceMode.Force);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            _cubeRb.AddForce(Vector3.left * _normalSpeed, ForceMode.Force);
        }
        else if (Input.GetKeyDown(KeyCode.W)) {
            _cubeRb.AddForce(Vector3.forward * _normalSpeed, ForceMode.Force);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            _cubeRb.AddForce(Vector3.back * _normalSpeed, ForceMode.Force);
        }*/
        if (Input.GetKeyDown(KeyCode.Space)) {
            _cubeRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * _cubeSpeed * Time.deltaTime);//Time.deltatime ensures you consistently move the same amount of units per second
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * _cubeSpeed * Time.deltaTime);
    }
}
