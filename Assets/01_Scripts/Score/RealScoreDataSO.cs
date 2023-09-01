using UnityEngine;

[CreateAssetMenu(menuName = "SO/RealScoreData", fileName = "RealScoreData")]
public class RealScoreDataSO : ScriptableObject
{
    [Header("Personal")]
    public int Aces = 0;
    public int Deuces = 0;
    public int Threes = 0;
    public int Fours = 0;
    public int Fives = 0;
    public int Sixes = 0;

    public int Subtotal = 0; // 위에 aces ~ sixes 점수 총합 (63점 이상일 때 보너스 35점 추가)
    public int Choice = 0;     // (아무 조건 없이) 나온 눈 수 총합

    [Header("Same")]
    public int FourOfKind = 0; // (동일한 주사위 4개 있을 때) 나온 눈 수 총합
    public int FullHouse = 0;  // (동일한 주사위 3개 + 2개 있을 때) 나온 눈 수 총합
    public int Yacht = 0; // (동일한 주사위 5개) 고정 50점

    [Header("Row")]
    public int SmallStraight = 0; // (주사위 눈이 연속된 횟수가 4회일 때) 고정 15점
    public int LargeStraight = 0; // (주사위 눈이 연속된 횟수가 5회일 때) 고정 30점

    public int GetSubTotal() => Aces + Deuces + Threes + Fours + Fives + Sixes;

    public int GetAllTotal() => Subtotal + Choice + FourOfKind + FullHouse + Yacht + SmallStraight + LargeStraight;

    public int GetIWantProperty(string name)
    {
        var temp = GetType().GetField(name).GetValue(this);
        return (int)temp;
    }

    public void SetIWantProperty(string name, int value)
    {
        var temp = GetType().GetField(name);
        temp.SetValue(this, value);
    }

    public void ResetValue()
    {
        Aces = 0;
        Deuces = 0;
        Threes = 0;
        Fours = 0;
        Fives = 0;
        Sixes = 0;

        Subtotal = 0; 
        Choice = 0;     

        FourOfKind = 0; 
        FullHouse = 0;  
        Yacht = 0; 

        SmallStraight = 0; 
        LargeStraight = 0; 
    }
}
