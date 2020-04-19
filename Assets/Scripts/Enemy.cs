
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject deathSound;

    CastleHealth castleHealth;
    BarricadeHealth barricadeHealth;

    public Enemy enemyAhead;

    public float speed;
    // Start is called before the first frame update
    public enum EnemyState
    {
        MOVING,
        ATTACKING_CASTLE,
        ATTACKING_BARRICADE,
        DEAD
    }

    public EnemyState currentState;

    float time;
    float attackTime = 1.5f;
    void Start()
    {
        currentState = EnemyState.MOVING;
        castleHealth = FindObjectOfType<CastleHealth>();
        barricadeHealth = FindObjectOfType<BarricadeHealth>();
        baseHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyAhead != null && enemyAhead.currentState == EnemyState.DEAD)
        {
            enemyAhead = enemyAhead.enemyAhead;
        }
        switch (currentState)
        {
            case EnemyState.MOVING:
                
                if(enemyAhead == null)
                {
                    if (transform.position.z <= castleHealth.transform.position.z) {
                        currentState = EnemyState.ATTACKING_CASTLE;
                    }else if (transform.position.z <= barricadeHealth.transform.position.z && barricadeHealth.active)
                    {
                        currentState = EnemyState.ATTACKING_BARRICADE;
                    }
                    else
                    {
                        MoveForward();
                    }
                }else if (transform.position.z - enemyAhead.transform.position.z > 2f)
                {
                    MoveForward();
                }
                break;
            case EnemyState.ATTACKING_CASTLE:
                time += Time.deltaTime;
                if(time >= attackTime)
                {
                    castleHealth.TakeDamage();
                    time -= attackTime;
                }

                transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 45, 0), time/attackTime);
                break;
            case EnemyState.ATTACKING_BARRICADE:
                if (!barricadeHealth.active)
                {
                    currentState = EnemyState.MOVING;
                }
                time += Time.deltaTime;
                if (time >= attackTime)
                {
                    barricadeHealth.TakeDamage();
                    time -= attackTime;
                }

                transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 45, 0), time / attackTime);
                break;
            case EnemyState.DEAD:
                if(rotTimer <= 1)
                {
                    rotTimer += Time.deltaTime*2f;
                    transform.rotation = Quaternion.Lerp(Quaternion.identity, deathRotation, rotTimer);
                }
                return;

            default:
                break;
        }

    }

    Quaternion deathRotation;
    float rotTimer;

    public void Die()
    {
        currentState = EnemyState.DEAD;
        deathRotation = Quaternion.Euler(0, Random.Range(0, 360), 90);
        GetComponent<BoxCollider>().enabled = false;
        FindObjectOfType<EnemySpawner>().OnUnitDeath(transform.position.z);

        Instantiate(deathSound);
    }

    float baseHeight;
    float moveTime;
    void MoveForward()
    {
        moveTime = (moveTime + Time.deltaTime);
        transform.position -= transform.forward * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, baseHeight + 0.2f*Mathf.Sin(moveTime*10f), transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 5f*Mathf.Sin(moveTime * 10f));
        
    }
}
