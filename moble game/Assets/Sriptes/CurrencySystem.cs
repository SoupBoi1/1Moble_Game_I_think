using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CurrencySystem : MonoBehaviour
{
    [SerializeField]

    public float Coins = 0;
    string s;
    public TMP_Text textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        s = Coins.ToString();
        textMesh.text = s;
    }
}
