using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpObjects : MonoBehaviour
{
    public InputActionReference mainTriggerLeft;
    public InputActionReference secondaryButton;

    private GameObject holdObject;
    private Rigidbody currentObjectRigidbody;
    private Vector3 lastPumpPosition;
    private Vector3 pumpVelocity;

    [SerializeField] Transform pump;
    [SerializeField] private float pickUpRange = 0.5f;
    [SerializeField] private float pickUpForce = 100f;
    [SerializeField] private LayerMask layerMask;

    private bool wasPressedLastFrame = false;

    void OnEnable()
    {
        if (mainTriggerLeft.action != null) mainTriggerLeft.action.Enable();
        if (secondaryButton.action != null) secondaryButton.action.Enable();
    }

    void Update()
    {
        Debug.DrawRay(pump.position, pump.up * pickUpRange, Color.yellow);
        Debug.DrawRay(pump.position, pump.forward * pickUpRange, Color.blue);
        Debug.DrawRay(pump.position, pump.right * pickUpRange, Color.red);

        pumpVelocity = (pump.position - lastPumpPosition) / Time.deltaTime;
        lastPumpPosition = pump.position;


        if (secondaryButton.action.IsPressed() && holdObject != null)
        {
            DropObject(keepPosition: true);
        }


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
                DropObject(keepPosition: false);
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
            currentObjectRigidbody = pickObj.GetComponent<Rigidbody>();
            currentObjectRigidbody.useGravity = false;
            currentObjectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            currentObjectRigidbody.transform.SetParent(pump);
            currentObjectRigidbody.isKinematic = true;
            holdObject = pickObj;
        }
    }

    void DropObject(bool keepPosition)
    {
        if (currentObjectRigidbody == null) return;

        Vector3 savedPosition = currentObjectRigidbody.transform.position;

        currentObjectRigidbody.transform.SetParent(null);
        currentObjectRigidbody.isKinematic = false;
        currentObjectRigidbody.useGravity = true;
        currentObjectRigidbody.constraints = RigidbodyConstraints.None;

        if (keepPosition)
        {
        
            currentObjectRigidbody.transform.position = savedPosition;
            currentObjectRigidbody.linearVelocity = Vector3.zero;
            currentObjectRigidbody.angularVelocity = Vector3.zero;
        }
        else
        {

            currentObjectRigidbody.transform.position = pump.position;
            currentObjectRigidbody.linearVelocity = pumpVelocity;
        }

        holdObject = null;
        currentObjectRigidbody = null;
    }

    void MoveObject()
    {
        if (Vector3.Distance(holdObject.transform.position, pump.position) > 0.1f)
        {
            Vector3 moveDirection = pump.position - holdObject.transform.position;
            currentObjectRigidbody.AddForce(moveDirection * pickUpForce);
        }
    }
}