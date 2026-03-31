using UnityEngine;
using UnityEngine.InputSystem; // AICI este permis
using Unity.Robotics.UrdfImporter.Control; // Pentru a vedea RotationDirection

public class ToggleXRControls : MonoBehaviour
{
    public ArticulationBody baseRotative;
    public InputActionProperty moveAction; // Aceasta va apărea în Inspector

    void OnEnable()
    {
        if (moveAction.action != null)
            moveAction.action.Enable();
    }

    void Update()
    {
        if (baseRotative == null || moveAction.action == null) return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        float horizontal = input.x;

       
        JointControl jControl = baseRotative.GetComponent<JointControl>();

        if (jControl != null)
        {
            if (horizontal > 0.2f) jControl.direction = RotationDirection.Negative;
            else if (horizontal < -0.2f) jControl.direction = RotationDirection.Positive;
            else jControl.direction = RotationDirection.None;
        }
    
    
    
    }
}