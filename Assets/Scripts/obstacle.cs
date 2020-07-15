using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private float halfWindWidth, halfWindHeight;
    private float spawnTime = 2f;
    private GameObject newObstacle;
    public float difficultyLevel, timeBtwnObstacles, yOffset, destroyTimer;
    public Vector2 directionSpawn;
    public Vector2 minMaxSecondsToSpawn, minMaxVelocity;
    // float accumulatedTime = 0; public float easiness;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        halfWindHeight = Camera.main.orthographicSize;
        halfWindWidth = halfWindHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        /* float currentTime = Time.time - accumulatedTime;
        if (currentTime > easiness) {
            CreateObstacle();
            accumulatedTime += easiness;
            currentTime = currentTime - easiness;

            if (easiness > 0.4f) {
                easiness -= 0.05f;
            }
        } */
        
        if (Time.timeSinceLevelLoad > spawnTime) {
            timeBtwnObstacles = Mathf.Lerp(minMaxSecondsToSpawn.y, minMaxSecondsToSpawn.x, Difficulty.GetDifficultyRate(difficultyLevel));
            spawnTime = Time.timeSinceLevelLoad + timeBtwnObstacles;

            CreateObstacle();
        } 
        
    }

    void CreateObstacle() {
        Vector3 size = new Vector3(Random.Range(0.5f, 3f), Random.Range(0.2f, 5f), 2f);
        Vector3 position = new Vector3(Random.Range(-halfWindWidth, halfWindWidth), Random.Range(halfWindHeight, halfWindHeight + 0.2f) + yOffset, 0);
        Vector3 rotation = new Vector3 (0, 0, Random.Range(0,360));
        
        newObstacle = Instantiate(obstaclePrefab, position, Quaternion.Euler(rotation));
        newObstacle.transform.localScale = size;
        newObstacle.transform.parent = this.transform;
        rb = newObstacle.GetComponent<Rigidbody>();
        float yForce = Mathf.Lerp(minMaxVelocity.x, minMaxVelocity.y, Difficulty.GetDifficultyRate(difficultyLevel));

        rb.AddForce(new Vector3 (Random.Range(directionSpawn.x, directionSpawn.y), -yForce, 0), ForceMode.Impulse);

        Destroy(newObstacle, destroyTimer);  
    }
}
