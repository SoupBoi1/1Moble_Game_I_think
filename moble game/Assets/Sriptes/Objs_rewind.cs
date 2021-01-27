using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objs_rewind : MonoBehaviour
{
    Obj_rewind _Rewind;

    List<GameObject> GOchilders;

    public float timeCounter = 5f;
    float timer;

    public bool isRewinding = false;
    public bool isRecouding = true;
    public bool rewindLoc = true;
    public bool rewindRot = true;

    //int a=0;
    // Start is called before the first frame update
    void Start()
    {
        GOchilders = new List<GameObject>();
        
        
        int go = gameObject.transform.childCount;
        int i = 0;
        
        while(go > GOchilders.Count)
        {
            GameObject _gamOb = gameObject.transform.GetChild(i).gameObject;

            GOchilders.Add(_gamOb);
            
            _gamOb.AddComponent<Obj_rewind>();

            _Rewind = _gamOb.GetComponent<Obj_rewind>();
            _Rewind.timeCounter = timeCounter;
            _Rewind.isRewinding = isRewinding;
            _Rewind.isRecouding = isRecouding;
            _Rewind.rewindLoc = rewindLoc;
            _Rewind.rewindRot = rewindRot;
            i++;
        }

    }

    // Update is called once per frame
    void Update()
    {


        foreach (GameObject a in GOchilders)
        {
            
            if (a.GetComponent<Obj_rewind>())
            {
                _Rewind = a.GetComponent<Obj_rewind>();
                print(a.name);
            }
            else
            {
                _Rewind= a.AddComponent<Obj_rewind>();
                print(a.name+"rr");
            }
            //_Rewind = a.GetComponent<Obj_rewind>();
            _Rewind.timeCounter = timeCounter;
            _Rewind.isRewinding = isRewinding;
            _Rewind.isRecouding = isRecouding;
            _Rewind.rewindLoc = rewindLoc;
            _Rewind.rewindRot = rewindRot;
        }

    }

   
}
