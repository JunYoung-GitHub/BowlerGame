using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInactive : MonoBehaviour
{
    [SerializeField] private GameObject[] _cubes;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log("Inactive cubes");
            foreach (var cubes in _cubes) {
                cubes.SetActive(false);
            }
        }
       
    }
}
