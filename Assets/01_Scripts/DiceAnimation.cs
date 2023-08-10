using System.Collections;
using UnityEngine;

public class DiceAnimation : MonoBehaviour
{
    public void MoveAnimPlay(Vector3 targetPos, float speed)
    {
        StartCoroutine(MoveAnim(targetPos, speed));
    }

    public void ScaleAnimPlay(float scale, float speed)
    {
        StartCoroutine(ScaleAnim(scale, speed));
    }

    private IEnumerator MoveAnim(Vector3 targetPos, float speed)
    {
        float moveTime = 0;
        float value = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            value = moveTime / speed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, value);

            if (value >= 0.8f)
            {
                transform.localPosition = targetPos;
                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator ScaleAnim(float scale, float speed)
    {
        Vector3 targetScale = new Vector3(scale, scale, scale);
        float moveTime = 0;
        float value = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            value = moveTime / speed;

            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, value);

            if (value >= 0.8f)
            {
                transform.localScale = targetScale;
                yield break;
            }

            yield return null;
        }
    }
}
