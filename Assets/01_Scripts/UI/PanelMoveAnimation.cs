using System.Collections;
using UnityEngine;

public class PanelMoveAnimation : MonoBehaviour
{
    private RectTransform _rectTr;

    [SerializeField] private RectTransform targetRectTr;
    [SerializeField] private RectTransform initRectTr;

    [SerializeField] private float moveSpeed = 10f;

    private bool isShow = false;

    private void Awake()
    {
        _rectTr = GetComponent<RectTransform>();
    } 

    public void PlayMoveAnim()
    {
        if (isShow) return;
        StartCoroutine(MoveAnim(targetRectTr, moveSpeed));
    }

    public void ResetMoveAnim()
    {
        if (isShow) return;
        StartCoroutine(MoveAnim(initRectTr, moveSpeed));
    }

    private IEnumerator MoveAnim(RectTransform targetTr, float speed)
    {
        isShow = true;

        float moveTime = 0;
        float value = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            value = moveTime / speed;

            _rectTr.position = Vector3.Lerp(_rectTr.position, targetTr.position, value);
            Debug.Log(_rectTr.position);

            if (value >= 0.1f)
            {
                _rectTr.position = targetTr.position;
                isShow = false;
                yield break;
            }

            yield return null;
        }
    }
}
