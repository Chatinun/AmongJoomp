using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour
{
    private Transform trasnform;

    [SerializeField] private float startPos;
    [SerializeField] private float endPos;

    public bool isThisGround;

    public float speed;
    public float speed2;

    private float timeCounter;
    private float spawnerSpeedup;

    public static BgMove instance;

    private void Start()
    {
        instance = this;
        trasnform = GetComponent<Transform>();
    }

    private void Update()
    {
        /*
        spawnerSpeedup = Spawner.instance.spawnerTimer;
        timeCounter += Time.deltaTime;
        if (timeCounter > 1.5f && spawnerSpeedup > 0.7f)
        {
            Debug.Log(speed + " Before Loop");
            speed += 0.2f;
            timeCounter = 0;
            Debug.Log(speed + " Loop");
        }
        */

        if (!PlayerMovement.instance.isDead)
        {
                trasnform.position = new Vector2(trasnform.position.x - speed * Time.deltaTime, trasnform.position.y);

                if (trasnform.position.x <= endPos)
                {
                    transform.position = new Vector2(startPos, trasnform.position.y);
                }

        }
        
    }

    public void speedBackground()
    {
        speed += 100f;
        Debug.Log(speed + " Power");
    }

    public void slowBackground()
    {
        speed -= 100f;
    }
}
