using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGetter : MonoBehaviour
{
    public GameObject floatingPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.instance.audioPlay("score");
            GameManager.instance.ScorePlus(1000);
            Instantiate(floatingPoint,new Vector3(transform.position.x, transform.position.y + 1,transform.position.z),Quaternion.identity);
        }
    }
}
