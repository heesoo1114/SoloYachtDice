using System.Collections;
using UnityEngine;

public class DiceContoller : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private TriggerController _triggerController;
    private DiceAnimation _diceAnim;
    private Outline _outLine;

    private Vector3 initPos;
    private float initScale;
    private Quaternion initRot;

    private int diceNum = 0;
    public int DiceNum => diceNum;

    [Header("Value")]
    [SerializeField] private float jumpPower = 10;
    [SerializeField] private bool isNeedCheck = false; // 주사위 눈 확인했는지 안 했는지
    [SerializeField] private bool isGround = false;    // 땅인지 아닌지
    [SerializeField] private bool isKeep = false;      // 킵한 주사위인지 아닌지

    [Header("Anim")]
    [SerializeField] private float moveAnimSpeed = 1;
    [SerializeField] private float selectAnimSpeed = 1;
    [SerializeField] private float selectAnimTargetScale = 1.2f;
    public float MoveAnimSpeed => moveAnimSpeed;
    public float SelectAnimSpeed => selectAnimSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _outLine = GetComponent<Outline>();
        _diceAnim = GetComponent<DiceAnimation>();
        _triggerController = transform.GetChild(0).GetComponent<TriggerController>();
    }

    private void Start()
    {
        initPos = transform.localPosition;
        initScale = transform.localScale.x;
        initRot = transform.localRotation;
    }

    private void Update()
    {
        if (_rigidBody.IsSleeping() && isGround && isNeedCheck)
        {
            _triggerController.ActTrigger();
            isNeedCheck = false;
        }
    } 

    #region 접근

    // DiceNumTrigger의 GetDiceNumEvent에 연결되어 있음
    public void SetDiceNum(int numValue) 
    {
        diceNum = numValue;
    }

    public void SetDiceKeepOrOut()
    {
        isKeep = (isKeep) ? false : true;
        _outLine.enabled = isKeep;
        _rigidBody.isKinematic = isKeep;
    }

    public bool IsKeep() => isKeep;

    public bool IsReady()
    {
        return isGround && !isNeedCheck;
    }

    #endregion

    #region Controll 함수

    public void InitCube()
    {
        transform.localRotation = initRot;

        isNeedCheck = false;
        isGround = true;

        if (isKeep)
        {
            isKeep = false;
            _outLine.enabled= isKeep;
            _rigidBody.isKinematic = isKeep;
        }

    }

    public void RollCube()
    {
        if (isKeep) return;

        diceNum = 0;
        isGround = false;
        isNeedCheck = true;

        _rigidBody.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
        _rigidBody.velocity = Vector3.up * jumpPower;
    }

    public void ResetCube()
    {
        if (isKeep) return;

        isNeedCheck = false;
        isGround = true;

        // rotation 부분
        Vector3 nowAngle = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        transform.localEulerAngles = nowAngle;

        _diceAnim.MoveAnimPlay(initPos, moveAnimSpeed);
    }

    #endregion

    #region 선택 애니메이션

    public void SelectScaleAnimPlay()
    {
        _diceAnim.ScaleAnimPlay(selectAnimTargetScale, selectAnimSpeed);
    }

    public void SelectScaleAnimStop()
    {
        _diceAnim.ScaleAnimPlay(initScale, selectAnimSpeed);
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

}