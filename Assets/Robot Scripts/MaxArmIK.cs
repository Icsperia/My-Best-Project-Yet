using UnityEngine;

public class MaxArmIK1 : MonoBehaviour
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

    private float _currentBase;
    private float _currentShoulder;
    private float _currentElbow;

    void Start()
    {
        ConfigureJoint(baseRotative, 10000f, 1000f);
        ConfigureJoint(shoulder,     10000f, 1000f);
        ConfigureJoint(elbow,        10000f, 1000f);
    }

    void ConfigureJoint(ArticulationBody joint, float stiffness, float damping)
    {
        var drive = joint.xDrive;
        drive.stiffness = stiffness;
        drive.damping = damping;
        drive.forceLimit = float.MaxValue;
        joint.xDrive = drive;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Debug.Log($"[TARGET] pozitie mondiala: {target.position}");

        Vector3 robotWorldPos = new Vector3(0f, 1.3f, 0f);
        Vector3 relativePos = target.position - robotWorldPos;

        float scale = robotScale * 0.001f;
        float x = relativePos.x / scale;
        float y = relativePos.z / scale;
        float z = relativePos.y / scale;

        float baseAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        float r = Mathf.Sqrt(x * x + y * y) - L1;
        float h = z - L0;

        float d2 = r * r + h * h;
        float d  = Mathf.Sqrt(d2);

        Debug.Log($"[IK] x={x:F0}mm y={y:F0}mm z={z:F0}mm | r={r:F0} h={h:F0} d={d:F0} | max={L2+L3:F0}mm");

        if (d > (L2 + L3) || d < Mathf.Abs(L2 - L3))
        {
            Debug.LogWarning($"[IK] Tinta in afara razei! d={d:F0} trebuie intre {Mathf.Abs(L2-L3):F0} si {L2+L3:F0}mm");
            return;
        }

        float cosA = Mathf.Clamp((L2*L2 + L3*L3 - d2) / (2*L2*L3), -1f, 1f);
        float cosB = Mathf.Clamp((L2*L2 + d2 - L3*L3) / (2*L2*d),  -1f, 1f);

        float a = Mathf.Acos(cosA) * Mathf.Rad2Deg;
        float b = Mathf.Acos(cosB) * Mathf.Rad2Deg;
        float c = Mathf.Atan2(h, r) * Mathf.Rad2Deg;

        float targetShoulder = 90f - (b + c);
        float targetElbow    = 90f - a;

        Debug.Log($"[IK] Base={-baseAngle:F1} Shoulder={targetShoulder:F1} Elbow={targetElbow:F1}");

        _currentBase     = Mathf.LerpAngle(_currentBase,     -baseAngle,     Time.fixedDeltaTime * jointSpeed);
        _currentShoulder = Mathf.LerpAngle(_currentShoulder, targetShoulder, Time.fixedDeltaTime * jointSpeed);
        _currentElbow    = Mathf.LerpAngle(_currentElbow,    targetElbow,    Time.fixedDeltaTime * jointSpeed);

        SetJointAngle(baseRotative, _currentBase);
        SetJointAngle(shoulder,     _currentShoulder);
        SetJointAngle(elbow,        _currentElbow);
    }

    void SetJointAngle(ArticulationBody joint, float angle)
    {
        var drive = joint.xDrive;
        drive.target = angle;
        joint.xDrive = drive;
    }
}
