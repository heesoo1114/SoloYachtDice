using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void ScaleAnimPlay(float scale, float speed)
    {
        StartCoroutine(ScaleAnim(scale, speed));
    }

    private IEnumerator ScaleAnim(float scale, float speed)
    {
        float targetScale = scale;
        float moveTime = 0;
        float value = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            value = moveTime / speed;

            _text.fontSize = Mathf.Lerp(_text.fontSize, targetScale, value);

            if (value >= 0.8f)
            {
                _text.fontSize = targetScale;
                yield break;
            }

            yield return null;
        }
    }
}
