using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/RealScoreData", fileName = "RealScoreData")]
public class RealScoreDataSO : ScriptableObject
{
    [Header("Personal")]
    // public int[] cntScoreList = new int[6];
    public int aces = 0;
    public int deuces = 0;
    public int threes = 0;
    public int fours = 0;
    public int fives = 0;
    public int sixes = 0;

    public int subtotal = 0; // 위에 aces ~ sixes 점수 총합 (63점 이상일 때 보너스 35점 추가)
    public int choice = 0;     // (아무 조건 없이) 나온 눈 수 총합

    [Header("Same")]
    public int fourOfKind = 0; // (동일한 주사위 4개 있을 때) 나온 눈 수 총합
    public int fullHouse = 0;  // (동일한 주사위 3개 + 2개 있을 때) 나온 눈 수 총합
    public int yacht = 0; // (동일한 주사위 5개) 고정 50점

    [Header("Row")]
    public int smallStraight = 0; // (주사위 눈이 연속된 횟수가 4회일 때) 고정 15점
    public int largeStraight = 0; // (주사위 눈이 연속된 횟수가 5회일 때) 고정 30점

    private int allTotal = 0;

    public int GetAllTotal()
    {
        allTotal = aces + deuces + threes + fours + fives + sixes + subtotal + choice + fourOfKind + fullHouse + yacht + smallStraight + largeStraight; ;
        return allTotal;
    }

    public void ResetProperty()
    {
        // Array.Clear(cntScoreList, 0, cntScoreList.Length);

        aces = 0;
        deuces = 0;
        threes = 0;
        fours = 0;
        fives = 0;
        sixes = 0;

        subtotal = 0; // 여기 추후 수정
        choice = 0;

        fourOfKind = 0;
        fullHouse = 0;
        yacht = 0;

        smallStraight = 0;
        largeStraight = 0;
    }

   
}
