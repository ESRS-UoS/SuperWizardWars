using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Random_Weapon : MonoBehaviour
{
    public GameObject yButton;
    private int contents;

    // Start is called before the first frame update
    void Start()
    {
        //Hide y button when spawned
        yButton.SetActive(false);

        //Generate random number between 1 and 4 (represents weapon)
        contents = Random.Range(1, 5); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Reveal y button when player enters collider bounds
        if(collider.gameObject.tag.Equals("Player"))
        {
            print("Player entered trigger");
            yButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //Hide y button when player leaves collider bounds
        if (collider.gameObject.tag.Equals("Player"))
        {
            print("Player exited trigger");
            yButton.SetActive(false);
        }
    }

    public int getContents()
    {
        return contents;
    }
}
