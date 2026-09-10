using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIController : MonoBehaviour
{
    public abstract void Init(GameUIManager uiManager);
    public abstract void OnActivate();
    public abstract void OnDeactivate();

}
