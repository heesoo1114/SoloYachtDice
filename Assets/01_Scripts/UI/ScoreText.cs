using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public TextMeshProUGUI TextMeshPro
    {
        get { return _text; }
        set { _text = value; }
    }
    private TextAnimation _textAnimation;

    private bool isCanPut = true;
    public bool IsCanPut
    {
        get => isCanPut;
        set
        {
            Debug.Log(value);
            isCanPut = value;
        }
    }

    private int score = 0;
    public int Score
    {
        get => score;
        set => score = value;
    }

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textAnimation = GetComponent<TextAnimation>();
    }

    private void Start()
    {
        isCanPut = true;
    }

    public void SetScoreText(int num)
    {
        isCanPut = false;
        score = num;

        _text.color = Color.black;
        _text.text = score.ToString();
    }

    public void StartScaleAnim(float targetScale, float speed)
    {
        _textAnimation.ScaleAnimPlay(targetScale, speed);
    }

}
