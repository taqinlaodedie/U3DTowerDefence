using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float bleedDamage; // Bleeding damage
    float damage;
    Enemy target;
    Transform pool;
    Vector3 initPos;
    
    public void Init(Vector3 position, Quaternion rotation, Enemy _target, float _damage, Transform _pool)
    {
        transform.SetParent(null);
        transform.position = position;
        transform.rotation = rotation;
        target = _target;
        damage = _damage;
        pool = _pool;
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Continue searching the target if not hit
        if(transform.parent == null)
        {
            transform.Translate(0, 0, speed * Time.deltaTime * speed);
            // Destroy if fled too far
            if (Vector3.Distance(initPos, transform.position) > 500)
                DestroySelf();
            if (target != null && target.state != EnemyState.death)
            {
                transform.LookAt(target.hitPos);
                if (Vector3.Distance(target.hitPos.position, transform.position) <= 1)
                {
                    target.Damage(damage);
                    transform.SetParent(target.hitPos);
                }
                Bleeding();
            }
        }
        else if (target.state == EnemyState.death)
            DestroySelf();
    }

    float bleedTime = 1;
    float bleedCount = 0;
    private void Bleeding()
    {
        if (bleedCount >= bleedTime)
        {
            target.Damage(bleedDamage);
            bleedCount = 0;
        }
        else
        {
            bleedCount += Time.deltaTime;
        }
    }

    // Destroy itself (back in the pool)
    private void DestroySelf()
    {
        transform.SetParent(pool);
    }
}
