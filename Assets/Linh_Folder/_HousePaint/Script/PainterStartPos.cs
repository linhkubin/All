using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterStartPos : MonoBehaviour
{
    public Transform child;
    public Vector2Int index;

    public Vector2Int GetIndex()
    {
        Collider[] colliders = Physics.OverlapSphere(child.position, .1f);

        if (colliders.Length > 0)
        {
            WallUnit wallUnit = colliders[0].GetComponent<WallUnit>();

            return wallUnit.index;
        }

        return Vector2Int.zero;
    }
}
