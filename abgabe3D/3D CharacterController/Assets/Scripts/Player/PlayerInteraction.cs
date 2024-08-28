using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Shader outlineShader;
    [SerializeField] Shader defaultShader;
    Transform cameraRoot;
    [SerializeField]float interactionRange;

    GameObject oldHitObject = null;

    GameControlls gameInput;
    InputAction leftClick;
    InputAction pickUp;

    void Awake()
    {
        gameInput = new GameControlls();
    }

    void OnEnable()
    {
        leftClick = gameInput.Player.LeftClick;
        pickUp = gameInput.Player.Interaction;

        leftClick.Enable();
        pickUp.Enable();
    }

    private void OnDisable()
    {
        leftClick.Disable();
        pickUp.Disable();
    }

    void Start()
    {

        cameraRoot = Camera.main.transform.parent;
        
    }

    void Update()
    {
        PlayerInteractions();
    }

    void PlayerInteractions()
    {
        RaycastHit hit; 
        GameObject hitObject = null;
        



        if(Physics.Raycast(cameraRoot.position, cameraRoot.forward, out hit, interactionRange))
        {
            Debug.Log("Looking at Object");
            hitObject = hit.transform.gameObject;
            
            //draw shader
            if (hitObject.GetComponent<IUpPickable>() != null)
            {
                Debug.Log("Looking at Item");
                hitObject.GetComponent<IUpPickable>().DrawWithOutline(outlineShader);
                oldHitObject = hitObject;
            }
            else
            {
                Debug.Log("Not Looking at Item anymore");
                if(oldHitObject != null)
                {
                    oldHitObject.GetComponent<IUpPickable>().DrawWithOutline(defaultShader);
                    oldHitObject = null;
                }
                
            }

            //PickUpItems
            if (pickUp.WasPressedThisFrame())
            {
                if (hitObject.GetComponent<IUpPickable>() != null)
                {
                    hit.transform.GetComponent<IUpPickable>().PickUp(inventoryManager);
                }
            }


        }
        else
        {
            Debug.Log("Looking at nothing");
            if(oldHitObject != null)
            {
                oldHitObject.GetComponent<IUpPickable>().DrawWithOutline(defaultShader);
                oldHitObject = null;
            }
            
        }

        //if (leftClick.WasPressedThisFrame())
        //{


        //    if (hit.collider != null)
        //    {
        //        Debug.Log("looking at something");

        //        if (hit.transform.gameObject.GetComponent<IUpPickable>() != null)
        //        {
        //            Debug.Log("Looking at Item");
        //            hit.transform.GetComponent<IUpPickable>().PickUp(inventoryManager);
        //        }
        //    }

        //}
    }

    RaycastHit PlayerLookRaycast()
    {
        RaycastHit hit;

        Physics.Raycast(cameraRoot.position, cameraRoot.forward, out hit, interactionRange);

        return hit;
        

    }
}
