using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ScoreData", fileName = "ScoreData")]
public class ScoreDataSO : ScriptableObject
{
    // Count
    // public int aces = 0;
    // public int deuces = 0;
    // public int threes = 0;
    // public int fours = 0;
    // public int fives = 0;
    // public int sixes = 0;
    [Header("Personal")]
    public int[] cntScoreList = new int[6];
    public int subtotal = 0; // 위에 aces ~ sixes 점수 총합 (63점 이상일 때 보너스 35점 추가)
    public int choice = 0;     // (아무 조건 없이) 나온 눈 수 총합

    // Sum
    [Header("Same")]
    public int fourOfKind = 0; // (동일한 주사위 4개 있을 때) 나온 눈 수 총합
    public int fullHouse = 0;  // (동일한 주사위 3개 + 2개 있을 때) 나온 눈 수 총합
    public int yacht = 0; // (동일한 주사위 5개) 고정 50점

    // Fixed
    [Header("Row")]
    public int smallStraight = 0; // (주사위 눈이 연속된 횟수가 4회일 때) 고정 15점
    public int largeStraight = 0; // (주사위 눈이 연속된 횟수가 5회일 때) 고정 30점
}
