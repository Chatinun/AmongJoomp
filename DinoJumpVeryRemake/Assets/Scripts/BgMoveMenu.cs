using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMoveMenu : MonoBehaviour
{
    private Transform trasnform;

    [SerializeField] private float startPos;
    [SerializeField] private float endPos;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        trasnform = GetComponent<Transform>();
    }

    private void Update()
    {
            trasnform.position = new Vector2(trasnform.position.x - speed * Time.deltaTime, trasnform.position.y);

            if (trasnform.position.x <= endPos)
            {
                transform.position = new Vector2(startPos, trasnform.position.y);
            }

    }
}
