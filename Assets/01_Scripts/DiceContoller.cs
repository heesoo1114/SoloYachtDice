using UnityEditor.Purchasing;
using UnityEngine;

public class DiceContoller : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private TriggerController _triggerController;

    private int diceNum = 0;
    public int DiceNum => diceNum;

    [Header("Value")]
    [SerializeField] private bool isNeedCheck = false; // 주사위 눈 확인했는지 안 했는지
    [SerializeField] private bool isGround = false;    // 땅인지 아닌지
    [SerializeField] private float speed = 10;

    private Vector3 initPos;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _triggerController = transform.GetChild(0).GetComponent<TriggerController>();
    }

    private void Start()
    {
        initPos = transform.localPosition;
        _rigidBody.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
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
        if (isGround)
        {
            diceNum = 0;
            isGround = false;
            isNeedCheck = true;

            _rigidBody.AddTorque(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360));
            _rigidBody.velocity = Vector3.up * speed;
        }
    }

    public void Reset()
    {
        isNeedCheck = false;
        isGround = false;
        diceNum = 0;
        transform.localPosition = initPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

}