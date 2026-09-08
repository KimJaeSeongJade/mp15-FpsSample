using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractor
{
    public GameObject GameObject { get; }

    public void TryInteract();
}
