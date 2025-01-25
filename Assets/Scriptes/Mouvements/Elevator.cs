using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float vitesseAscension;
    public float hauteur;
    public float velocite;
    public float leavingJumpPower;

    public GameObject elevator;
    public Vector3 initialPos;
    private bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        occupied = false;
        elevator = this.gameObject;
        GetComponent<BoxCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            GetComponent<BoxCollider>().enabled = false;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = new Vector3(0, vitesseAscension);
            GetComponent<Rigidbody>().velocity = new Vector3(0, vitesseAscension);
            StartCoroutine(stopElevator(rb));
            Object.Destroy(this);
        }
    }

    IEnumerator stopElevator(Rigidbody rb)
    {
        float initailPos = transform.position.y;
        Object.Instantiate(elevator, initialPos, Quaternion.identity);
        while (true)
        {
            if (transform.position.y > initailPos + hauteur )
            {
                break;
            }
            yield return null;
        }

        rb.velocity = new Vector3(velocite, leavingJumpPower);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
        rb.useGravity = true;
    }
}
