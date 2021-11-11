using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Ultities
{
    /// <summary>
    /// (floor, ceiling) -> (min, max)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="floor"></param>
    /// <param name="ceiling"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float GetValueEquivalent(float value, float floor, float ceiling, float min, float max)
    {
        return (value - floor) * (max - min) / (ceiling - floor) + min;
    }
}
