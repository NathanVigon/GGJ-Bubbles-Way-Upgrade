using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody rb;
    public float velocite;
    private bool canBouge;
    public float gravityScale = -9.81f;
    void Start() {
        rb = GetComponent<Rigidbody>();
        canBouge = true;
        StartCoroutine(Dead());
    }

    void FixedUpdate() {
        if (canBouge) {
            rb.velocity = new Vector3(velocite, rb.velocity.y, rb.velocity.z);
            rb.AddForce(new Vector3(0, gravityScale, 0) * 2, ForceMode.Acceleration);
        }
    }

    public void Elevator(bool value) {
        canBouge = value;
        if (!canBouge) {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator Dead() {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
}