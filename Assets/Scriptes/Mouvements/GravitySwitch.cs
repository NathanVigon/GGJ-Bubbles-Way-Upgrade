using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour {
    public Movement movement;
    private void OnTriggerEnter(Collider other) {
        movement = other.GetComponent<Movement>();
        if (movement != null) {
            if (!movement.invertedGravity) {
                movement.invertedGravity = true;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            } else {
                movement.invertedGravity = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}