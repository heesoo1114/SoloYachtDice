using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    public TextMeshProUGUI Text => _text;
    private TextAnimation _textAnimation;

    public int scoreKindId;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _textAnimation = GetComponent<TextAnimation>();
    }

    public void SetScoreText()
    {

    }

    public void StartScaleAnim(float targetScale, float speed)
    {
        _textAnimation.ScaleAnimPlay(targetScale, speed);
    }

}
