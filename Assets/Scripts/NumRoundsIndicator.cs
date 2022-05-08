using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumRoundsIndicator : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        GameMaster.TotalRounds = 1;
    }

    // Update is called once per frame
    void Update()
    {
        text.GetComponent<Text>().text = GameMaster.TotalRounds.ToString();
    }
}
