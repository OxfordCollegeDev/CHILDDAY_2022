using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTAR_Circus_Tent : MonoBehaviour
{
    void Update()
    {
        Vector3 rotationToAdd = new Vector3(0, -1, 0);
        transform.Rotate(rotationToAdd);
    }
}