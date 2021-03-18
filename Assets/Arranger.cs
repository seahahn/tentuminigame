using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arranger : MonoBehaviour
{
    List<Transform> children;
    // Start is called before the first frame update
    void Start()
    {
        children = new List<Transform>();
        UpdateChildren();
    }

    public void UpdateChildren()
    {
        for (int i=0; i < transform.childCount; i++)
        {
            if (i == children.Count) 
            {
                children.Add(null);
            }
            var child = transform.GetChild(i);

            if (child != children[i])
            {
                children[i] = child;
            }
        }
        children.RemoveRange(transform.childCount, children.Count - transform.childCount);
    }
    // Update is called once per frame

    public int GetIndexByPosition(Transform slot, int skipIndex = -1)
    {
        int result = 0;
        for (int i = 0; i < children.Count; i++) 
        {
            if (slot.position.x < children[i].position.x)
            {
                break;
            }
            else if (skipIndex != i)
            {
                result++;
            }
        }
        return result;
    }

    public void SwapCard(int index01, int index02)
    {
        InventoryUI.SwapCards(children[index01], children[index02]);
        UpdateChildren();
    }
    void Update()
    {
    }
}
