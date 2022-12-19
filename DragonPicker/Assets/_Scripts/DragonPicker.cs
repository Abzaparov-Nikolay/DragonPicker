using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using System.Linq;

public class DragonPicker : MonoBehaviour
{
    public GameObject energyShieldPrefab;
    public TextMeshProUGUI scoreGT;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -6f;
    public float energyShieldRadius = 1.5f;

    public List<GameObject> shieldList;
    public TextMeshProUGUI playerName;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetSavedData;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetSavedData;
    }



    void Start()
    {
        if (YandexGame.SDKEnabled)
            GetSavedData();

        shieldList = new List<GameObject>();

        for (var i = 1; i <= numEnergyShield; i++)
        {
            var tShieldGo = Instantiate(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(i, i, i);
            shieldList.Add(tShieldGo);
        }
    }

    void Update()
    {

    }


    public void DragonEggDestroyed()
    {
        var tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (var tGO in tDragonEggArray)
        {
            Destroy(tGO);
        }
        var shieldIndex = shieldList.Count - 1;
        var tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0)
        {
            EndGame();
        }
    }

    public void GetSavedData()
    {
        playerName.text = YandexGame.playerName;
    }

    public static void SaveScore(int score)
    {
        YandexGame.savesData.score = score;
        if (YandexGame.savesData.bestScore < score)
            YandexGame.savesData.bestScore = score;
        YandexGame.SaveProgress();
    }

    public static void SaveAchivement(string achivement)
    {
        if(!YandexGame.savesData.achivements.Contains(achivement))
            YandexGame.savesData.achivements = YandexGame.savesData.achivements.Append(achivement).ToArray();
        YandexGame.SaveProgress();
    }

    private void EndGame()
    {
        var scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        SaveScore(int.Parse(scoreGT.text));
        SaveAchivement("Lol you died! Try to avoid it");
        YandexGame.NewLeaderboardScores("Leaderboard0", int.Parse(scoreGT.text));
        YandexGame.RewVideoShow(0);
        SceneManager.LoadScene("_0Scene");
    }
}
