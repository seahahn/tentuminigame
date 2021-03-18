using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onOffInven : MonoBehaviour
{
    public GameObject InventoryPanel;
    bool activeInventory = false;

    void Start()
    {
        InventoryPanel.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            InventoryPanel.SetActive(activeInventory);
        }
    }
}
