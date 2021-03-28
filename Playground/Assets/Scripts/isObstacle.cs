using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isObstacle : MonoBehaviour
{
    [SerializeField]
    Material invisibleMat;

    [SerializeField]
    Material opaqueMat;

    public void TurnInvisible()
    {
        GetComponent<Renderer>().material = invisibleMat;
    }

    public void TurnOpaque()
    {
        GetComponent<Renderer>().material = opaqueMat;
    }
}