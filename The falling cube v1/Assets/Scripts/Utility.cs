using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static int Sign(float input)
    {
        return input == 0 ? 0 : input > 0 ? 1 : -1;
    }

    public static T Choose<T>(params T[] input)
    {
        return input[UnityEngine.Random.Range(0, input.Length)];
    }
}
