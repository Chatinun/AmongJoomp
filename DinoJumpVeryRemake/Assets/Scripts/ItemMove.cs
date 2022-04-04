using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    public float speed = 5f;

    public static ItemMove instance;
    void Start()
    {
        instance = this;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

        if (!PlayerMovement.instance.isDead)
        {
            //Move Obstacle to the left
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);

            if (gameObject != null && transform.position.x < -15)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.audioPlay("powerup");
        if (collision.gameObject.tag == "Player" )
        {
            if(gameObject.tag == "Speed")
            {
                PowerUp.instance.SpeedPowerUp();
            }
            else if(gameObject.tag == "Giant")
            {
                PowerUp.instance.GiantPowerUp();
            }
            else if (gameObject.tag == "Heal")
            {
                PowerUp.instance.HpPowerUp();
            }
            Destroy(gameObject);
        }
    }
}
