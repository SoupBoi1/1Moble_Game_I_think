using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPad : MonoBehaviour
{
    public GameObject gamemagger;
    CurrencySystem currencySystem;

    // Start is called before the first frame update
    void Start()
    {
        currencySystem = gamemagger.GetComponent<CurrencySystem>();
    }

    void OnTriggerEnter(Collider collision)
    {
        print(collision.tag);
        if (collision.tag == "Product")
        {
            print("monre==");
            currencySystem.Coins += 40;
            Destroy(collision.gameObject);
        }
    }
}
