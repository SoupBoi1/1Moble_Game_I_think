using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_rewind : MonoBehaviour
{
    
    List<LocRotPoints> point;
    public float timeCounter = 5f;
    float timer;

    public bool isRewinding = false;
    public bool isRecouding = true;
    public bool rewindLoc = true;
    public bool rewindRot = true;


    private void Start()
    {
        point = new List<LocRotPoints>();
    }

     void Update()
     {
        if (isRewinding)
        {
            Rewind(point, gameObject);
        }
        else
        {
            if (isRecouding)
            {
                Record(point, gameObject, timeCounter);
            }
            

        }
       
     }

    public void Rewind(List<LocRotPoints> point, GameObject gameObj)
    {
        if (gameObj.GetComponent<Rigidbody>())
        {
           // gameObj.GetComponent<Rigidbody>().isKinematic = true;
        }
        if (point.Count > 0)
        {
            print(point[0].postion);
            gameObj.transform.position = point[0].postion;
            gameObj.transform.rotation = point[0].rotation;
            point.RemoveAt(0);
        }
        else
        {
            StopReWind(gameObj, isRewinding);
            gameObj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    public void Record(List<LocRotPoints> point, GameObject gameObj, float timeCounter, bool RecLoc = true, bool RecRot = true)
    {


        if (point.Count > Mathf.Round(timeCounter / Time.fixedDeltaTime))
        {
            point.RemoveAt(point.Count - 1);
        }

        point.Insert(0,new LocRotPoints(gameObj.transform.position, gameObj.transform.rotation));
       // print(point[point.Count-1].postion);
    }

    public void StopReWind(GameObject gameObj, bool ToggleV1, bool ToggleV2= false)
    {
        ToggleV1 = false;
        ToggleV2 = false;


        if (gameObj.GetComponent<Rigidbody>())
        {
            gameObj.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
