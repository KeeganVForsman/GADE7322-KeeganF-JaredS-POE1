using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int towerHP = 100;

    public TextMeshProUGUI UITowHP;

    private void Start()
    {
        UpdateHPUI();
    }

    private void Update()
    {
        
    }

    void towerTakeDamage()
    {
        towerHP--;
        UpdateHPUI();

        if (towerHP >= 0)
        {
            SceneManager.LoadScene("Death");
        }
        else
        {
            new WaitForSeconds(3f);
            towerHP--;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Tower"))
        {
            Debug.Log("damage taken");

            towerTakeDamage();
        }
    }

    void UpdateHPUI()
    {
       
        
           //UITowHP.text = "Tower HP: " + towerHP.ToString(); 
        
    }

}
