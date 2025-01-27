using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f); // Détruit l'effet après 2 secondes
    }
}
