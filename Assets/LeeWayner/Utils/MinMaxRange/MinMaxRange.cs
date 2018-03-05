using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxRangeAttribute : PropertyAttribute
{
    public int minLimit, maxLimit;

    public MinMaxRangeAttribute(int minLimit, int maxLimit)
    {
        this.minLimit = minLimit;
        this.maxLimit = maxLimit;
    }
}

[System.Serializable]
public class MinMaxRange
{
    public int rangeStart, rangeEnd;

    public int GetRandomValue()
    {
        return Random.Range(rangeStart, rangeEnd + 1);
    }
}
