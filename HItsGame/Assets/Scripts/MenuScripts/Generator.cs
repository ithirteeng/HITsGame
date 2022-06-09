using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    GameObject[] stars;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    GameObject[] endPoint;

    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        Invoke("AttemptSpawn", spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnStar()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        int index = 0;

        if (randomNumber > 80)
        {
            index = 1;
        } 

        GameObject star = Instantiate(stars[index]);

        float startY = UnityEngine.Random.Range(startPos.y - 3f, startPos.y + 3f);
        star.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scale = UnityEngine.Random.Range(0.8f, 1.2f);
        star.transform.localScale = new Vector2(scale, scale);

        float speed = UnityEngine.Random.Range(1f, 2f);
        star.GetComponent<Glitter>().StartFloating(speed, 15);
    }

    void AttemptSpawn()
    {
        SpawnStar();

        Invoke("AttemptSpawn", spawnInterval);
    }
}
