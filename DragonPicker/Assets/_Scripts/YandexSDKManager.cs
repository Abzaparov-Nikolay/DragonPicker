using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
using UnityEngine.Events;

public class YandexSDKManager : MonoBehaviour
{
    public TextMeshProUGUI onlineStatus;
    public GameObject onlineStatusOrb;

    public TextMeshProUGUI textBox;
    public UnityEvent authorizationEvent;

    private bool isFirstLaunch = true;
    public TextMeshProUGUI bestScoreGO;


    public void AuthorizationSuccessfull()
    {
        textBox.text = $"Authorization successfull. Hello, {YandexGame.playerName}!";
        if (onlineStatus != null)
        {
            onlineStatus.text = "You are online";
            onlineStatusOrb.GetComponent<MeshRenderer>().material.color = new Color(0, 255, 0);
        }
    }

    public void AuthorizationFailed()
    {
        textBox.text = "Authorization failed.";
        if (onlineStatus != null)
        {
            onlineStatus.text = "You are offline";
            onlineStatusOrb.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
        }
        YandexGame.AuthDialog();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += ReceiveSDKData;
        YandexGame.CloseVideoEvent += RewardAdWatcher;
        DisplayData();

    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ReceiveSDKData;
        YandexGame.CloseVideoEvent -= RewardAdWatcher;
    }

    public void RewardAdWatcher(int id)
    {
        if(id == 1)
        {
            Debug.Log("Награда получена!");
        }
    }

    public void ShowAd()
    {
        YandexGame.RewVideoShow(1);
    }

    public void ReceiveSDKData()
    {
        
        if (YandexGame.SDKEnabled && isFirstLaunch)
        {
            textBox.text = "SDK enabled. Waiting for authorization.";
            isFirstLaunch = false;
            authorizationEvent.Invoke();
            
        }
        else
        {
            
        }
        YandexGame.RewVideoShow(0);
    }

    public void DisplayData()
    {
        if (bestScoreGO != null)
        {
            bestScoreGO.text = $"Best Score: {YandexGame.savesData.bestScore}";
        }
    }
}
