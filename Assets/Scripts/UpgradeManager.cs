using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject panel;

    [SerializeField]
    Text moneyText;
    [SerializeField]
    Text dayText;
    [SerializeField]
    Text itUpgradeText;
    [SerializeField]
    BowController cannon;
    [SerializeField]
    BowController bow;



    [SerializeField]
    ShooterManager shooterManager;
    [SerializeField]
    EnemySpawner enemySpawner;
    [SerializeField]
    BarricadeHealth barricade;

    int money;
    int day;
    int itLevel;

    CastleHealth castleHealth;

    [SerializeField]
    GameObject itUpgradGO;
    [SerializeField]
    Transform itUpgradePos;

    [SerializeField]
    GameObject winScreen;

    [SerializeField]
    GameObject looseScreen;
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            cannon.IncreaseAmmo();
        }
        for (int i = 0; i < 10; i++)
        {
            bow.IncreaseAmmo();
        }

        money = 0;
        moneyText.text ="Gold: " + money.ToString();
        shooterManager.Pause();
        day = 0;
        dayText.text = dayText.text = "Day: "  + day.ToString();
        castleHealth = FindObjectOfType<CastleHealth>();
        itLevel = 1;
        HandleITUpgrade();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelFinish()
    {
        panel.SetActive(true);
        shooterManager.Pause();


        int newMoney = itLevel * (10);
        money += newMoney;
        moneyText.text = "Gold: " + money.ToString();
        FindObjectOfType<Popup>().ShowMessage("+"  + newMoney.ToString() + " gold");
    }

    void HandleITUpgrade()
    {
        int itCost = itLevel * 10 + 10;

        itUpgradeText.text = "Upgrade IT (+" + (10).ToString() + " g per day)   [" + itCost.ToString() + " g]";

        
    }

    public void BuyArrows()
    {

        if (TrySpendMoney(2))
        {
            bow.IncreaseAmmo();
        }

    }
    public void BuyCannonballs()
    {
        if (TrySpendMoney(5))
        {
            cannon.IncreaseAmmo();
        }
    }
    public void BuyBarricade()
    {
        if (barricade.active)
        {
            FindObjectOfType<Popup>().ShowMessage("Already built");
            return;
        }
        if (TrySpendMoney(10)) {
            barricade.EnableBarricade();
        }
    }
    public void HealIT()
    {
        if (castleHealth.IsFullHealth())
        {
            FindObjectOfType<Popup>().ShowMessage("Already full health");
            return;
        }
        if (TrySpendMoney(50))
        {
            castleHealth.Heal();
        }
        
    }

    public void UpgradeIT()
    {
        if(TrySpendMoney( itLevel * 10 + 10))
        {
            itLevel++;
            Instantiate(itUpgradGO, itUpgradePos.transform.position + Vector3.up * 3f * (itLevel), Quaternion.identity);
            HandleITUpgrade();
        }
    }


    bool TrySpendMoney(int amount)
    {
        if(amount > money)
        {
            return false;
        }
        money -= amount;
        moneyText.text = "Gold: " + money.ToString();
        return true;
    }


    public void NextDay()
    {
        panel.SetActive(false);
        day++;
        dayText.text = "Day: " +  day.ToString();
        shooterManager.UnPause();
        enemySpawner.PlayLevel();
    }

    public void Win()
    {
        winScreen.SetActive(true);
        shooterManager.Pause();
    }

    public void Loose()
    {
        looseScreen.SetActive(true);
        shooterManager.Pause();
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EnemyKilled(float zPos)
    {
        float t = zPos/(enemySpawner.transform.position.z - castleHealth.transform.position.z);
        t = t < 0.3f ? 0f : t;
        int amount = (int)Mathf.Lerp(0,8, t);
        money += amount;
        FindObjectOfType<Popup>().ShowMessage("+ " + amount.ToString() + " gold");
        moneyText.text = "Gold: " + money.ToString();
    }
}
