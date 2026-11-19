using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private bool isBulletMoving = false;

    [SerializeField]
    private float bulletSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
    }

    private void moveBullet()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
