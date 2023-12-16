using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float max;
    GameObject bar;
    void Start()
    {
        bar = transform.GetChild(0).gameObject;
    }



    public void updateBar(float value)
    {
        float theNormal = Mathf.Clamp(value/max, 0, 1);
        bar.transform.localScale = new Vector3(theNormal,
                                               bar.transform.localScale.y,
                                               bar.transform.localScale.z);
    }
}
