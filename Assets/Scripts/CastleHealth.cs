using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{

    [SerializeField]
    Transform[] positions;

    Enemy[] attackingEnemies;
    // Start is called before the first frame update
    [SerializeField]
    Slider healthSlider;

    [SerializeField]
    GameObject hitSound;

    int health;
    int maxHealth = 10;
    void Start()
    {
        healthSlider.maxValue = maxHealth;
        Heal();
        attackingEnemies = new Enemy[positions.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health -= 1;
        healthSlider.value = health;
        Instantiate(hitSound);

        if(health <= 0)
        {
            FindObjectOfType<UpgradeManager>().Loose();
        }
    }

    public void Heal()
    {
        health = maxHealth;
        healthSlider.value = health;
    }

    public Transform OccupyPosition(Enemy enemy)
    {
        for (int i = 0; i < attackingEnemies.Length; i++)
        {
            if(attackingEnemies[i] == null || attackingEnemies[i].currentState == Enemy.EnemyState.DEAD)
            {
                attackingEnemies[i] = enemy;
                return positions[i];
            }
        }
        return null;
    }

    public bool IsFullHealth()
    {
        return health == maxHealth;
    }
}
