using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float totalHp = 100;
    float restHp;
    protected bool isDead;
    protected Transform hpObj;      // Yellow stripe
    protected Transform redHp;      // Red stripe
    protected Transform mainCamera; // Position of the camera

    public virtual void Init()
    {
        restHp = totalHp;
        hpObj = transform.Find("BloodStripe");
        redHp = hpObj.Find("Hp");
        mainCamera = GameObject.Find("Main Camera").transform;
        isDead = false;
    }

    /// <summary>
    /// Received damage, Hp decreases
    /// </summary>
    /// <param name="damage"></param> Received damage
    public void Damage(float damage)
    {
        if (restHp > damage)
        {
            restHp -= damage;
            // Display the blood stripe when hurted
            if (restHp < totalHp)
                hpObj.gameObject.SetActive(true);
            Vector3 hpScale = redHp.localScale;
            hpScale.x = restHp / totalHp;
            redHp.localScale = hpScale;
        }
        else
            Death();
    }

    public virtual void Death()
    {
        restHp = 0;
        if (isDead == false)
        {
            isDead = true;
            hpObj.gameObject.SetActive(false);
        }
    }
}
