using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject achivementListScrollViewContent;

    [SerializeField]
    private GameObject achivementPrefab;
    [SerializeField]
    private LeaderboardYG leaderboard;
    [SerializeField]
    private TextMeshProUGUI bestScore;
    // Start is called before the first frame update
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        UpdateBestScore();
    }

    public void FillAchivementList()
    {
        if (YandexGame.savesData.achivements.Length == 0)
        {
            var slot = Instantiate(achivementPrefab, achivementListScrollViewContent.transform);
            var text = slot.GetComponentInChildren<TextMeshProUGUI>();
            text.text = "It`s also achivement that you have none";
        }
        foreach (var achivement in YandexGame.savesData.achivements)
        {
            var slot = Instantiate(achivementPrefab, achivementListScrollViewContent.transform);
            var text = slot.GetComponentInChildren<TextMeshProUGUI>();
            text.text = achivement;
        }
    }

    public void ClearAchivementList()
    {
        foreach (Transform child in achivementListScrollViewContent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void ReloadLeaderboard()
    {
        leaderboard.UpdateLB();
    }

    public void UpdateBestScore()
    {
        if (bestScore != null)
            bestScore.text = $"Best Score:{YandexGame.savesData.bestScore}";
    }
}
