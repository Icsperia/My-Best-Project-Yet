using System;
using UnityEngine;

public class AnglePropertiesVerticalArm : MonoBehaviour
{
    private ArticulationBody articulationBody;

    [SerializeField] private float upperLimit;
    [SerializeField] private float lowerLimit;


    void Start()
    {
        articulationBody = GetComponent<ArticulationBody>();
        ApplySettings();
        Invoke(nameof(ApplySettings), 0.1f);
    }

    public void ApplySettings()
    {
        if (articulationBody == null) return;
        Drive(lowerLimit, upperLimit);


    }
    public void Drive(float lowerLimit, float upperLimit)
    {
        ArticulationDrive articulationDrive = articulationBody.xDrive;
        articulationDrive.upperLimit = upperLimit;
        articulationDrive.lowerLimit = lowerLimit;

        articulationDrive.driveType = ArticulationDriveType.Force;

        articulationBody.xDrive = articulationDrive;


    }

}
