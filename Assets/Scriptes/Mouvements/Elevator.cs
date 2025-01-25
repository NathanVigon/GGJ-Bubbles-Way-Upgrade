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
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, vitesseAscension);
            rb.useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, vitesseAscension); 
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(stopElevator(rb));
        }
    }

    IEnumerator stopElevator(Rigidbody rb)
    {
        
        float initailY = transform.position.y;
        rb.transform.position = new Vector3 (rb.transform.position.x, initailY + (float)0.6);
        Object.Instantiate(elevator, initialPos, Quaternion.identity);

        while (this.transform.position.y < (initailY + hauteur))
        {
            yield return null;
        }

        rb.velocity = new Vector3(velocite, leavingJumpPower);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
        rb.useGravity = true;
        Object.Destroy(this.gameObject);
    }
}
