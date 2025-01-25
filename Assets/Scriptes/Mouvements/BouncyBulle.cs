using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulle : MonoBehaviour {
    [SerializeField] private float bounciness;
    
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
            other.gameObject.GetComponent<Rigidbody>().velocity.x,
            (-other.gameObject.GetComponent<Rigidbody>().velocity.y) * bounciness);
    }
}