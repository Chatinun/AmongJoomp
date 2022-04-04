using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    
    public float speed = 5f;
    private float posY;
    private float posX;
    private float rotationSpeed = 0;

    public static ObstacleMove instance;

    private void Awake()
    {
        instance = this;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (!PlayerMovement.instance.isDead)
        {
            //Move Obstacle to the left
            transform.position = new Vector2(transform.position.x - (speed - posX) * Time.deltaTime, transform.position.y + posY * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            if (gameObject != null && transform.position.x < -15 || transform.position.x > 50)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !PlayerMovement.instance.isDead)
        {
            //AudioManager.instance.audioPlay("hit");
            boxCollider.enabled = false;
            StartCoroutine(delayDamage());
            ShakeScreen.instance.start = true;
            if (PowerUp.instance.isPowerup)
            {
                AudioManager.instance.audioPlay("destroy");
                if (PowerUp.instance.isSpeedPowerup)
                {
                    posX = 200f;
                }
                else
                {
                    posX = 20f;
                }
                posY = 10f;
                rotationSpeed = 360f;
            }
        }
    }

    IEnumerator delayDamage()
    {
        yield return new WaitForSeconds(1);
        boxCollider.enabled = true;
    }
}
