using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DetectionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    
    private SphereCollider _sphereCollider;

    public Transform TargetTransform { get; private set; }
    public float Range => _sphereCollider.radius;

    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void OnTriggerEnter(Collider other) => DetectTarget(other);
    private void OnTriggerExit(Collider other) =>  UndetectTarget(other);
    
    private void CacheComponents()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void DetectTarget(Collider collider)
    {
        if(!_targetLayer.Contains(collider)) return;
        TargetTransform = collider.transform;
    }

    private void UndetectTarget(Collider collider)
    {
        if(!_targetLayer.Contains(collider)) return;
        TargetTransform = null;
    }

    private void Init()
    {
        TargetTransform = null;
    }
}
