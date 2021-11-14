using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HousePaint
{
    public class CameraControl : Singleton<CameraControl>
    {
        public Transform tf;

        public void SetPoint(Vector3 pos, Quaternion rot)
        {
            tf.SetPositionAndRotation(pos, rot);
        }
    }
}