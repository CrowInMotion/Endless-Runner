using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject bgSpawner;
    public GameObject bgDestroyer;
    private BackgroundSpawner bgSpawnScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgSpawnScript = bgSpawner.GetComponent<BackgroundSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createNewBG()
    {
        bgSpawnScript.spawnBg();
    }
}
