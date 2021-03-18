using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Items items;
    public SpriteRenderer image;
    public void SetItem(Items _items) 
    {
        items.itemName = _items.itemName;
        items.itemImage = _items.itemImage;
        items.itemType = _items.itemType;

        image.sprite = items.itemImage;
    }
    public Items GetItem()
    {
        return items;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
