using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject background;
    private GameObject newBg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBg();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawnBg()
    {
        newBg = Instantiate(background);
        newBg.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
    }
}
