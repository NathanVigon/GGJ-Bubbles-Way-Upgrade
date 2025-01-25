using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public Movement movement;
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
        print(other.transform.parent.name);
        movement = other.GetComponent<Movement>();
        if (!movement.invertedGravity)
        {
            movement.invertedGravity = true;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            movement.invertedGravity = false;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
