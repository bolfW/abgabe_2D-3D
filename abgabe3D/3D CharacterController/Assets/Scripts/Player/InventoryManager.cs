using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject[] inventory = new GameObject[8];

    public GameObject[] Inventory { get => this.inventory; set => this.inventory = value; }

    private GameObject itemToHold;
    [SerializeField]private GameObject ObjectPosition;
    private Vector3 ObjPos;
    

    
    void Update()
    {

        if (SelectItemToHold() != null)
        {
            itemToHold = SelectItemToHold();

        
            itemToHold.transform.parent = ObjectPosition.transform;
            itemToHold.SetActive(true);
        }
    }

    GameObject SelectItemToHold()
    {
        GameObject itemToHold = inventory[0];

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[2];
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[3];
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[4];
        }
        else if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[5];
        }
        else if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[6];
        }
        else if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[7];
        }
        else if (Keyboard.current.digit8Key.wasPressedThisFrame)
        {
            itemToHold = Inventory[8];
        }

        if (itemToHold != null)
        {
            itemToHold.transform.position = ObjectPosition.transform.position;
        }

        return itemToHold;
    }

    public void AddItem(GameObject item)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                inventory[i].GetComponent<Rigidbody>().freezeRotation = true;
                inventory[i].GetComponent<Collider>().enabled = false;
                inventory[i].SetActive(false);
                break;
            }
        }
        
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
            }
        }

    }
}
