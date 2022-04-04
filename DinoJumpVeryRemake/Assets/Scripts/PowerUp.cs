using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isPowerup;
    public bool isSpeedPowerup;
    public static PowerUp instance;
    [SerializeField] private Transform transform;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        instance = this;
    }

    //Speed Up Character & Background
    //Invincible to all obstacle
    public void SpeedPowerUp()
    {
        //BgMove.instance.speedBackground();
        GameManager.instance.bgSpeedup += 100f;
        GameManager.instance.obstacleSpeedup += 100f;
        GameManager.instance.itemSpeedup += 100f;
        GameManager.instance.powerupScore = true;
        PlayerMovement.instance.invincible = true;
        PlayerMovement.instance.playerAnimator.speed = 3;
        isPowerup = true;
        isSpeedPowerup = true;

        StartCoroutine(BeforePowerUpEnd("Speed"));
        StartCoroutine(SpeedSound());
    }

    IEnumerator SpeedSound()
    {
        float soundDelay = 0.1f;
        for (int i = 0; i < 46; i++)
        {
            AudioManager.instance.audioPlay("giant");
            yield return new WaitForSeconds(soundDelay);
        }
    }

    void SpeedPowerUpEnd()
    {
        //BgMove.instance.slowBackground();
        GameManager.instance.bgSpeedup -= 100f;
        GameManager.instance.obstacleSpeedup -= 100f;
        GameManager.instance.itemSpeedup -= 100f;
        GameManager.instance.powerupScore = false;
        PlayerMovement.instance.playerAnimator.speed = 1;
        PlayerMovement.instance.invincible = false;
        isPowerup = false;
        isSpeedPowerup = false;
    }

    //Increase size of a Character
    //Invincible to all obstacle
    public void GiantPowerUp()
    {
        transform.localScale += new Vector3(5, 5, 0);
        PlayerMovement.instance.invincible = true;
        isPowerup = true;

        StartCoroutine(GiantSound());
        StartCoroutine(BeforePowerUpEnd("Giant"));
    }

    IEnumerator GiantSound()
    {
        float soundDelay = 0.5f;
        for (int i = 0; i < 10; i++)
        {
            AudioManager.instance.audioPlay("giant");
            yield return new WaitForSeconds(soundDelay);
        }
    }

    private void GiantPowerUpEnd()
    {
        transform.localScale -= new Vector3(5, 5, 0);
        PlayerMovement.instance.invincible = false;
        isPowerup = false;
    }

    public void SlowTimePowerUp()
    {
        Time.timeScale = 0.7f;
        StartCoroutine(BeforePowerUpEnd("SlowTime"));
    }

    private void SlowTimePowerUpEnd()
    {
        Time.timeScale = 1f;
    }

    IEnumerator BeforePowerUpEnd(string power)
    {
        yield return new WaitForSeconds(3);

        float flashDelay = 0.08f;
        for (int i = 0; i < 10; i++)
        {
            PlayerMovement.instance.sprite.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            PlayerMovement.instance.sprite.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }

        if(power == "Speed")
        {
            SpeedPowerUpEnd();
        }
        else if(power == "Giant")
        {
            GiantPowerUpEnd();
        }else if(power == "SlowTime")
        {
            SlowTimePowerUpEnd();
        }

    }

    public void HpPowerUp()
    {
        HealthManager.instance.health++;
    }
}
