using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpPickable
{
    GameObject gameObject { get; }

    public MeshRenderer meshRenderer { get; set; }
    
    void PickUp(InventoryManager inventory)
    {
        inventory.AddItem(gameObject);
    }

    void DrawWithOutline(Shader outlineShader)
    {
        meshRenderer.materials[1].shader = outlineShader;
    }

    void DrawWithoutOutline()
    {
        meshRenderer.materials[1] = null;
    }
}
