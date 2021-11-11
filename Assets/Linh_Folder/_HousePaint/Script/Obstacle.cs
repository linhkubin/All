using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
