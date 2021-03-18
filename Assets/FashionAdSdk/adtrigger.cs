using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class adtrigger : MonoBehaviour
{
    // public RawImage adPanel;
    // public Button goToBuyButton;

    private BannerView adBanner;
    private UnifiedNativeAd adNative;
    private bool nativeLoaded = false;
    [SerializeField] GameObject adNativePanel;
	[SerializeField] RawImage adIcon;
	[SerializeField] RawImage adChoices;
	[SerializeField] Text adHeadline;
	[SerializeField] Text adCallToAction;
    [SerializeField] Button adCallToActionButton;
	[SerializeField] Text adAdvertiser;
    string userId = "james";
    private bool isUserInTrigArea;
    private Queue<string> queue = new Queue<string>(); // 5�� ������ �� �޽��� ���� ť
    private int trigNum; // ���° Ʈ���� �ȿ� ���Դ���
    private bool isAdPanelSet = false; //�����ǿ� ������ ���õǾ��ִ��� �ƴ���
    
    private string idApp, idBanner, idNative;
    
    void Start ()
	{
		idApp = "ca-app-pub-5866285719867705~7266895518";
		idBanner = "ca-app-pub-3940256099942544/6300978111";
		idNative = "ca-app-pub-3940256099942544/2247696110";

		MobileAds.Initialize(initStatus => { });
		// MobileAds.Initialize (idApp);

		// RequestBannerAd ();
		RequestNativeAd ();
        adCallToActionButton.gameObject.SetActive(false);
	}
    private void Update()
    {
        if (queue.Count > 0 && !isUserInTrigArea)
        {
            adNativePanel.SetActive (false); //hide ad panel
            adIcon.texture = null;
            adChoices.texture = null;
            adHeadline.text = null;
            adCallToAction.text = null;
            adCallToActionButton.gameObject.SetActive(false);
            adAdvertiser.text = null;
            // goToBuyButton.gameObject.SetActive(false);
            queue.Dequeue();
            isAdPanelSet = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "sphere")
        {
            trigNum++;
            Debug.Log("Trigger Enter!");
            
            if (!isAdPanelSet)
            {
                // FashionAd fd = adPanel.GetComponent<FashionAd>();
                // fd.ShowAd();
                // goToBuyButton.gameObject.SetActive(true);
                RequestNativeAd ();

                if (nativeLoaded) {
                    nativeLoaded = false;

                    Texture2D iconTexture = this.adNative.GetIconTexture ();
                    Texture2D iconAdChoices = this.adNative.GetAdChoicesLogoTexture ();
                    string headline = this.adNative.GetHeadlineText ();
                    string cta = this.adNative.GetCallToActionText ();
                    string advertiser = this.adNative.GetAdvertiserText ();
                    adIcon.texture = iconTexture;
                    adChoices.texture = iconAdChoices;
                    adHeadline.text = headline;
                    adAdvertiser.text = advertiser;
                    adCallToActionButton.gameObject.SetActive(true);
                    adCallToAction.text = cta;
                    
                    //register gameobjects
                    adNative.RegisterIconImageGameObject (adIcon.gameObject);
                    adNative.RegisterAdChoicesLogoGameObject (adChoices.gameObject);
                    adNative.RegisterHeadlineTextGameObject (adHeadline.gameObject);
                    adNative.RegisterCallToActionGameObject (adCallToAction.gameObject);
                    adNative.RegisterAdvertiserTextGameObject (adAdvertiser.gameObject);

                    adNativePanel.SetActive (true); //show ad panel
                }
            }

            isUserInTrigArea = true;
            isAdPanelSet = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "sphere")
        {
            Debug.Log("Trigger Exit!");

            //5�� �ڿ� ������� �ϴ� ������
            Thread td = new Thread(new ParameterizedThreadStart(Run));
            td.Start(trigNum);
            isUserInTrigArea = false;
        }
    }


    /*
     * 5�� �� ������ �����϶�� �޼����� ���� �޼ҵ�
     * �ٽ� Ʈ���� �ȿ� ������ ��� ������ ���ư��� �����带
     * ����ȭ ��Ű�� ���� �� �����尡 ���۵� ���� trignumber�� ���簡
     * �������� ����.
     * �̷��� ���� ������ ������ Ʈ���� ���� �����ٰ� �ٽ� 5�� �ȿ� �ٽ� ���� �Ŀ�
     * �ٷ� �ٽ� ������ ���� 5�� �ڿ� ������ ������� �ʰ� �ٷ� ������ų� 5�ʰ� �Ǳ� ���� �����.
     * ������ ���ư��� �����尡 ������ �����鼭 �޼����� ������ ���� 
     */
    private void Run(object trigNumber)
    {
        Thread.Sleep(5000);
        if ((int)trigNumber == trigNum)
        {
            string message = "resetAdPanel";
            queue.Enqueue(message);
        }
    }

    #region Banner Methods --------------------------------------------------

	public void RequestBannerAd ()
	{
		adBanner = new BannerView (idBanner, AdSize.Banner, AdPosition.Bottom);
		AdRequest request = AdRequestBuild ();
		adBanner.LoadAd (request);
	}

	public void DestroyBannerAd ()
	{
		if (adBanner != null)
			adBanner.Destroy ();
	}

	#endregion

	#region Native Ad Mehods ------------------------------------------------

	private void RequestNativeAd ()
	{
		AdLoader adLoader = new AdLoader.Builder (idNative).ForUnifiedNativeAd ().Build ();
		adLoader.OnUnifiedNativeAdLoaded += this.HandleOnUnifiedNativeAdLoaded;
		adLoader.LoadAd (AdRequestBuild ());
	}

	//events
	private void HandleOnUnifiedNativeAdLoaded (object sender, UnifiedNativeAdEventArgs args)
	{
		this.adNative = args.nativeAd;
		nativeLoaded = true;
	}

	#endregion

	//------------------------------------------------------------------------
	AdRequest AdRequestBuild ()
	{
		return new AdRequest.Builder ().Build ();
	}

	void OnDestroy ()
	{
		DestroyBannerAd ();
	}
}
    
