using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDirFinder : MonoBehaviour
{

    Rigidbody rb;
   

    List<GameObject> seeds;

    public Vector3 dir = new Vector3(0, -1, 0);
    public float CheckShpereRadus = .5f;
    public bool Check;

    public Transform Tr;
    public GameObject em;
    
    Ray ray;
    RaycastHit hit;
    private float CheckShpereRadius;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray();
        dir = dir.normalized;

        seeds = new List<GameObject>();
        if (Tr == null)
        {
            Tr = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Physics.Raycast(Tr.position, dir,out hit, Mathf.Infinity, ~1))
        {

            Collider[] hitCollider = Physics.OverlapSphere(hit.point, CheckShpereRadius, ~1);

            Debug.DrawLine(Tr.position, hit.point,Color.blue);
            em.transform.position = hit.point;
            foreach (Collider a in hitCollider)
            {
              //  print(hitCollider.Length);
               // print(a.name);
            }

        }
    


        

            
        
    }


}
