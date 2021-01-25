using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparencyManager : MonoBehaviour
{
    private Material opaqueMaterial;
    private Material transparentMaterial;

    //Variables
    private Renderer renderer;
    private bool transparent = false;

    private void Start()
    {
        opaqueMaterial = Resources.Load<Material>("Materials/Building");
        transparentMaterial = Resources.Load<Material>("Materials/TransparentBuilding");

        //Get the renderer of the object
        renderer = GetComponent<Renderer>();
    }

    public void SetTransparent(bool transparent)
    {
        //Avoid to set the same transparency twice
        if (this.transparent == transparent) return;

        //Set the new configuration
        this.transparent = transparent;

        //Check if should be transparent or not
        if (transparent)
        {
            renderer.material = transparentMaterial;
        }
        else
        { 
            renderer.material = opaqueMaterial;
        }
    }
}
