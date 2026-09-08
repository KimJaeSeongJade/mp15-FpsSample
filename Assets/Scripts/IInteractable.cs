using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public GameObject GameObject { get; }

    public void Targeting();
    public void Untargeting();

    public void Interact(IInteractor owner);
}
