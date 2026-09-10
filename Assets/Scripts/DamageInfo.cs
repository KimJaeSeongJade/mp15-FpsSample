using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public IDamageable Target;
    public Vector3 HitPoint;
    public int Value;
    public bool IsValid;

    public DamageInfo(IDamageable target, int value, Vector3 hitPoint)
    {
        Target = target;
        Value = value;
        HitPoint = hitPoint;
        IsValid = true;
    }
}
