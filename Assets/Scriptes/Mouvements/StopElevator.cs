using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopElevator : MonoBehaviour
{

    public float velocite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.velocity = new Vector3(velocite, 0);

        //On recupere la plateforme pour arreter le mouvement
        transform.parent.gameObject.transform.GetChild(0).GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
    }

}
