using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum BtnTypeEquip
{
    Buy,
    Sell,       
    Equip,
    UnEquip,
    StorageIn,
    StorageOut
}


public class itemequip : MonoBehaviour
{
    public BtnTypeEquip currentTypeEquip;

     
    public void OnBtnClick()
    {
        switch (currentTypeEquip)
        {
            case BtnTypeEquip.Buy:
                Debug.Log("구매");  //착용:equip, 착용해제:unequip,  구매:buy, 판매:sell 창고in: storagein, 창고out:storageout

                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "buy");

                break;

            case BtnTypeEquip.Sell:
                Debug.Log("판매");

                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "sell");

                break;

            case BtnTypeEquip.Equip:
                Debug.Log("착용");
               
                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "equip");

                break;

            case BtnTypeEquip.UnEquip:
                Debug.Log("착용해제");

                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "unequip");

                break;

           

            case BtnTypeEquip.StorageIn:
                Debug.Log("창고넣기");

                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "storagein");

                break;

            case BtnTypeEquip.StorageOut:
                Debug.Log("창고빼기");

                UserPreferenceCalculate("MINIGAME", "james", "JJJ", "man_knitwear", "storageout");

                break;

        }

    }
    
    
    public void UserPreferenceCalculate(String GameID, String UserId, String ItemID, String Category, String Action)
    {

        StartCoroutine(WebItemEquip(GameID, UserId, ItemID, Category, Action));

    }



    public IEnumerator WebItemEquip(String GameID, String UserId, String ItemID, String Category, String Action)
    {
        WWWForm form = new WWWForm();
        form.AddField("ID", GameID);
        form.AddField("UserId", UserId);
        form.AddField("ItemID", ItemID);
        form.AddField("Category", Category);
        form.AddField("Action", Action);


        using (UnityWebRequest www = UnityWebRequest.Post("http://52.79.143.149/kimtest/ItemPreference_Test.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

            }
        }
    }

}
