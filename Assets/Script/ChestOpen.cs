using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestOpen : MonoBehaviour
{
    public GameObject warningUI;
    //public Data data;
    public int maxcoin = 3;
    public int total;
    public GameObject chestOP;
    public GameObject Ilang;
    public bool completed = false;
    public SpriteRenderer box;
    //public PlayerController callthis;
    
    void Start()
    {
        warningUI.SetActive(false);
        chestOP.SetActive(false);
        box.enabled = true;
        Ilang.SetActive(true);
    }

    void Update()
    {
        total = Data.score;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && completed == false)
        {
            if (total < maxcoin && completed == false)
            {
               warningUI.SetActive(true);

            }
            if (total >= maxcoin)
            {
                Debug.Log("COMPLETED");
                chestOP.SetActive(true);
                completed = true;
                box.enabled = false;
                Ilang.SetActive(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (total < maxcoin)
        {
            warningUI.SetActive(false);
        }
    }


}
