using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class slot : MonoBehaviour,IPointerUpHandler
{
    public Items items;
    public Image itemICon;


    public bool isShopMode;
    public void UpdateSoltUI()
    {
        itemICon.sprite = items.itemImage;
        itemICon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        items = null;
        itemICon.gameObject.SetActive(false);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        bool isUse = items.Use();
        if (isUse)
        {
        }
    }
}
