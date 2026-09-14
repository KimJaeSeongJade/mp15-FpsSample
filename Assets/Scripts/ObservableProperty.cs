using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObservableProperty<T>
{
    private Action<T> _onValueChanged;
    private T _value;
    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            Notify();
        }
    }
    
    public void AddListener(Action<T> onValueChanged)
    {
        _onValueChanged += onValueChanged;
    }

    public void RemoveListener(Action<T> onValueChanged)
    {
        _onValueChanged -= onValueChanged;
    }
    
    public void RemoveAllListeners()
    {
        _onValueChanged = null;
    }

    public void Notify()
    {
        _onValueChanged?.Invoke(_value);
    }
    
    public ObservableProperty(T initialValue)
    {
        _value = initialValue;
    }
}



















