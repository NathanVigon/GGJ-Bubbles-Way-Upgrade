using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulle : MonoBehaviour {
    [SerializeField] private float bounciness;

    private void OnTriggerEnter(Collider other) {
        Movement movement = other.GetComponent<Movement>();
        if (movement != null) {
            float tempBounciness = bounciness;
            if (other.transform.position.x < transform.position.x) {
                tempBounciness *= 1 + (transform.position.x - other.transform.position.x) * 0.8f;
            }

            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
                other.gameObject.GetComponent<Rigidbody>().velocity.x,
                (-other.gameObject.GetComponent<Rigidbody>().velocity.y) * tempBounciness);
        }
    }
}