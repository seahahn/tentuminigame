using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Tentuplay.FashionAd
{
    public class FashionAdApi : MonoBehaviour
    {
        /*url을 통해 웹에서 이미지 받아와서 광고판 이미지 변경하기 */
        public IEnumerator sendShowAd(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                this.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }

        public void AdClick(string userId, string adId, string ownerId)
        {
            StartCoroutine(sendAdClick(userId, adId, ownerId));
        }
        public void AdImpression(string userId, string adId, string ownerId)
        {
            StartCoroutine(sendAdImpression(userId, adId, ownerId));
        }
        //보조 메소드
        private IEnumerator sendAdClick(string userId, string adId, string ownerId)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("adId", adId));
            formData.Add(new MultipartFormDataSection("userId", userId));
            formData.Add(new MultipartFormDataSection("ownerId", ownerId));
            formData.Add(new MultipartFormDataSection("type", "adClick"));
            formData.Add(new MultipartFormDataSection("clientId", "clientId"));

            UnityWebRequest www = UnityWebRequest.Post("http://52.79.143.149/DataProcessing/putAdData.php", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
        }
        private IEnumerator sendAdImpression(string userId, string adId, string ownerId)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("adId", adId));
            formData.Add(new MultipartFormDataSection("userId", userId));
            formData.Add(new MultipartFormDataSection("ownerId", ownerId));
            formData.Add(new MultipartFormDataSection("type", "adImpression"));
            formData.Add(new MultipartFormDataSection("clientId", "clientId"));

            UnityWebRequest www = UnityWebRequest.Post("http://52.79.143.149/DataProcessing/putAdData.php", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
        public IEnumerator sendSetImageAdData(string userId, PanelShape adPanelShape, System.Action<FashionAdApi.AdData> var)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("userId", userId));
            formData.Add(new MultipartFormDataSection("adPanelShape", adPanelShape.ToString()));
            formData.Add(new MultipartFormDataSection("clientId", "clientId"));

            UnityWebRequest www = UnityWebRequest.Post("http://52.79.143.149/DataProcessing/getPanelAdData.php", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                string data = www.downloadHandler.text;
                Debug.Log(data);
                AdData adData = JsonConvert.DeserializeObject<AdData>(data);
                var(adData);
            }
        }
        /* 팝업 광고 보여주는 메소드 */
        public IEnumerator ShowPopUpAd(string url, AdData adData, UnityEngine.Events.UnityAction buttonAction)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            WaitForSeconds ws = new WaitForSeconds(2);
            yield return www.SendWebRequest();
            yield return ws;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                switch (adData.imageRatio)
                {
                    case "img43" :
                        // 팝업 광고 이미지 UI 찾아서 바꿔주고, 버튼에 링크 달기
                        GameObject canvas = GameObject.Find("PopUpAdGroup").transform.Find("PopUpAd43").gameObject;
                        canvas.SetActive(true);
                        GameObject panel = canvas.transform.Find("Panel").gameObject;
                        panel.transform.Find("PopUpAdImage43").GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture; // 팝업 광고 이미지 UI 찾아서 바꿔주기
                        panel.transform.Find("GoToBuyButton").GetComponent<Button>().onClick.AddListener(buttonAction);

                        break;
                    case "img34":
                        GameObject canvas1 = GameObject.Find("PopUpAdGroup").transform.Find("PopUpAd34").gameObject;
                        canvas1.SetActive(true);
                        GameObject panel1 = canvas1.transform.Find("Panel").gameObject;
                        panel1.transform.Find("PopUpAdImage34").GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture; // 팝업 광고 이미지 UI 찾아서 바꿔주기
                        panel1.transform.Find("GoToBuyButton").GetComponent<Button>().onClick.AddListener(buttonAction);
                        break;
                    case "img11":
                        GameObject canvas2 = GameObject.Find("PopUpAdGroup").transform.Find("PopUpAd11").gameObject;
                        canvas2.SetActive(true);
                        GameObject panel2 = canvas2.transform.Find("Panel").gameObject;
                        panel2.transform.Find("PopUpAdImage11").GetComponent<RawImage>().texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                        panel2.transform.Find("GoToBuyButton").GetComponent<Button>().onClick.AddListener(buttonAction);
                        break;
                }
            }
        }


        /* 팝업 광고 정보 받아와서 세팅해주는 메소드 */
        public IEnumerator SetPopUpAdData(string userId, string adId, System.Action<FashionAdApi.AdData> var)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("userId", userId));
            formData.Add(new MultipartFormDataSection("adId", adId));
            formData.Add(new MultipartFormDataSection("clientId", "clientId"));

            UnityWebRequest www = UnityWebRequest.Post("http://52.79.143.149/DataProcessing/getPopUpAdData.php", formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                string data = www.downloadHandler.text;
                Debug.Log("팝업광고 받아온 데이터는 : " + data);
                AdData adData = JsonConvert.DeserializeObject<AdData>(data);
                var(adData);
            }
        }

        //보조 enum ,calss
        public class AdData
        {
            public string adId;
            public string adSourceUrl;
            public string productUrl;
            public string ownerId;
            public string imageRatio;
        }

        public enum Category
        {
            man_knit,
            man_tShirts,
            man_shirts,
            man_cardigan,
            man_jumper,
            man_jacket,
            man_coat,
            man_blueJean,
            man_pants,
            man_sweatPants,
            man_sweatShirts,
            man_sneakers,
            man_boots,
            man_slipper,
            man_moccasin,
            man_sportsShoes,
            man_worker,
            man_dressShoes,
            woman_knit,
            woman_cardigan,
            woman_onePiece,
            woman_tShirts,
            woman_blouse,
            woman_jumper,
            woman_jacket,
            woman_coat,
            woman_blueJean,
            woman_skirt,
            woman_leggings,
            woman_pants,
            woman_sweatPants,
            woman_sweatShirts,
            woman_boots,
            woman_worker,
            woman_heel,
            woman_sportsShoes,
            woman_flatShoes,
            woman_bootie,
            woman_sneakers,
        }
        public enum PanelShape
        {
            img11,
            img43,
            img34
        }
    }
}


