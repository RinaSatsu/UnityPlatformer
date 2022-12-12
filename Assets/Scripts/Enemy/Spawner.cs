using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private float currentTick = 0;

    [SerializeField] private float cd = 5.0f;

    [SerializeField] private GameObject spawnObj;
    [SerializeField] private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void FixedUpdate()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (currentTick == 0)
            GameObject.Instantiate(spawnObj, spawnPoint.position, Quaternion.identity);
        currentTick += Time.deltaTime;
        if (currentTick >= cd)
            currentTick = 0;
    }
}
