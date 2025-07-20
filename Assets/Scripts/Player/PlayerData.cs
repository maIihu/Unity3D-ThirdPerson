using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerData : ScriptableObject
{
    public float health;
    public float damage;
    public float exp;
    public float level;
    public float moveSpeed;
    public float jumpForce;
    public float interactDistance;

}
