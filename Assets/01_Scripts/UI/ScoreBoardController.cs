using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private List<ScoreText> scoreKindList;

    private bool isScoreBoardOn = false;

    private ScoreText _selectScoreKind;

    // select
    private bool isChanging;
    private int selectedIndex = 0;
    private int initIndex = 0;

    [Header("Anim")]
    [SerializeField] private float EndFontSize;
    [SerializeField] private float scaleAnimSpeed = 1f;
    private float startFontSize;

    private void Start()
    {
        startFontSize = scoreKindList[0].Text.fontSize;

        StartSelectScoreKind();
    }

    private void Update()
    {
        if (isScoreBoardOn && !isChanging)
        {
            float inputV = Input.GetAxisRaw("Vertical");
            float inputH = Input.GetAxisRaw("Horizontal");

            if (inputH > 0)
            {
                // 스코어 보드 들어가고 다시 주사위 선택 나오는 함수 생성

                EndSelectScorekind();
                Debug.Log("다시 주사위 선택으로");
            }

            if (inputV != 0)
            {
                ChangeScoreKind(-inputV);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_selectScoreKind != null)
                {
                    // 점수 넣어주고 다음 턴으로 넘어가는 함수 생성 

                    EndSelectScorekind();
                    Debug.Log("점수 넣어주기");
                }
            }
        }
    }

    #region Select

    public void StartSelectScoreKind()
    {
        isScoreBoardOn = true;

        _selectScoreKind = scoreKindList[initIndex];
        ChangeScoreKind();
    }

    // 주사위 돌릴 준비 마쳤을 때 실행
    public void EndSelectScorekind()
    {
        if (_selectScoreKind != null)
        {
            _selectScoreKind.StartScaleAnim(startFontSize, scaleAnimSpeed);
            _selectScoreKind = null;
            selectedIndex = initIndex;
        }
        isScoreBoardOn = false;
    }

    private void ChangeScoreKind(float axis = 0)
    {
        StartCoroutine(Col_ChangeScoreKind(selectedIndex + (int)axis));
    }

    private IEnumerator Col_ChangeScoreKind(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= scoreKindList.Count) yield break;

        isChanging = true;

        _selectScoreKind.StartScaleAnim(startFontSize, scaleAnimSpeed);
        _selectScoreKind = scoreKindList[currentIndex];
        _selectScoreKind.StartScaleAnim(EndFontSize, scaleAnimSpeed);

        selectedIndex = currentIndex;

        yield return new WaitForSeconds(scaleAnimSpeed);
        isChanging = false;
    }

    #endregion
}
