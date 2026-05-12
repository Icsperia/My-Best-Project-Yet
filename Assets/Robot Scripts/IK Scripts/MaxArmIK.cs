using UnityEngine;

public class RoboticArmInverseKinematics : MonoBehaviour
{
    [Header("Joints")]
    public ArticulationBody baseRotative;
    public ArticulationBody shoulder;
    public ArticulationBody elbow;

    [Header("Target")]
    public Transform target;

    [Header("Scala robot in Unity (base_link scale)")]
    public float robotScale = 5f;

    [Header("IK Settings")]
    public float jointSpeed = 8f;

    const float L0 = 84.0f;
    const float L1 = 8.2f;
    const float L2 = 128.0f;
    const float L3 = 138.0f;

    private float _currentShoulder;
    private float _currentElbow;

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 robotWorldPos = new Vector3(0f, 1.3f, 0f);
        Vector3 relativePos = target.position - robotWorldPos;

        float scale = robotScale * 0.001f;
        float x = relativePos.z / scale;
        float y = relativePos.x / scale;
        float z = relativePos.y / scale;

        float horizDist = Mathf.Sqrt(x * x + y * y);
        float r = horizDist - L1;
        float h = (z - L0);

        float d2 = r * r + h * h;
        float d  = Mathf.Sqrt(d2);

        if (d > (L2 + L3))
        {
            float s = (L2 + L3) / d;
            r *= s; h *= s;
            d2 = r * r + h * h;
            d  = Mathf.Sqrt(d2);
        }

        if (d < Mathf.Abs(L2 - L3) + 1f)
        {
            float s = (Mathf.Abs(L2 - L3) + 1f) / d;
            r *= s; h *= s;
            d2 = r * r + h * h;
            d  = Mathf.Sqrt(d2);
        }

        float cosA = Mathf.Clamp((L2*L2 + L3*L3 - d2) / (2*L2*L3), -1f, 1f);
        float cosB = Mathf.Clamp((L2*L2 + d2 - L3*L3) / (2*L2*d),  -1f, 1f);

        float a = Mathf.Acos(cosA) * Mathf.Rad2Deg;
        float b = Mathf.Acos(cosB) * Mathf.Rad2Deg;
        float c = Mathf.Atan2(h, r) * Mathf.Rad2Deg;

        //float targetShoulder = 90f - (b + c);
        float targetShoulder = -90f + (b + c);
        float targetElbow    = 90f - a;

        Debug.Log($"[IK] Shoulder={targetShoulder:F1} Elbow={targetElbow:F1} d={d:F0}mm");

        _currentShoulder = Mathf.LerpAngle(_currentShoulder, targetShoulder, Time.fixedDeltaTime * jointSpeed);
        _currentElbow    = Mathf.LerpAngle(_currentElbow,    targetElbow,    Time.fixedDeltaTime * jointSpeed);

        SetJointAngle(shoulder, _currentShoulder);
        SetJointAngle(elbow,    _currentElbow);
    }

    void SetJointAngle(ArticulationBody joint, float angle)
    {
        var drive = joint.xDrive;
        drive.target = angle;
        joint.xDrive = drive;
    }
}