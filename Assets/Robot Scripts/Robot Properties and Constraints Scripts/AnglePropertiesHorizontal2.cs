using System;
using UnityEngine;


public class AnglePropertiesHorizontal2 : MonoBehaviour
{
    private ArticulationBody articulationBody;

    [SerializeField] private float upperLimit = 270f;
    [SerializeField] private float lowerLimit = -270f;


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
