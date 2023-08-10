using System.Collections;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private DiceContoller[] _diceController = new DiceContoller[5];

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _diceController[i] = transform.GetChild(i).GetComponent<DiceContoller>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AllDiceRoll();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AllDiceReset();
        }
    }

    private bool AllDiceReady()
    {
        foreach (DiceContoller _controller in _diceController)
        {
            if (!_controller.IsReady())
            {
                return false;
            }
        }
        return true;
    }

    public void AllDiceRoll()
    {
        if (!AllDiceReady()) return;

        foreach (DiceContoller _controller in _diceController)
        {
            _controller.RollCube();
        }

        StartCoroutine(LaterRollCheck());
    }

    private IEnumerator LaterRollCheck()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            if (AllDiceReady())
            {
                if (!IsCorrectRoll())
                {
                    Debug.Log("is not correct");
                    AllDiceReset();
                    yield return new WaitForSeconds(_diceController[0].AnimSpeed);
                    AllDiceRoll();
                }
                else
                {
                    Debug.Log("is correct");
                }

                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AllDiceReset()
    {
        if (!AllDiceReady()) return;

        foreach (DiceContoller _controller in _diceController)
        {
            _controller.ResetCube();
        }
    }

    public bool IsCorrectRoll() // 주사위 중 하나라도 이상한 상태인 경우
    {
        foreach (DiceContoller _controller in _diceController)
        {
            if (_controller.DiceNum == 0)
            {
                return false;
            }
        }
        return true;
    }
}
