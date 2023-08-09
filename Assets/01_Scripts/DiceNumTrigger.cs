using UnityEngine;
using UnityEngine.Events;

public enum NumKey
{
    None = 0,
    One = 6,
    Two = 5,
    Three = 4,
    Four = 3,
    Five = 2,
    Six = 1
}

public class DiceNumTrigger : MonoBehaviour
{
    public NumKey numKey = NumKey.None;

    private int numValue = 0;

    public UnityEvent<int> GetDiceNumEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            numValue = (int)numKey;
            GetDiceNumEvent?.Invoke(numValue);
        }
    }
}
