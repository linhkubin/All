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

    public void IsObstacle()
    {
        wall.IsObstacle(this);
    }

    public void Contact()
    {
        wall.Paint(this);
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
