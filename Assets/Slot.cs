using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public Inventory inven;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellBtn;
    public UnityEngine.UI.Text itemname;
    public UnityEngine.UI.Text price;
    public UnityEngine.UI.Image head;
    public UnityEngine.UI.Image body;
    public UnityEngine.UI.Image bottom;
    public UnityEngine.UI.Image shoes;
    public UnityEngine.UI.Image equipCheck;
    private void Awake()
    {
        image.enabled = false;
        SetSellBtnInteractable(false);
    }

    void Update()
    {
    }

        void SetSellBtnInteractable(bool b) 
    {
        if (sellBtn != null)
            sellBtn.interactable = b;
    }
    public void SetItem(ItemProperty item)//상점 아이템 셋팅.
    {
        this.item = item;

        if (item == null)
        {
            image.enabled = false;
            SetSellBtnInteractable(false);
            gameObject.name = "Empty";
            itemname.text = "Empty";
            price.text = "Empty".ToString();
        }
        else
        {
            SetSellBtnInteractable(true);
            image.enabled = true;
            gameObject.name = item.name;
            image.sprite = item.sprite;
            itemname.text = item.itemname;
            price.text = item.price.ToString();
        }
    }

    public void OnClickSellBtn()
    {
        SetItem(null);
    }

    public void onClickItem()
    {
        Debug.Log(item.name);
        Debug.Log(item.parts);
        itemequip ie = gameObject.AddComponent<itemequip>();
        ie.UserPreferenceCalculate("MINIGAME", "james", item.name, item.categories, "equip");
        switch (item.parts)
        {
            case "head":
                if (equipCheck.gameObject.activeSelf == true)
                {
                    equipCheck.gameObject.SetActive(false);
                    Debug.Log(item.name + "을 장착 해제.");
                    head.sprite = null;
                }
                else if (equipCheck.gameObject.activeSelf == false && head.sprite != null) 
                {
                    Debug.Log("다른 의상을 입고있어 착용이 불가능 합니다.");
                }
                else
                {
                    head.sprite = item.sprite;
                    equipCheck.gameObject.SetActive(true);
                    Debug.Log(item.name + "을 장착했습니다.");
                }
                break;
            case "body":
                if (equipCheck.gameObject.activeSelf == true)
                {
                    equipCheck.gameObject.SetActive(false);
                    Debug.Log(item.name + "을 장착 해제.");
                    body.sprite = null;
                } else if (equipCheck.gameObject.activeSelf == false && body.sprite != null) 
                {
                    Debug.Log("다른 의상을 입고있어 착용이 불가능 합니다.");
                }
                else
                {
                    body.sprite = item.sprite;
                    equipCheck.gameObject.SetActive(true);
                    Debug.Log(item.name + "을 장착했습니다.");
                }
                break;
            case "bottom":
                if (equipCheck.gameObject.activeSelf == true)
                {
                    equipCheck.gameObject.SetActive(false);
                    Debug.Log(item.name + "을 장착 해제.");
                    bottom.sprite = null;
                }
                else if (equipCheck.gameObject.activeSelf == false && bottom.sprite != null)
                {
                    Debug.Log("다른 의상을 입고있어 착용이 불가능 합니다.");
                }
                else
                {
                    bottom.sprite = item.sprite;
                    equipCheck.gameObject.SetActive(true);
                    Debug.Log(item.name + "을 장착했습니다.");
                }
                break;
            case "shoes":
                if (equipCheck.gameObject.activeSelf == true)
                {
                    equipCheck.gameObject.SetActive(false);
                    Debug.Log(item.name + "을 장착 해제.");
                    shoes.sprite = null;
                }
                else if (equipCheck.gameObject.activeSelf == false && shoes.sprite != null) 
                {
                    Debug.Log("다른 의상을 입고있어 착용이 불가능 합니다.");
                }
                else
                {
                    shoes.sprite = item.sprite;
                    equipCheck.gameObject.SetActive(true);
                    Debug.Log(item.name + "을 장착했습니다.");
                }
                break;
        }
    }
}
