using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {
    //reference animator component
    Animator _amin;

    private void awake() {
        _amin = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.K)) {
            _amin.SetTrigger("Wave");
        }
    }
}
