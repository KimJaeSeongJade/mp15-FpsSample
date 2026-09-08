using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IInteractable
{
    public GameObject GameObject { get => gameObject; }
    private Outline _outline;
    
    private void Awake() => CacheComponents();
    private void Start() => Init();
    
    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }

    public void Interact(IInteractor owner)
    {
        // onwer의 능력치를 상승시킨다던가...
        // 인벤토리로 들어간다던가....
        // 무기가 생긴다던가...
        // 장탄수를 리필해준다던가...

        Destroy(gameObject);
    }

    private void Init()
    {
        _outline.enabled = false;
    }

    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
