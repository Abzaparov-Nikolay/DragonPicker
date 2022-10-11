using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDragon : MonoBehaviour
{
    public GameObject DragonEggPrefab;
    public float speed = 1f;
    public float timeBetweenEggDrops = 1f;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.03f;

    private void Start()
    {
        Invoke("DropEgg", 2f);
    }

    private void DropEgg()
    {
        var diff = new Vector3(0, 0.5f, 0);
        var egg = Instantiate<GameObject>(DragonEggPrefab);
        egg.transform.position = diff + transform.position;
        Invoke("DropEgg", timeBetweenEggDrops);
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if(pos.x < -leftRightDistance)
        {
            speed = Mathf.Abs(speed);
        }
        else if(pos.x > leftRightDistance)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate()
    {
        if(UnityEngine.Random.value < chanceDirection)
        {
            speed *= -1;
        }
    }
}
