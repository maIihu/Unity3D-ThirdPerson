
    using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }
}
