﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text fruitCount;
    public Text healthCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fruitCount.text = CharacterController.fruitCount.ToString();
        healthCount.text = CharacterController.health.ToString();
    }
}
