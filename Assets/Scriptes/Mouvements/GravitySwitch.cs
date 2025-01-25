using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        Movement movement = other.GetComponent<Movement>();
        if (movement != null) {
            movement.gravityScale *= -1;
        }
    }
}