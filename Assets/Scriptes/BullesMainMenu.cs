using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullesMainMenu : MonoBehaviour
{
    public float gravityScale = 0.1f; // Gravité faible
    public float minSpeed = 1.0f; // Vitesse minimale pour éviter l'arrêt complet

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // Désactiver la gravité par défaut
        rb.constraints = RigidbodyConstraints.FreezePositionZ; // Bloquer le mouvement sur l'axe Z
    }

    // Update is called once per frame
    void Update()
    {
        // Appliquer une gravité personnalisée
        rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);

        // Assurer une vitesse minimale pour éviter l'arrêt complet
        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Inverser la direction de la vitesse en fonction de la normale de la collision
        Vector3 velocity = rb.velocity;
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflectedVelocity = Vector3.Reflect(velocity, normal);
        rb.velocity = reflectedVelocity.normalized * velocity.magnitude;

        // Ajouter une petite force aléatoire pour éviter l'arrêt complet
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * 0.1f, ForceMode.Impulse);
    }
}