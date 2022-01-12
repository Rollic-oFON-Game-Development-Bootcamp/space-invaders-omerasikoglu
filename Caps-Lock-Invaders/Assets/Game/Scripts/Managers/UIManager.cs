using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    private TextMeshProUGUI textMesh;
    private Transform inGameUI;

    private int currentScore = 0;

    protected void Awake()
    {
        Init();
        UpdateScore();
    }

    private void Init()
    {
        inGameUI = transform.Find(StringData.INGAME_UI);
        textMesh = inGameUI.Find(StringData.BACKGROUND).Find(StringData.TEXT).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore(int amount = 0)
    {
        currentScore += amount;
        textMesh.SetText(currentScore.ToString());
    }

    public void SetActiveInGameUI()
    {
        inGameUI.gameObject.SetActive(true);
    }
}