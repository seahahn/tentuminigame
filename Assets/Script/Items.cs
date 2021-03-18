using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {head,body,legs,foots}

[System.Serializable]
public class Items
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public bool Use()
    {
        bool isUsed = false;
        isUsed = true;
        return false;
    }
}
