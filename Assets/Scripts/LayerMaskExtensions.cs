using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool Contains(this LayerMask mask, int layer)
    {
        return 0 != (mask.value & (1 << layer));
    }

    public static bool Contains(this LayerMask mask, GameObject obj)
    {
        return 0 != (mask.value & (1 << obj.layer));
    }

    public static bool Contains(this LayerMask mask, Component component)
    {
        return 0 != (mask.value & (1 << component.gameObject.layer));
    }
}
