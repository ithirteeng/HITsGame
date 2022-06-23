using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitter : MonoBehaviour
{
    private float _speed;
    private float _endPosX;

    void Start()
    {

    }

    public void StartFloating(float speed, float endPosX)
    {
        _speed = speed;
        _endPosX = endPosX;
    }

    void Update()
    {
        Vector3[] directions = new Vector3[2];
        directions[0] = new Vector3(0.5f, 0.5f, 0.5f);
        directions[1] = new Vector3(1, 0, 0);
        int index = UnityEngine.Random.Range(0, directions.Length);
        Vector3 direction = directions[index];

        transform.Translate(direction.normalized * (Time.deltaTime * _speed));

        if (transform.position.x > _endPosX)
        {
            Destroy(gameObject);
        }
    }
}
 