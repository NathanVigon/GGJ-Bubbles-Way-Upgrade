using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    [SerializeField] private float jumpPower;

    private void OnTriggerEnter(Collider other) {
        other.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    }
}