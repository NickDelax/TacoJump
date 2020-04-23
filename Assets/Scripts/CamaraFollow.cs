using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform objeto;
    public Vector3 desfase;
    private void LateUpdate()
    {
        transform.position = objeto.position + desfase;
    }
}
