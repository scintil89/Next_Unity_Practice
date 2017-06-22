using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3.0f;

    float deltaSpawnTime = 0.0f;

    GameObject[] enemyPool = null;
    int poolSize = 10;

    void Start()
    {
        enemyPool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; ++i)
        {
            enemyPool[i] = Instantiate(enemy) as GameObject;
            enemyPool[i].name = "Enemy_"  + i;
            enemyPool[i].SetActive(false);
        }
    }//end of Start()

    void Update()
    {
        deltaSpawnTime += Time.deltaTime;

        if (deltaSpawnTime > spawnTime)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 5)
            {
                deltaSpawnTime = 0.0f;

                //GameObject enemyObj = Instantiate(enemy) as GameObject;

                for (int i = 0; i < poolSize; ++i)
                {
                    GameObject enemyObj = enemyPool[i];
                    if (enemyObj.activeSelf == true)
                        continue;

                    enemyObj.SetActive(true);

                    float x = Random.Range(-20.0f, 20.0f);
                    enemyObj.transform.position = new Vector3(x, 0.1f, 20.0f);
                    break;
                }
            }
        }
	}//end of Update()
}
