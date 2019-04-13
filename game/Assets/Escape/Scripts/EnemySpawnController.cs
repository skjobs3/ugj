using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController: MonoBehaviour
{
    public GameObject spawnableEnemy;
    public float spawnInterval;
    public Vector2 restrictedArea;

    private float levelTime = 0.0f;


    Vector2 generateSpawnPosition()
    {
        var randomizeCornerX = Mathf.Sign(Random.Range(-1.0f, 1.0f));
        var randomizeCornerY = Mathf.Sign(Random.Range(-1.0f, 1.0f));
        Vector2 cornerRandomization = new Vector2(restrictedArea.x * randomizeCornerX, restrictedArea.y * randomizeCornerY);
        
        var randomizeShiftSide = Mathf.Sign(Random.Range(-1.0f, 1.0f));

        Vector2 instantiatePosition = new Vector2();
        if (randomizeShiftSide > 0.0f)
        {
            //shift x coord
            instantiatePosition.x = Random.Range(-restrictedArea.x, restrictedArea.x);
            instantiatePosition.y = cornerRandomization.y;
        }
        else
        {
            //shift y coord
            instantiatePosition.x = cornerRandomization.x;
            instantiatePosition.y = Random.Range(-restrictedArea.y, restrictedArea.y);
        }

        return instantiatePosition;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

  
    // Update is called once per frame
    void Update()
    {
        levelTime += Time.deltaTime;

        if(levelTime > spawnInterval)
        {
            Vector2 instantiatePosition = generateSpawnPosition();

            var spawnedEnemy = Instantiate(spawnableEnemy);
            spawnedEnemy.transform.position = new Vector3(instantiatePosition.x, instantiatePosition.y, spawnedEnemy.transform.position.z);
            levelTime = 0.0f;
        }
    }
}
