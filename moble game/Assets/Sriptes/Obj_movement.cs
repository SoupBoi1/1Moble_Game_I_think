using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Obj_movement : MonoBehaviour
{
    public LayerMask layerMask;
    public InputActionAsset inputActionsA;
    InputActionMap gameplay_Map_A;
    InputAction screen_position_A;
    InputAction click_A;

    Ray ray;
    RaycastHit hitData;

    Vector2 t;
    bool click;
    GameObject moveit;
    public GameObject mover;
    bool canmoveit;
    Vector3 worldPosition;
    Vector3 Holdpos;
    //public Transform yoffset;
    public Vector3 offset = new Vector3(0,3,0);
    public float srping=10;
    // Start is called before the first frame update

    bool isHoes;
    void Start()
    {
        gameplay_Map_A = inputActionsA.FindActionMap("gameplay");
        gameplay_Map_A.Enable();
        screen_position_A = gameplay_Map_A.FindAction("screen_position");
        click_A = gameplay_Map_A.FindAction("click");

        screen_position_A.performed += context => On_Torch(context);
        screen_position_A.canceled += ctx => On_Torch(ctx);

        click_A.performed += context => onClick(context);
        click_A.canceled += ctx => onClick(ctx);
    }

    // Update is called once per frame
    void Update()
    {
        ScreenRay_castdata(ray,t,out hitData);
        

        if (click == true )
        {
            if (hitData.rigidbody != null && canmoveit == true)
            {
                moveit = GameObject.Find(hitData.rigidbody.name);
                moveit.layer = 2;
               //print(moveit.name);
                canmoveit = false;
                if (moveit.name == "hoe")
                {

                    HingAobj(moveit, mover, worldPosition);
                }
                else
                {
                    SpringAObj(moveit, mover, worldPosition);
                }
                

                //yoffset.position = offset;

            }
            if ( moveit != null && moveit.GetComponent<Rigidbody>().isKinematic == false)
            {
                // moveit.transform.position = worldPosition + offset;
                if (moveit.name == "hoe")
                {
                    isHoes = true;
                    
                    //mover.transform.position = new Vector3(worldPosition.x, 3f/*moveit.transform.position.y*/,worldPosition.z);
                    mover.transform.position = worldPosition;

                }
                else{
                    mover.transform.position = worldPosition + offset;

                }
               
                
            }

        }
        else if (click == false)
        {
            if (moveit != false)
            {
                moveit.layer = 0;
                Destroy(moveit.GetComponent<ConfigurableJoint>());
                //yoffset.position = new Vector3(-50, -1, -50);
                Destroy(moveit.GetComponent<HingeJoint>());
            }

            isHoes = false;
            canmoveit = true;
        }
        
    }

    void On_Torch(InputAction.CallbackContext context)
    {
        t = context.ReadValue<Vector2>();

    }

    void onClick(InputAction.CallbackContext context)
    {
        if (context.performed) {
            click = true;
            
        }
        else
        {
            click = false;
        }
    }

    void ScreenRay_castdata(Ray ray, Vector2 ScreenPos, out RaycastHit hitData)
    {
        ray = Camera.main.ScreenPointToRay(ScreenPos);
     

        if (Physics.Raycast(ray, out hitData, 1000, ~layerMask))
        {
            Vector3 pos = Camera.main.transform.position;

            // Vector3 dir = ((worldPosition+offset) - pos).normalized;

            // Debug.DrawLine(pos, pos + dir * 12f, Color.red);

            worldPosition = hitData.point;
            Debug.DrawLine(pos, worldPosition, Color.red);

            //Holdpos = pos + dir * 12f;
        }

    }

    void SpringAObj(GameObject gameObject, GameObject secound, Vector3 worldPosition)
    {

        ConfigurableJoint configurableJoint;

        if (gameObject.GetComponent<ConfigurableJoint>())
        {
            configurableJoint = gameObject.GetComponent<ConfigurableJoint>();
        }
        else
        {
            configurableJoint = gameObject.AddComponent<ConfigurableJoint>();
        }


       
        Vector3 anchor = gameObject.transform.position - worldPosition;
       // print(anchor);
        configurableJoint.anchor = anchor;

        secound.transform.position = gameObject.transform.position - anchor;

        configurableJoint.connectedBody = secound.GetComponent<Rigidbody>();

        configurableJoint.xMotion = ConfigurableJointMotion.Limited;
        configurableJoint.yMotion = ConfigurableJointMotion.Limited;
        configurableJoint.zMotion = ConfigurableJointMotion.Limited;

        JointDrive drive = new JointDrive();
        drive.mode = JointDriveMode.Position;
        drive.positionSpring = 3;
        drive.maximumForce = 0.15f;
        drive.positionDamper = 0;
        

        configurableJoint.yDrive = drive;
        configurableJoint.xDrive = drive;
        configurableJoint.zDrive = drive;

        SoftJointLimitSpring limitSpring = new SoftJointLimitSpring();
        limitSpring.spring = srping;
        limitSpring.damper = 1;

        configurableJoint.linearLimitSpring = limitSpring;

        SoftJointLimit leinarl = new SoftJointLimit();
        leinarl.limit = 0f;
        leinarl.contactDistance = 1;
        leinarl.bounciness = 1;

        configurableJoint.linearLimit = leinarl;

    }

    void HingAobj(GameObject gameObject, GameObject secound, Vector3 worldPosition)
    {
        HingeJoint hingeJoint;
        Transform Hpoint = gameObject.transform.Find("point");
        secound.transform.position = Hpoint.position;
        if (gameObject.GetComponent<HingeJoint>())
        {
            hingeJoint = gameObject.GetComponent<HingeJoint>();
        }
        else
        {
            hingeJoint = gameObject.AddComponent<HingeJoint>();
        }
       

        hingeJoint.connectedAnchor = secound.transform.position;
        hingeJoint.connectedBody = secound.GetComponent<Rigidbody>();

        
    }
}


