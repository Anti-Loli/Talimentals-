using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentScene.name == "InsideSchool")
        {
            SceneManager.LoadScene("Cave");
        }
        else if (currentScene.name == "OutsideSchool")
        {
            SceneManager.LoadScene("Credits");
        }
        else if (currentScene.name == "Cave")
        {
            SceneManager.LoadScene("OutsideSchool");
        }
    }
}
