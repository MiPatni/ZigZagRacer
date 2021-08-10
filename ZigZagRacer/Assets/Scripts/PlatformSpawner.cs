using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public Transform lastPlatform;
    Vector3 lastPos;
    Vector3 newPos;
    bool stop;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = lastPlatform.position;
        StartCoroutine(SpawnPlatforms());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnPlatforms()
    {
        while(!stop)
        {
            GeneratePosition();
            Instantiate(platform, newPos, Quaternion.identity);
            lastPos = newPos;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void GeneratePosition()
    {
        newPos = lastPos;
        int rand = Random.Range(0, 2);
        if(rand > 0)
        {
            newPos.x += 2f;
        }
        else
        {
            newPos.z += 2f;
        }
    }
}
