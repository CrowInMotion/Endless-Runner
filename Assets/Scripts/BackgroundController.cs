using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    private Rigidbody2D bgRb;

    [SerializeField] 
    private float scrollSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBackground();
    }

    private void moveBackground()
    {
        //bgRb.linearVelocity = new Vector2(bgRb.linearVelocityX - bgSpeed, bgRb.linearVelocityY);
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BGDestroyer"))
        {
            //Debug.Log("Here");
            //Destroy(this.gameObject);
        }
    }
}
