
using UnityEngine;
using UnityEngine.InputSystem;



public class PickUpObjects : MonoBehaviour
{
    public InputActionReference mainTriggerLeft;
    public InputActionReference secondaryTriggerLeft;
    private GameObject holdObject;

    private Rigidbody CurrentObjectRigidbody;

    private Vector3 lastPumpPosition;

    private Vector3 pumpVelocity;

    [SerializeField] Transform pump;


    [SerializeField] private float pickUpRange = 0.5f;
    [SerializeField] private float prickUpForce = 100f;

    [SerializeField] private LayerMask layerMask;
    private bool wasPressedLastFrame = false;
    void OnEnable()
    {
        if (mainTriggerLeft.action != null)
            mainTriggerLeft.action.Enable();

        if (secondaryTriggerLeft.action != null)
            secondaryTriggerLeft.action.Enable();


    }


    void Start()
    {

    }

  
    void Update()
    {
    Debug.DrawRay(pump.position, pump.up * pickUpRange, Color.yellow);
    

    Debug.DrawRay(pump.position, pump.forward * pickUpRange, Color.blue);

    Debug.DrawRay(pump.position, pump.right * pickUpRange, Color.red);
    
   
    Debug.DrawRay(pump.position, pump.forward * pickUpRange, Color.blue);

    pumpVelocity = (pump.position - lastPumpPosition)/Time.deltaTime;
    lastPumpPosition = pump.position;
       bool isPressed = mainTriggerLeft.action.IsPressed();

        if (isPressed && !wasPressedLastFrame)
        {
            if (holdObject == null)
            {
                RaycastHit hit;
               
                if (Physics.Raycast(pump.position, pump.right, out hit, pickUpRange, layerMask))
                {
                    PickupObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }

        wasPressedLastFrame = isPressed; 

        if (holdObject != null)
        {
            MoveObject();
        }
    }

       

 void PickupObject(GameObject pickObj)
        {
            if (pickObj.GetComponent<Rigidbody>())
            {

                CurrentObjectRigidbody = pickObj.GetComponent<Rigidbody>();
                CurrentObjectRigidbody.useGravity = false;
                CurrentObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                CurrentObjectRigidbody.transform.SetParent(pump);
                CurrentObjectRigidbody.isKinematic = true;
                holdObject = pickObj;
            }
        }


        void DropObject()
        {

            CurrentObjectRigidbody.useGravity = true;
            CurrentObjectRigidbody.constraints = RigidbodyConstraints.None;
            CurrentObjectRigidbody.transform.SetParent(null);
            CurrentObjectRigidbody.isKinematic = false;
            holdObject = null;
            CurrentObjectRigidbody.transform.position = pump.position;
            CurrentObjectRigidbody.linearVelocity = pumpVelocity;
        }

        void MoveObject()
        {
            if (Vector3.Distance(holdObject.transform.position, pump.position) > 0.1f)
            {
                Vector3 moveDirection = (pump.position - holdObject.transform.position);
                CurrentObjectRigidbody.AddForce(moveDirection * prickUpForce);
            }
        }
}
