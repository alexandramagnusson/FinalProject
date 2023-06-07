using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Igloo : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextLevel = "";

    private void Start()
    {
        if (nextLevel == "")
        {
            nextLevel = SceneManager.GetActiveScene().name;
        }
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        PlatformerController2D controller = other.gameObject.GetComponent<PlatformerController2D>();
        if (controller != null)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    

}
