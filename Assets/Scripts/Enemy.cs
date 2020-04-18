using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    CastleHealth castleHealth;

    public Enemy enemyAhead;
    // Start is called before the first frame update
    public enum EnemyState
    {
        MOVING,
        ATTACKING,
        DEAD
    }

    public EnemyState currentState;

    float time;
    float attackTime = 2f;
    void Start()
    {
        currentState = EnemyState.MOVING;
        castleHealth = FindObjectOfType<CastleHealth>();
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
                        currentState = EnemyState.ATTACKING;
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
            case EnemyState.ATTACKING:
                time += Time.deltaTime;
                if(time >= attackTime)
                {
                    castleHealth.TakeDamage();
                    time -= attackTime;
                }
                break;
            case EnemyState.DEAD:
                if(rotTimer <= 1)
                {
                    rotTimer += Time.deltaTime;
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

    }

    float baseHeight;
    float moveTime;
    void MoveForward()
    {
        moveTime = (moveTime + Time.deltaTime);
        transform.position -= transform.forward * Time.deltaTime * 2f;
        transform.position = new Vector3(transform.position.x, baseHeight + 0.2f*Mathf.Sin(moveTime*10f), transform.position.z);

        
    }
}
