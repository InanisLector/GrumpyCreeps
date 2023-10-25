using System;
using UnityEngine;

[Serializable]
public struct SplineSegment
{
    [field : SerializeField] public Transform StartPoint { get; private set;}
    [field : SerializeField] public Transform FirstInterpolationPoint { get; private set; }
    [field : SerializeField] public Transform SecondInterpolationPoint { get; private set; }
    [field : SerializeField] public Transform EndPoint { get; private set; }
}
