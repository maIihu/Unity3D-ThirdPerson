
using UnityEngine;

public class BlueBallProjectile : PlayerProjectile
{
    private void Update()
    {
        if(IsFlying) ProjectileFly();
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable target))
        {
            if (CharacterOwnerType != target.GetCharacterType)
            {
                target.TakeDamage(data.damage);
            }
            else return;
        }
        //Destroy(gameObject);
        TriggerHitEffect(other.transform.position);
    }
}
