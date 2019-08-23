using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    forward,
    attack,
    death
}

public class Enemy : Character
{
    Animator anim;
    Rigidbody rigid;
    public EnemyState state;
    Transform eye;      // An eye for searching the path
    List<Collider> ways;
    public float damage;
    public float speed;
    public Transform hitPos;


    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        state = EnemyState.forward;
        eye = transform.Find("Eye");
        ways = new List<Collider>();
        hitPos = ToolsMethod.Instance.FindChildByName(transform, "HitPos");
    }

    // Update is called once per frame
    void Update()
    {
        hpObj.rotation = mainCamera.rotation;
        if (GameMain.instance.gameOver)
            anim.Play("idle");
        else if (state == EnemyState.forward)
            Forward();
    }

    public int view;
    Quaternion wayDir;  // Direction
    Base target;        // Attack target: the base
    Transform path;     // Path to the base



    public void Forward()
    {
        RaycastHit hit;

        // Attack the target if see it
        if (Physics.Raycast(eye.position, transform.forward, out hit, view, LayerMask.GetMask("Base")))
        {
            anim.Play("attack");
            state = EnemyState.attack;
            target = hit.collider.GetComponent<Base>();
        }
        else
            anim.Play("walk");

        // 30 degrees to the ground to search the way
        if (Physics.Raycast(eye.position, Quaternion.AngleAxis(30, transform.right)
            * transform.forward, out hit, 50, LayerMask.GetMask("Path")))
        {
            Debug.DrawLine(eye.position, hit.point, Color.blue);
            // Find a new path
            if (!ways.Contains(hit.collider))
            {
                ways.Add(hit.collider);
                path = hit.transform;
                wayDir = Quaternion.LookRotation(path.forward);
            }
        }
        else // Search if there's a new path if the path is ended
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 8, LayerMask.GetMask("Path"));
            for (int i = 0; i < colliders.Length; i++)
            {
                // Get a new path
                if (!ways.Contains(colliders[i]))
                {
                    path = colliders[i].transform;
                    wayDir = Quaternion.LookRotation(path.forward);
                    break;
                }
            }
        }

        // Get the offset in order to walk at the centre
        float offset = 0;
        if(path != null)
        {
            Vector3 distance = transform.position - path.position;
            offset = Vector3.Dot(distance, path.right.normalized);
        }

        // Head the right way
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wayDir, speed * 20 * Time.deltaTime);
        transform.Translate(-offset * Time.deltaTime, 0, speed * Time.deltaTime);
    }

    public void Attack()
    {
        target.Damage(damage);
    }

    bool isStunned;
    public void Stun(float time)
    {
        if(isStunned == false)
        {
            isStunned = true;
            float initSpeed = speed;
            speed = 0;
            anim.Play("idle");
            Util.Instance.AddTimeTask(() =>
            {
                speed = initSpeed;
                isStunned = false;
            }, time);
        }
    }

    public override void Death()
    {
        base.Death();
        gameObject.layer = LayerMask.NameToLayer("Corpse");
        anim.Play("death");
        state = EnemyState.death;
        //DestroySelf();
    }

    // Corpse disapears
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
