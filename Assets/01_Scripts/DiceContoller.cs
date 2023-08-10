using System.Collections;
using UnityEngine;

public class DiceContoller : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private TriggerController _triggerController;

    private Vector3 initPos;

    private int diceNum = 0;
    public int DiceNum => diceNum;

    private bool isNeedCheck = false; // 주사위 눈 확인했는지 안 했는지
    private bool isGround = false;    // 땅인지 아닌지

    [Header("Value")]
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private float animSpeed = 1;
    public float AnimSpeed => animSpeed;

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
        if (_rigidBody.IsSleeping() && isGround && isNeedCheck)
        {
            _triggerController.ActTrigger();
            isNeedCheck = false;
        }
    }

    // DiceNumTrigger의 GetDiceNumEvent에 연결되어 있음
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

    public void ResetCube()
    {
        isNeedCheck = false;
        isGround = true;
        diceNum = 0;

        // // 현재 로컬 회전 값을 가져옴
        // Vector3 originalRotation = transform.localEulerAngles;
        // 
        // // y축 회전 값만 남기고 다른 축은 초기화
        // Vector3 newRotationEuler = new Vector3(originalRotation.x, 0, originalRotation.z);
        // 
        // // 새로운 Quaternion을 생성하여 회전을 조작
        // Quaternion newRotationQuaternion = Quaternion.Euler(newRotationEuler);
        // transform.localRotation = newRotationQuaternion;

        StartCoroutine(MoveAnim(initPos));
    }

    private IEnumerator MoveAnim(Vector3 targetPos)
    {
        float moveTime = 0;
        float value = 0;

        while (true)
        {
            moveTime += Time.deltaTime;
            value = moveTime / animSpeed;

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, value);

            if (value >= 0.8f)
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