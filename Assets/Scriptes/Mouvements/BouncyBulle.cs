using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBulle : MonoBehaviour
{

    [SerializeField] private float bounciness;
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
        float tempBounciness = bounciness;
        if (other.gameObject.transform.position.x < this.gameObject.transform.position.x)
        {
            print(1 + (this.gameObject.transform.position.x - other.gameObject.transform.position.x) * 0.15f);
            tempBounciness *= 1+(this.gameObject.transform.position.x - other.gameObject.transform.position.x) * 0.15f;
        }
        other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(other.gameObject.GetComponent<Rigidbody>().velocity.x, (-other.gameObject.GetComponent<Rigidbody>().velocity.y) * tempBounciness);
    }
}
