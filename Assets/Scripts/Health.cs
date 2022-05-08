using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 5;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public GameObject gravestone;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = fullHeart;
            }
            else
            {
                hearts[i].GetComponent<SpriteRenderer>().sprite = emptyHeart;
            }
        }
        //Die if health is fully depleted
        if(health < 1)
        {
            //Play death sound
            SoundManager.PlaySound("death");
            //Spawn gravestone prefab
            Instantiate(gravestone, gameObject.transform.position, gameObject.transform.rotation);
            //Destroy player prefab
            Destroy(gameObject);
        }
    }

    public void takeDamage(int dmg)
    {
        health = health - dmg;
        SoundManager.PlaySound("hit");
    }
}
