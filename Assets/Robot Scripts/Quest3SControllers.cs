using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Quest3SControllers : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f; // Viteza 1f e prea mică, am pus 3f
    [SerializeField] private float turnAngle = 45f;
    public XRNode inputSource = XRNode.LeftHand;
    public LayerMask groundLayer;

    private Vector2 inputAxis;
    private XROrigin rig;
    private CharacterController characterController;
    
    private float fallingSpeed;
    private float heightOffset = 0.05f;
    private bool hasTurned = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    void Update()
    {
    
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

  
        if (inputAxis.magnitude > 0.1f)
            Debug.Log("Joystick active: " + inputAxis);
    }

    private void FixedUpdate()
    {

    }




}