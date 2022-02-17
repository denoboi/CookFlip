using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaterial : MonoBehaviour
{
    public Material standardTexture, cookedTexture, burntTexture;

    [SerializeField] private GameObject pancake;
    SkinnedMeshRenderer meshRenderer;



    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        
        
    }

   

    public void ChangeStandard()
    {
        meshRenderer.material = standardTexture;
    }

    public void ChangeCooked()
    {
        meshRenderer.material = cookedTexture;
    }

    public void ChangeBurnt()
    {
        meshRenderer.material = burntTexture;
    }

}
