using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbitBowlingBall : MonoBehaviour
{
    [SerializeField] GameObject bowlingBall;
    [SerializeField] float rotateSpeed;
    [SerializeField] Vector3 vector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(bowlingBall.transform.position, vector, rotateSpeed * Time.deltaTime);
    }
}
