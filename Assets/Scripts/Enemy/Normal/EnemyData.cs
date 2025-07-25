using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class EnemyData : ScriptableObject
{
   public float health;
   public float damage;
   public float attackRange;
   public float attackCooldown;
}
