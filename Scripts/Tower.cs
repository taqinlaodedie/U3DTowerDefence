using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    protected Animator anim;
    protected Transform muzzle;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    public virtual void Init()
    {
        enabled = true;
        anim = GetComponentInChildren<Animator>();
        muzzle = ToolsMethod.Instance.FindChildByName(transform, "Muzzle");
    }
    // Update is called once per frame
    void Update()
    {
        if (GameMain.instance.gameOver == true)
        {
            anim.SetBool("Attack", false);
            return;
        }
        GetTarget();
    }
    public float attackRange;
    public float damage;
    protected Enemy target;
    void GetTarget()
    {
        // Search target
        if (target == null)
        {
            Collider[] enemis = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
            if (enemis.Length == 0)
            {
                anim.SetBool("Attack", false);
            }
            for (int i = 0; i < enemis.Length;)
            {
                target = enemis[i].GetComponent<Enemy>();
                anim.SetBool("Attack", true);
                break;
            }
        }
        // Face to the target
        else
        {
            Vector3 pos = target.transform.position;
            Quaternion dir = Quaternion.LookRotation(new Vector3(pos.x, transform.position.y, pos.z) - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, 0.1f);
            // Find new target if too far or target death
            if (Vector3.Distance(target.transform.position, transform.position) >= attackRange || target.state == EnemyState.death)
                target = null;
        }
    }
    public virtual void Attack()
    {

    }

    public void disableEffect()
    {
        muzzle.gameObject.SetActive(false);
    }
}
