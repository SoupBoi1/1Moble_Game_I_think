using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public bool watering;
    public MeshFilter meshFilter;
    public MeshCollider collider;

    public Mesh Bmesh;
    public Mesh Mmesh;
    public GameObject Eobject;

    public float BwaterNeeded=10;
    public float EwaterNeeded = 90;
    public float maxWaterperSec = 20;

    public float waterrate = 6;
    float watered;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        collider = GetComponent<MeshCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (watering)
        {
            watered += waterrate*Time.deltaTime;
        }
        
        if (watered> BwaterNeeded && watered < (EwaterNeeded/2))
        {


          //  print("stage1 : done!"+ watered);
            meshFilter.mesh = Bmesh;
            collider.sharedMesh = Bmesh;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (watered > (EwaterNeeded/2) && watered < EwaterNeeded )
        {
         //   print("stage2 : done!" + watered);
            meshFilter.mesh = Mmesh;
            collider.sharedMesh = Mmesh;
        }
        else if (watered > EwaterNeeded)
        {
            Vector3 t = gameObject.transform.position  +new Vector3(0, 1, 0);

           // print("wait 5sec" + watered);
            Object.Instantiate(Eobject, t, Quaternion.Euler(0,1,0));
            Destroy(gameObject);
        }

    }


}
