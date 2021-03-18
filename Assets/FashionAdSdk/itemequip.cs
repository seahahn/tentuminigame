using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class itemequip : MonoBehaviour
{    
    public void UserPreferenceCalculate(String GameID, String UserId, String ItemID, String Category, String Action)
    {
        StartCoroutine(WebItemEquip(GameID, UserId, ItemID, Category, Action));
    }



    public IEnumerator WebItemEquip(String GameID, String UserId, String ItemID, String Category, String Action)
    {
        Debug.Log("helloworld");
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
