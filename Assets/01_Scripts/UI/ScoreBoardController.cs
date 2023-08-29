using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    private FakeScoreDataSO fakeScoreData;
    private RealScoreDataSO realScoreData;

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

    private void Awake()
    {
        fakeScoreData = Resources.Load<FakeScoreDataSO>("FakeScoreData");
        realScoreData = Resources.Load<RealScoreDataSO>("RealScoreData");
    }

    private void Start()
    {
        startFontSize = scoreKindList[0].TextMeshPro.fontSize;
    }

    private void Update()
    {
        if (isScoreBoardOn && !isChanging)
        {
            float inputV = Input.GetAxisRaw("Vertical");
            float inputH = Input.GetAxisRaw("Horizontal");

            if (inputH > 0)
            {
                // TODO: 스코어 보드 들어가고 다시 주사위 선택 나오는 함수 생성

                EndSelectScorekind();
                Debug.Log("다시 주사위 선택으로");
            }

            if (inputV != 0)
            {
                ChangeScoreKind(-inputV);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (_selectScoreKind != null)
                {
                    SetRealScore();
                    EndSelectScorekind();
                }
            }
        }
    }

    #region Select

    [ContextMenu("On")]
    public void StartSelectScoreKind()
    {
        isScoreBoardOn = true;
        
        // 여기서 넣을 수 있는 점수들 넣어서 텍스트로 보여주기
        foreach (var scoreKind in scoreKindList)
        {
            string name = scoreKind.gameObject.name;
            int temp = fakeScoreData.GetIWantProperty(name);

            scoreKind.TextMeshPro.text = temp.ToString();
            
            // Debug.Log($"{name} + {temp}");
        }

        _selectScoreKind = scoreKindList[initIndex];
        ChangeScoreKind();
    }

    public void SetRealScore()
    { 
        // _selectScoreKind.IsCanPut 이거 값 항상 false임

        if (!_selectScoreKind.IsCanPut) return;

        string name = _selectScoreKind.gameObject.name;
        int num = fakeScoreData.GetIWantProperty(name);
        realScoreData.SetIWantProperty(name, num);

        _selectScoreKind.SetScoreText(num);
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
