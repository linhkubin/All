using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    public Transform[] list;

    public Material mat;

    public Vector3 space;

    public void Sort()
    {
        float x = 0;
        float z = 0;

        for (int i = 0; i < list.Length; i++)
        {
            x = i % 10 * space.x;
            z = i / 10 * space.z;

            list[i].position = Vector3.right * x + Vector3.forward * z;
        }

    }

    public void ChangeMat()
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].GetComponentInChildren<MeshRenderer>().material = mat;
        }
    }
}
