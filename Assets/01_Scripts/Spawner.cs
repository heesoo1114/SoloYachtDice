using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    [SerializeField]
    private LayerMask _whatIsGround;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = transform;
            obj.transform.position = GetMouseWorldPosision();
        }
    }

    public Vector3 GetMouseWorldPosision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, Camera.main.farClipPlane, _whatIsGround);
        if (result)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
