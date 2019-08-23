using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : Tower
{
    public float force; //Stun force
    public ParticleSystem effect; // Stun particle

    public override void Attack()
    {
        muzzle.gameObject.SetActive(true);
        Collider[] enemis = Physics.OverlapSphere(muzzle.position, attackRange / 2, LayerMask.GetMask("Enemy"));
        for (int i = 0; i < enemis.Length; i++)
        {
            Enemy enemy = enemis[i].GetComponent<Enemy>();
            enemy.Damage(damage);
            effect.transform.position = muzzle.position;
            effect.Play();
            if (enemy.state != EnemyState.death)
                enemy.Stun(force);
        }
    }
}
