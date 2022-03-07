using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallUnit : GameUnit, IContact
{
    [HideInInspector] public Wall wall;
    [HideInInspector] public Vector2Int index;
    public LayerMask layerMask;
    public MeshRenderer mesh;

    public ParticleSystem grassVFX;
    public GameObject grass;
    public GameObject ground;

    public void IsObstacle()
    {
        wall.IsObstacle(this);

        grass.SetActive(false);
        ground.SetActive(false);
    }

    public void Contact()
    {
        wall.Paint(this);
        grass.SetActive(false);
        grassVFX.Play();
    }

    public void Painted()
    {
        //fix lai material theo mau son
        SetMat(HousePaint.Level.Instance.newPaint);
    }

    public void SetMat(Material mat)
    {
        mesh.material = mat;
    }

}
