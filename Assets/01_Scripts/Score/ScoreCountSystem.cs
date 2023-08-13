using System.Collections;
using UnityEngine;
using System;

public class ScoreCountSystem : MonoBehaviour
{
    [SerializeField] private DiceManager _diceManager;
    [SerializeField] private FakeScoreDataSO fakeScoreData;

    private int[] numList = new int[5];
    private int[] numCntList = new int[6];

    private int[] arr = new int[6]; // temp 역할
    private int sum = 0;

    private int bonusScore = 35;
    private int ssFixedScore = 15;
    private int lsFixedScore = 30;
    private int yachtFixedScore = 50;

    private bool isCounted = false; // 점수 세는 중인지 아닌지

    private void Start()
    {
        _diceManager.rollDoneEvent += SetAndCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            fakeScoreData.ResetProperty();
        }
    }

    #region Flow

    private void SetAndCount()
    {
        StartCoroutine(Col_SetAndCount());
    }

    private IEnumerator Col_SetAndCount()
    {
        isCounted = true;
        yield return SetNumInfo();
        yield return ScoreAllTypeCount();
        yield return ResetScoreInfO();
        isCounted = false;
    }

    // 현재 주사위 숫자 정보 세팅
    private IEnumerator SetNumInfo()
    {
        // 1. List와 배열들 정보 세팅
        // 2. 주사위 눈이 각각 몇 개가 있는지 체크 (SetNumInfo에서 실행)
        // 3. 주사위 눈 합
        int n = 0;
        for (int i = 0; i < _diceManager.DiceInfo.Length; i++)
        {
            n = _diceManager.DiceInfo[i];

            numList[i] = n;
            numCntList[n - 1]++;
            sum += n;
        }

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator ResetScoreInfO()
    {
        sum = 0;
        Array.Clear(numList, 0, numList.Length);
        Array.Clear(numCntList, 0, numCntList.Length);

        yield return new WaitForEndOfFrame();
    }

    #endregion

    #region CountScore

    private IEnumerator ScoreAllTypeCount()
    {
        PersonalScoreTypeCount();
        SameScoreTypeCount();
        RowTypeScoreCount();
        yield return new WaitForEndOfFrame();
    }

    private void PersonalScoreTypeCount()
    {
        int num = 0;
        for (int i = 0; i < numCntList.Length; i++)
        {
            num = numCntList[i];
            fakeScoreData.cntScoreList[i] = num * (i + 1);
        }

        // subtotal 부분 수정 필요 (활성화 되어 있는 애들만 + 해주기)
        // for (int i = 0; i < fakeScoreData.cntScoreList.Length; i++)
        // {
        //     fakeScoreData.subtotal += fakeScoreData.cntScoreList[i];
        // }
    }

    private void SameScoreTypeCount()
    {
        fakeScoreData.choice = sum;

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = numCntList[i];
        }
        
        Array.Sort(arr);

        int max = arr[arr.Length - 1];
        int min = arr[arr.Length - 2];

        // Debug.Log("max : " + max);
        // Debug.Log("min : " + min);

        if (max >= 4)
        {
            fakeScoreData.fourOfKind = sum;
            Debug.Log("fourofkind");

            if (max == 5)
            {
                Debug.Log("yacht");
                fakeScoreData.yacht = yachtFixedScore;
            }
        }
        else if (max == 3)
        {
            if (min == 2)
            {
                Debug.Log("fullhouse");
                fakeScoreData.fullHouse = sum;
            }
        }
    }

    private void RowTypeScoreCount()
    {
        int cnt = 0;
        for (int i = 0; i < numCntList.Length; i++)
        {
            if (i == numCntList.Length - 1)
            {
                if (numCntList[i] != 0) cnt++;
                break;
            }

            if (numCntList[i] == 0) cnt = 0;
            else cnt++;
        }

        // Debug.Log("cnt : " + cnt);

        if (cnt >= 4)
        {
            Debug.Log("smallStraight");
            fakeScoreData.smallStraight = ssFixedScore;

            if (cnt == 5)
            {
                Debug.Log("largeStraight");
                fakeScoreData.largeStraight = lsFixedScore;
            }
        }
    }

    #endregion

}
