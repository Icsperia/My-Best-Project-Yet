using System;
using UnityEngine;

[RequireComponent(typeof(ArticulationBody))]
public class PropertiesArticulateBody0 : MonoBehaviour
{
    private ArticulationBody articulationBody;
    [SerializeField] private float stiffness = 10000f;
    [SerializeField] private float damping = 10f;
    [SerializeField] private float limit = 10000f;
    [SerializeField] private float upperLimit = 270f;
    [SerializeField] private float lowerLimit = -270f;
    [SerializeField] private float maxVelocityLimit = 1000f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float acceleration = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        articulationBody = GetComponent<ArticulationBody>();
        ApplySettings();
        Invoke(nameof(ApplySettings), 0.1f);
    }

    public void ApplySettings()
    {
        if (articulationBody == null) return;
        Drive(stiffness, damping, limit, lowerLimit, upperLimit, maxVelocityLimit);


    }
    public void Drive(float stifness, float damping, float forceLimit, float lowerLimit, float upperLimit, float maxVelocityLimit)
    {
        ArticulationDrive articulationDrive = articulationBody.xDrive;
        articulationDrive.upperLimit = upperLimit;
        articulationDrive.lowerLimit = lowerLimit;
        articulationDrive.stiffness = stifness;
        articulationDrive.damping = damping;
        articulationDrive.forceLimit = forceLimit;
        articulationBody.maxAngularVelocity = maxVelocityLimit;
        articulationDrive.driveType = ArticulationDriveType.Force;
        
        articulationBody.xDrive = articulationDrive;


    }
    // Update is called once per frame
    void Update()
    {

    }
}
