using System.Collections;
using System.Collections.Generic;
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
        if (towerHP <= 0) 
        {
            SceneManager.LoadScene(4);
        }
    }
}
