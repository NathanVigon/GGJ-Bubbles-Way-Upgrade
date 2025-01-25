using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulle : MonoBehaviour {
    [SerializeField] private float bounciness;

    private void OnTriggerEnter(Collider other) {
        float tempBounciness = bounciness;
        if (other.gameObject.transform.position.x < this.gameObject.transform.position.x) {
            tempBounciness *=
                1 + (this.gameObject.transform.position.x - other.gameObject.transform.position.x) * 0.8f;
        }

        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
            other.gameObject.GetComponent<Rigidbody>().velocity.x,
            (-other.gameObject.GetComponent<Rigidbody>().velocity.y) * tempBounciness);
    }
}