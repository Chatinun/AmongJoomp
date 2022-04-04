using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public SpriteRenderer[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static HealthManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health == 0)
        {
            PlayerMovement.instance.isDeaded();
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.RestartScene();
    }
}
