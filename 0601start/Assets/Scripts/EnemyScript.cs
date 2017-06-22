using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    public ENEMYSTATE state = ENEMYSTATE.IDLE;

    //delegate void Func();
    //Dictionary<ENEMYSTATE, Func> dicState = new Dictionary<ENEMYSTATE, Func>();
    Dictionary<ENEMYSTATE, System.Action> dicState = new Dictionary<ENEMYSTATE, System.Action>();

    float stateTime = 0.0f;
    public float idleStateMaxTime = 2.0f;
    public Animation anim;

    public Transform target = null;
    PlayerState playerState = null;
    CharacterController characterController = null;

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float attackableRange = 2.5f;

    int healthPoint = 5;

    public int score = 0;

    void Start()
    {
        target = GameObject.Find("Player").transform; //do not use Find()
        characterController = GetComponent<CharacterController>();

        playerState = target.GetComponent<PlayerState>();
    }

    void Awake()
    {
        anim = GetComponent<Animation>();
        characterController = GetComponent<CharacterController>();

        dicState[ENEMYSTATE.NONE] = None;
        dicState[ENEMYSTATE.IDLE] = Idle;
        dicState[ENEMYSTATE.MOVE] = Move;
        dicState[ENEMYSTATE.ATTACK] = Attack;
        dicState[ENEMYSTATE.DAMAGE] = Damage;
        dicState[ENEMYSTATE.DEAD] = Dead;

        initSpider();
    }

    void initSpider()
    {
        healthPoint = 5;
        state = ENEMYSTATE.IDLE;
        anim.Play("idle");
    }

    void Update()
    {
        if (playerState.isDead == false)
            dicState[state]();
    }

    void None()
    {

    }

    void Idle()
    {
        //Debug.Log("Idle ================================= ");

        stateTime += Time.deltaTime;
        if (stateTime >= idleStateMaxTime)
        {
            stateTime = 0.0f;
            state = ENEMYSTATE.MOVE;
        }
    }

    void Move()
    {
        //Debug.Log("Move ================================= ");
        anim.Play("walk");

        Vector3 dir = target.position - transform.position;

        if (dir.magnitude > attackableRange)
        {
            dir.Normalize();
            characterController.SimpleMove(dir * moveSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                    Quaternion.LookRotation(dir),
                                                    rotationSpeed * Time.deltaTime);
        }
        else
        {
            stateTime = 2.0f;
            state = ENEMYSTATE.ATTACK;
        }
    }

    void Attack()
    {
        //Debug.Log("Attack ================================= ");
        stateTime += Time.deltaTime;
        if (stateTime > 2.0f)
        {
            stateTime = 0.0f;
            anim.Play("attack_Melee");
            anim.PlayQueued("idle", QueueMode.CompleteOthers);

            playerState.DamageByEnemy();
        }

        Vector3 dir = target.position - transform.position;

        if (dir.magnitude > attackableRange)
        {
            state = ENEMYSTATE.IDLE;
        }
    }

    void Damage()
    {
        //Debug.Log("Damage ================================= ");
        healthPoint -= 1;

        anim["damage"].speed = 0.5f;
        anim.Play("damage");

        anim.PlayQueued("idle", QueueMode.CompleteOthers);

        stateTime = 0.0f;
        state = ENEMYSTATE.IDLE;

        if (healthPoint <= 0)
            state = ENEMYSTATE.DEAD;
    }

    void Dead()
    {
        //Destroy(gameObject);
        StartCoroutine("DeadProcess");
        state = ENEMYSTATE.NONE;

        ScoreManager.Instance().myScore += score;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == ENEMYSTATE.NONE || state == ENEMYSTATE.DEAD)
            return;

        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.layer.GetHashCode() != 11)
            return;

        state = ENEMYSTATE.DAMAGE;
    }

    public GameObject explosionParticle = null;
    public GameObject deadObject = null;

    IEnumerator DeadProcess()
    {
        anim["death"].speed = 0.5f;
        anim.Play("death");

        while (anim.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        //particle
        yield return new WaitForSeconds(1.0f);

        GameObject explosionObj = Instantiate(explosionParticle) as GameObject;
        Vector3 explosionObjPos = transform.position;
        explosionObjPos.y = 0.6f;
        explosionObj.transform.position = explosionObjPos;

        //dead object
        yield return new WaitForSeconds(0.5f);

        GameObject deadObj = Instantiate(deadObject) as GameObject;
        Vector3 deadObjPos = transform.position;
        deadObjPos.y = 0.6f;
        deadObj.transform.position = deadObjPos;

        float rotationY = Random.Range(-180.0f, 180.0f);
        deadObj.transform.eulerAngles = new Vector3(0.0f, rotationY, 0.0f);

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}