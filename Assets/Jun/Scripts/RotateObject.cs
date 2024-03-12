using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _ballRb;
    // Start is called before the first frame update
    void Start()
    {
        _ballRb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //_ballRb.AddForce(0, 0, 100f, ForceMode.Acceleration);
        transform.Rotate(0, 0, 1);
    }
}
