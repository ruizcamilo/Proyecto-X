using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    [SerializeField]
    bool _stopSpawning = false;
    [SerializeField]
    GameObject _powerupPrefab;
    [SerializeField]
    GameObject _enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
         while (_stopSpawning == false)
            {
                Vector3 pos = new Vector3(7.28f, -0.23f, 0f);
                
                GameObject newPowerup = Instantiate(_powerupPrefab, pos, Quaternion.identity);
                yield return new WaitForSeconds(5.0f);
            }
        
    }

    IEnumerator SpawnEnemyRoutine() {
        while(_stopSpawning == false) {
            Vector3 pos = new Vector3(1.49f, 3.6f, 0);
            if (GameObject.Find("TrialEnemy") == null) {
                GameObject newPowerup = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
