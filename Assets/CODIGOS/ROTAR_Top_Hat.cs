using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTAR_Top_Hat : MonoBehaviour
{
    void Update()
    {
        Vector3 rotationToAdd = new Vector3(0, 0, -1);
        transform.Rotate(rotationToAdd);
    }
}