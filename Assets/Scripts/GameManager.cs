using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject[] spawnPoints;

    public float timer;
    public float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenSpawns)
        {
            timer = 0;

            int randNum = Random.Range(0, 14);
            Instantiate(obstacle, spawnPoints[randNum].transform.position, Quaternion.identity);
        }
    }
}
