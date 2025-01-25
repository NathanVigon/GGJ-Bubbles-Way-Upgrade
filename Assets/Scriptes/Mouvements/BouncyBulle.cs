using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulle : MonoBehaviour
{

    public float bounciness;
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
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x, (-other.gameObject.GetComponent<Rigidbody>().velocity.y) * bounciness);
    }
}
