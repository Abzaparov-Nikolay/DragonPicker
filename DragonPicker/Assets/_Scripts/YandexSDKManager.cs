using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;
using UnityEngine.Events;

public class YandexSDKManager : MonoBehaviour
{
    public TextMeshProUGUI textBox;
    public UnityEvent authorizationEvent;

    private bool isFirstLaunch = true;
    public TextMeshProUGUI bestScoreGO;


    public void AuthorizationSuccessfull()
    {
        textBox.text = $"Authorization successfull. Hello, {YandexGame.playerName}!";
    }

    public void AuthorizationFailed()
    {
        textBox.text = "Authorization failed.";
        YandexGame.AuthDialog();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += ReceiveSDKData;
        DisplayData();

    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ReceiveSDKData;
    }

    public void ReceiveSDKData()
    {
        
        if (YandexGame.SDKEnabled && isFirstLaunch)
        {
            textBox.text = "SDK enabled. Waiting for authorization.";
            isFirstLaunch = false;
            authorizationEvent.Invoke();
        }
    }

    public void DisplayData()
    {
        if (bestScoreGO != null)
        {
            bestScoreGO.text = $"Best Score: {YandexGame.savesData.bestScore}";
        }
    }
}
