using System.Collections;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private SphereCollider[] _sphereCollider = new SphereCollider[6];

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _sphereCollider[i] = transform.GetChild(i).GetComponent<SphereCollider>();
        }
    }

    public void ActTrigger()
    {
        StartCoroutine(Cor_Trigger());
    }

    private IEnumerator Cor_Trigger()
    {
        ControllCollider(true);
        yield return new WaitForFixedUpdate();
        ControllCollider(false);
    }

    private void ControllCollider(bool isOn)
    {
        for (int i = 0; i < _sphereCollider.Length; i++)
        {
            _sphereCollider[i].enabled = isOn;
        }
    }
}
