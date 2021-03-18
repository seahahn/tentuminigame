using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    bool activeInventory = false;
    public Transform invisibleSlot;

    List<Arranger> arrangers;

    private void Start()
    {

        arrangers = new List<Arranger>();

        var arrs = transform.GetComponentsInChildren<Arranger>();

        for (int i = 0; i < arrs.Length; i++)
        {
            arrangers.Add(arrs[i]);
        }
    }

    private void Update()
    {
    }

    public static void SwapCards(Transform sour, Transform dest)
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;

        int sourIndex = sour.GetSiblingIndex();
        int destIndex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destIndex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourIndex);
    }
    void SwapSlotinHierarchy(Transform sour, Transform dest)
    {

        SwapCards(sour,dest);
        arrangers.ForEach(t => t.UpdateChildren());
    }

    bool ContainPos(RectTransform rt, Vector2 pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt,pos);

    }

    void BeginDrag(Transform item)
    {
        //Debug.Log("BeginDrag: " + item.name);

        SwapSlotinHierarchy(invisibleSlot, item);
    }
    void Drag(Transform card)
    {
        //Debug.Log("Drag: " + item.name);
        var whichArrangerCard = arrangers.Find(t => ContainPos(t.transform as RectTransform, card.position));
        if (whichArrangerCard == null)
        {
        }
        else
        {
           // Debug.Log(whichArrangerCard);
            Debug.Log(whichArrangerCard.GetIndexByPosition(card, invisibleSlot.GetSiblingIndex()));
            /*int invisibleSlotIndex = invisibleSlot.GetSiblingIndex();
            int targetIndex = whichArrangerCard.GetIndexByPosition(card, invisibleSlotIndex);
            if (invisibleSlotIndex != targetIndex)
            {
                whichArrangerCard.SwapCard(invisibleSlotIndex,targetIndex);
            }*/
            
        }
    }
    void EndDrag(Transform item)
    {
        //Debug.Log("EndDrag: " + item.name);

        SwapSlotinHierarchy(invisibleSlot, item);
    }
}
