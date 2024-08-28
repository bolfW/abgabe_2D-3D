using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour, IUpPickable
{
    [SerializeField] private InventoryManager inventory;
    [SerializeField] private MeshRenderer itemRenderer;
    [SerializeField] private Rigidbody rb;
    public MeshRenderer meshRenderer { get => itemRenderer; set => itemRenderer = value; } 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Throw()
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
    }

    
}
