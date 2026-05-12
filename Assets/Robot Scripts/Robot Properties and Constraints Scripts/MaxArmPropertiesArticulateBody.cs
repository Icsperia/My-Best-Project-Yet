using System;
using UnityEngine;


public class MaxPropertiesArticulateBody : MonoBehaviour
{

    [Header("Main Components")]

    [SerializeField] private ArticulationBody baseLink;

    [SerializeField] private ArticulationBody baseRotative;
    [SerializeField] private ArticulationBody verticalArm;

    [SerializeField] private ArticulationBody upDown;
    [SerializeField] private ArticulationBody horizontalArm;
    [SerializeField] private ArticulationBody noozle;

    [Header("Secondary Components")]
    [SerializeField] private ArticulationBody vertical1;
    [SerializeField] private ArticulationBody vertical2;

    [SerializeField] private ArticulationBody horizontal1;

    [SerializeField] private ArticulationBody horizontal2;
    [SerializeField] private ArticulationBody noozleSupport;

[Header("xDrive Custom")]

    [SerializeField]  private float stiffness;
    [SerializeField]  private float damping;
    [SerializeField]  private float limit;




    void Start()
    {

        ApplySettings();
        Invoke(nameof(ApplySettings), 0.1f);
    }

    public void ApplySettings()
    {

        Drive(baseLink, stiffness, damping, limit);
        Drive(baseRotative, stiffness, damping, limit);
        Drive(verticalArm, stiffness, damping, limit);
        Drive(upDown, stiffness, damping, limit);
        Drive(horizontalArm, stiffness, damping, limit);
        Drive(noozle, stiffness, damping, limit);

        
        Drive(vertical1, stiffness, damping, limit);
        Drive(vertical2, stiffness, damping, limit);
        Drive(horizontal1, stiffness, damping, limit);
        Drive(horizontal2, stiffness, damping, limit);
        Drive(noozleSupport, stiffness, damping, limit);


    }
    public void Drive(ArticulationBody articulationBody, float stifness, float damping, float forceLimit)
    {

        ArticulationDrive articulationDrive = articulationBody.xDrive;

        articulationDrive.stiffness = stifness;
        articulationDrive.damping = damping;
        articulationDrive.forceLimit = forceLimit;
        articulationDrive.driveType = ArticulationDriveType.Force;

        articulationBody.xDrive = articulationDrive;


    }

}
