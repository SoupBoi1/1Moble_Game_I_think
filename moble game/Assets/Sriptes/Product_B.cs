using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_B : MonoBehaviour
{

    public bool watering;

    private IEnumerator waterup;
    float value;
    public GameObject rootenfruit;
    // Start is called before the first frame update
    void Start()
    {

       
    }
    private void Update()
    {

        StartCoroutine(Rooting(5f, rootenfruit));
        print(value);
    }


    IEnumerator Rooting(float timeout, GameObject rotten)
    {
        
        float starttime = Time.time; 

        while (!watering)
        {
            yield return new WaitForSeconds(0.1f);
            value++;
            
            if ((Time.time- starttime ) > timeout)
            {
                print("it is working");
                GameObject r = Instantiate(rotten);
                r.transform.position = transform.position;
                r.transform.rotation = transform.rotation;
                Destroy(gameObject);
                break;
            }
        }
        if (watering)
        {
            value = 0;
        }


    }
}
