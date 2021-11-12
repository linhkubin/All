using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof( BoxCollider))]
[RequireComponent(typeof( Rigidbody))]
public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        WallUnit wallUnit = other.GetComponent<WallUnit>();

        if (wallUnit != null)
        {
            wallUnit.IsObstacle();
        }
    }
}
