using UnityEngine;
using UnityEngine.Rendering;

public class IsObstacle : MonoBehaviour
{
    [SerializeField]
    Material invisibleMat;

    [SerializeField]
    Material opaqueMat;
    //Material material;

    //[SerializeField] const float transparency = 0.1f;

    //private void Start()
    //{
    //    material = GetComponent<Renderer>().material;
    //}

    public void TurnInvisible()
    {
        GetComponent<Renderer>().material = invisibleMat;
        //material.SetFloat("_Mode", 0);
        //material.SetInt("_SrcBlend", (int)BlendMode.One);
        //material.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
        //material.SetInt("_ZWrite", 0);
        //material.DisableKeyword("_ALPHATEST_ON");
        //material.DisableKeyword("_ALPHABLEND_ON");
        //material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        //material.renderQueue = 3000;

        //Color color = material.color;
        //color.a = transparency;
        //material.color = color;
    }

    public void TurnOpaque()
    {
        GetComponent<Renderer>().material = opaqueMat;
    }
}