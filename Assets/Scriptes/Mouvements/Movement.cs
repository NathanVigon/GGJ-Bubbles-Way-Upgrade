using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rb;
    public float velocite;
    public bool invertedGravity = false;
    private Vector3 gravity;
    private bool canBouge;

    // Start is called before the first frame update
    void Start() {
        gravity = Physics.gravity;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (invertedGravity) {
            rb.AddForce(6 * -gravity, ForceMode.Force);
        }

        if (canBouge) {
            rb.velocity = new Vector3(velocite, GetComponent<Rigidbody>().velocity.y);
        }
    }

    public void BougeFilsDe() {
        canBouge = true;
    }
}