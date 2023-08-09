using System.Collections;
using UnityEngine;

public class DiceContoller : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private TriggerController _triggerController;

    private Vector3 initPos;

    private int diceNum = 0;
    public int DiceNum => diceNum;

    [Header("Value")]
    [SerializeField] private bool isNeedCheck = false; // 주사위 눈 확인했는지 안 했는지
    [SerializeField] private bool isGround = false;    // 땅인지 아닌지
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float animSpeed = 1;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _triggerController = transform.GetChild(0).GetComponent<TriggerController>();
    }

    private void Start()
    {
        initPos = transform.localPosition;
    }

    private void Update()
    {
        if (_rigidBody.IsSleeping() && isGround & isNeedCheck)
        {
            _triggerController.ActTrigger();
            isNeedCheck = false;
        }
    }

    public void SetDiceNum(int numValue)
    {
        diceNum = numValue;
    }

    public bool IsReady()
    {
        return isGround & !isNeedCheck;
    }

    public void RollCube()
    {
        diceNum = 0;
        isGround = false;
        isNeedCheck = true;

        _rigidBody.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        _rigidBody.velocity = Vector3.up * jumpPower;
    }

    public void Reset()
    {
        isNeedCheck = false;
        isGround = true;
        diceNum = 0;

        Vector3 nowAngle = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        transform.localEulerAngles = nowAngle;

        StartCoroutine(ResetMoveRotAnim(initPos));
    }

    private IEnumerator ResetMoveRotAnim(Vector3 targetPos)
    {
        float moveTime = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            float value = moveTime / animSpeed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, value);

            if (value >= 1f)
            {
                transform.localPosition = targetPos;
                yield break;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

}