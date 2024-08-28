using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class ItemContainer : ScriptableObject
{
    [SerializeField]
    private Item[] items = new Item[8];

    public Item[] Items { get => this.items; set => this.items = value; }
    

}
