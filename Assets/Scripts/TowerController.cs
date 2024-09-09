using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerController : MonoBehaviour
{

    [SerializeField]
    public new int towerHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        void OnTriggerEnter(Collider Tower)
        {
            Debug.Log(towerHP.ToString());
            if (Tower.CompareTag("Enemy"))
            {
                towerHP--;
                Debug.Log(towerHP.ToString());
                {
                    Destroy(gameObject);

                    SceneManager.LoadScene(0);
                }
            }
        }

        if (towerHP <= 0) 
        {
            SceneManager.LoadScene(4);
        }
    }
}
