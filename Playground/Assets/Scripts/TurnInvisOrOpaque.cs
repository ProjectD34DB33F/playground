using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInvisOrOpaque : MonoBehaviour
{
    [SerializeField]
    Material invisibleMaterial;

    [SerializeField]
    Material opaqueMaterial;

    public void turnInvis()
    {
        GetComponent<Renderer>().material = invisibleMaterial;
    }

    public void turnOpaque()
    {
        GetComponent<Renderer>().material = opaqueMaterial;
    }
}
