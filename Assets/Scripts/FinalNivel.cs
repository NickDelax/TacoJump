using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalNivel : MonoBehaviour
{
    public GameObject Ganaste;

    void OntriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            Ganaste.GetComponent<Canvas>().enabled = true;
    }
}
