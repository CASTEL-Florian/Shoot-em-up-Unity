using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<string> enemyType;
    [SerializeField] private List<float> timeBetweenSpawn;
    [SerializeField] private float spawnLineLength;
    [SerializeField] private bool useXmlFile = false;
    [SerializeField] private TextAsset xmlFile;
    public class LevelData
    {
        public List<string> enemyType;
        public List<float> timeBetweenSpawn;
    }

    private void Start()
    {
        if (useXmlFile)
        {
            //LevelData level = new LevelData();
            //level.enemyType = enemyType;
            //level.timeBetweenSpawn = timeBetweenSpawn;
            //XmlHelpers.SerializeToXML<LevelData>("testXML", level);
            LevelData level = XmlHelpers.DeserializeFromXML<LevelData>(xmlFile);
            enemyType = level.enemyType;
            timeBetweenSpawn = level.timeBetweenSpawn;
        }
    }
    public int StartSpawn()
    {
        StartCoroutine(SpawnEnemies());
        return enemyType.Count;
    }
    private IEnumerator SpawnEnemies()
    {
        yield return null;
        for (int i = 0; i < enemyType.Count; i++)
        {
            Vector3 spawnPoint = RandomSpawnPosition();
            ObjectPooler.Instance.Spawn(enemyType[i], spawnPoint, transform.rotation);
            if (i < timeBetweenSpawn.Count)
            {
                if (timeBetweenSpawn[i] >= 0)
                    yield return new WaitForSeconds(timeBetweenSpawn[i]);
                else
                {
                    while (GameManager.Instance.GetEnemyRemaining() > enemyType.Count - i - 1)
                        yield return null;
                    yield return new WaitForSeconds(-timeBetweenSpawn[i]);
                }    
            }

        }
        yield return null;
    }

    private Vector3 RandomSpawnPosition()
    {
        float y = Random.Range(-spawnLineLength / 2, spawnLineLength / 2);
        return new Vector3(transform.position.x, y, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + new Vector3(0, spawnLineLength / 2, 0), transform.position + new Vector3(0, -spawnLineLength / 2, 0));
    }

}
