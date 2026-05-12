using UnityEngine;

public class TestJoint : MonoBehaviour
{
    public ArticulationBody baseRotative;

    void Start()
    {
        var drive = baseRotative.xDrive;
        drive.stiffness = 10000f;
        drive.damping = 1000f;
        drive.forceLimit = float.MaxValue;
        drive.target = 45f;
        baseRotative.xDrive = drive;
        Debug.Log($"Stiffness={baseRotative.xDrive.stiffness} Target={baseRotative.xDrive.target}");
    }
}