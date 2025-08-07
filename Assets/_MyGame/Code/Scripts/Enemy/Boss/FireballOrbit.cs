using UnityEngine;

public class FireballOrbit : MonoBehaviour
{
    public Transform center;
    public float speed = 50f;
    public float radius;
    public float startingAngle = 0f; // Góc khởi đầu (tùy mỗi fireball)

    private float angle;

    private void Start()
    {
        angle = startingAngle;
    }

    private void Update()
    {
        if (center == null) return;

        angle += speed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(rad) * radius;
        float z = Mathf.Sin(rad) * radius;

        transform.position = center.position + new Vector3(x, 0, z);
    }
}