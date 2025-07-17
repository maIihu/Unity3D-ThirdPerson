using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Player, Enemy, Shield    
}

public interface IAttackable
{
    public Team GetTeam();
    public void TakeDamage(float damage);
}
