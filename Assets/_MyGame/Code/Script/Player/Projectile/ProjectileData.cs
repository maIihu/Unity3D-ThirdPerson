
using UnityEngine;

[CreateAssetMenu(fileName = ("ProjectileData"), menuName = "ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float damage;
    public float lifeTime;
    public float speed;
    public float cooldown;
}
