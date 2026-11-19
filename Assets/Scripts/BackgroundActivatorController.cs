using UnityEngine;

public class BackgroundActivatorController : MonoBehaviour
{
    public GameObject bgManager;
    private BackgroundManager bgManagerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgManagerScript = bgManager.GetComponent<BackgroundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))
        {
            bgManagerScript.createNewBG();
        }

        else
        {
            Destroy(collision.gameObject);
            Debug.Log("Bullet Destroyed");
        }
    }
}
