using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetTransform.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        Debug.Log(cutoutPos);

        Vector3 offset = targetTransform.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for(int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
            }
        }
    }
}
