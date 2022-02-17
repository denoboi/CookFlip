using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeMaterial : MonoBehaviour
{
    public Material[] materials;

    
    SkinnedMeshRenderer meshRenderer;



    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        
        
    }

   

    public void ChangeStandard()
    {
        
    }

    public void CookedUp()
    {
        Material[] mats = meshRenderer.materials;
        mats[1] = materials[1];

        meshRenderer.materials = mats;
    }

    public void CookedDown()
    {
        Material[] mats = meshRenderer.materials;
        mats[0] = materials[1];

        meshRenderer.materials = mats;
    }

    public void BurntUp()
    {
        Material[] mats = meshRenderer.materials;
        mats[1] = materials[2];

        meshRenderer.materials = mats;
    }

    public void BurntDown()
    {
        Material[] mats = meshRenderer.materials;
        mats[0] = materials[2];

        meshRenderer.materials = mats;
    }

}
