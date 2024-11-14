using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOrNext : MonoBehaviour
{
    public string NameScene = "";
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            SceneManager.LoadScene(NameScene);
        }
    }
    
    public void Level1()
    {
        SceneManager.LoadScene("level111");
    }
    public void Level2()
    {
        SceneManager.LoadScene("level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("level3");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
