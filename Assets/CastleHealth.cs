using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Slider healthSlider;

    int health;
    int maxHealth;
    void Start()
    {
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage()
    {
        health -= 1;
        healthSlider.value = health;
    }

    public void Heal()
    {

    }
}
