using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{
    public float CurrentHealth { get; }
    public float MaxHealth { get; }
    public event Action<float, float> OnHealthChanged;
}
