using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    [Header("Flash Material")]
    public Material flashMaterial;
    public float flashDuration = 0.1f; // How long the flash effect should last
   
    
    // private references
    private Renderer spaceShipRenderer;
    private Material originalMaterial;
    

    private void Start()
    {
        spaceShipRenderer = GetComponent<Renderer>();
        originalMaterial = spaceShipRenderer.material;
        
    }
    

    public IEnumerator FlashCoroutine()
    {
        // swap the material
        spaceShipRenderer.material = flashMaterial;
        

        yield return new WaitForSeconds(flashDuration);

        spaceShipRenderer.material = originalMaterial;
       

    }

    
}
