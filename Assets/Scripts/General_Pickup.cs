using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_Pickup : MonoBehaviour
{
    public GameObject yButton;
    public int contents;

    // Start is called before the first frame update
    void Start()
    {
        //Hide y button when spawned
        yButton.SetActive(false);

        //Contents variable set in inspector
        //5 Represents health potion
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Reveal y button when player enters collider bounds
        if (collider.gameObject.tag.Equals("Player"))
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
