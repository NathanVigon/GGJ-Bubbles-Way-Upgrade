using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pikes : MonoBehaviour {
    public GameObject deathEffect; // Assigne ton prefab de particules dans l'inspecteur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Instancier l'effet à la position du joueur avant de le détruire
            Instantiate(deathEffect, other.transform.position, Quaternion.identity);

            // Détruire le joueur
            Destroy(other.gameObject);
        }
    }
}
