using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : Tower
{
    
    Bullet magicHit;
    Transform bulletPool;

    public override void Init()
    {
        enabled = true;
        anim = GetComponentInChildren<Animator>();
        muzzle = ToolsMethod.Instance.FindChildByName(transform, "Muzzle");
        magicHit = Resources.Load<Bullet>("Bullet/MagicHit");
        bulletPool = GameObjectPool.Instance.getPool(magicHit.name);
    }

    public override void Attack()
    {
        base.Attack();
        if (bulletPool.childCount > 0)
            bulletPool.GetChild(0).GetComponent<Bullet>().Init(muzzle.position, muzzle.rotation, target, damage, bulletPool);
        else
            Instantiate(magicHit).Init(muzzle.position, muzzle.rotation, target, damage, bulletPool);
    }
}
