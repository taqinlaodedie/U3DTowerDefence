using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saber : Tower
{
    public float critChance = 0.2f; // Chance of critical hits
    public ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    public override void Attack()
    {
        if (target != null)
        {
            int crit = (int)(critChance * 100);
            target.Damage(damage * (Random.Range(0, 100) < crit ? 2 : 1));
            muzzle.gameObject.SetActive(true);
            effect.Play();
        }
    }

    
}
