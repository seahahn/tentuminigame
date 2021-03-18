using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class adtrigger : MonoBehaviour
{
    public RawImage adPanel;
    public Button goToBuyButton;
    string userId = "james";
    private bool isUserInTrigArea;
    private Queue<string> queue = new Queue<string>(); // 5초 지나고 올 메시지 담을 큐
    private int trigNum; // 몇번째 트리거 안에 들어왔는지
    private bool isAdPanelSet = false; //광고판에 광고가 세팅되어있는지 아닌지
    
    private void Update()
    {
        if (queue.Count > 0 && !isUserInTrigArea)
        {
            adPanel.texture = null;
            goToBuyButton.gameObject.SetActive(false);
            queue.Dequeue();
            isAdPanelSet = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "sphere")
        {
            trigNum++;
            Debug.Log("Trigger!");
            
            if (!isAdPanelSet)
            {
                FashionAd fd = adPanel.GetComponent<FashionAd>();
                fd.ShowAd();
                goToBuyButton.gameObject.SetActive(true);
            }

            isUserInTrigArea = true;
            isAdPanelSet = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "sphere")
        {
            Debug.Log("Trigger!");

            //5초 뒤에 사라지게 하는 쓰레드
            Thread td = new Thread(new ParameterizedThreadStart(Run));
            td.Start(trigNum);
            isUserInTrigArea = false;
        }
    }


    /*
     * 5초 뒤 광고판 리셋하라는 메세지를 보낼 메소드
     * 다시 트리거 안에 들어왔을 경우 기존에 돌아가던 쓰레드를
     * 무력화 시키기 위해 이 쓰레드가 시작될 때의 trignumber와 현재가
     * 동일한지 비교함.
     * 이렇게 하지 않으면 유저가 트리거 밖을 나갔다가 다시 5초 안에 다시 들어온 후에
     * 바로 다시 나갔을 때는 5초 뒤에 광고가 사라지지 않고 바로 사라지거나 5초가 되기 전에 사라짐.
     * 기존에 돌아가던 쓰레드가 실행이 끝나면서 메세지를 날리기 때문 
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
}
    
