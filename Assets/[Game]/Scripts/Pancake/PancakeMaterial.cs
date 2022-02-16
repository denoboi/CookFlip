using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaterial : MonoBehaviour
{
    public Material standardTexture, cookedTexture, burntTexture;

    [SerializeField] private GameObject pancake;
    MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

   

    public void changeStandard()
    {
        meshRenderer.material = standardTexture;
    }

    public void changeCooked()
    {
        meshRenderer.material = cookedTexture;
    }

    public void changeBurnt()
    {
        meshRenderer.material = burntTexture;
    }

}
