
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InverseKinematic : MonoBehaviour
{
public int Iterations = 10;
public float Delta = 0.001f;
public Transform[] joints;//piesele robotului meu
public Transform Target; //destinatia la care trebuie sa ajunga piesele

private float[] jointLength;//lungimea segmentelor robotului
private Vector3[] jointPositions;//pozitiile joint-urilor 

private float totalLength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

//calcul distanta dintre puncte        
int jointCount = joints.Length;
jointLength = new  float[jointCount - 1];
jointPositions = new Vector3[jointCount];
totalLength = 0;

for(int i = 0; i < jointCount - 1; i++)
        {
            jointLength[i] = Vector3.Distance(joints[i].position, joints[i+1].position);
            totalLength+=jointLength[i];

        }
  for(int i = 0; i < joints.Length ; i++)
            {
               jointPositions[i]= joints[i].position;

            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target == null) return;
    
//pentru miscare in linie dreapta

          //algoritmul Fabrik pentru celelalte miscari
           //obtinere pozitii
            for(int i = 0; i < joints.Length ; i++)
            {
               jointPositions[i]= joints[i].position;

            }

            if ( (Target.position - joints[0].position).sqrMagnitude>=(totalLength*totalLength))
            {
          
                Vector3 direction = (Target.position - joints[0].position).normalized;
                        for(int i = 0; i < jointLength.Length; i++) 
                jointPositions[i+1]= jointPositions[i]+direction*jointLength[i];
        }else
        {
           
           //Algortimul Fabrik
            for(int Iteration = 0; Iteration < Iterations; Iteration++)
           
             {
//backward part
                for(int i = jointPositions.Length - 1;i>0;i--)
                {
                    if(i==jointPositions.Length-1)
                    jointPositions[i] = Target.position;
                    else
                    
                        jointPositions[i]= jointPositions[i+1] + (jointPositions[i]-jointPositions[i+1]).normalized*jointLength[i];
                    
                }
               //forward part
               jointPositions[0] = joints[0].position;
               for(int i = 1;i<jointPositions.Length;i++)
                     jointPositions[i]= jointPositions[i-1] + (jointPositions[i]-jointPositions[i-1]).normalized*jointLength[i-1];

                if((jointPositions[jointPositions.Length-1]-Target.position).sqrMagnitude < Delta*Delta)
                break;

            }
        }
                
                
            
            
            //setare pozitii
            for(int i = 0; i < joints.Length-1; i++)
            {
                Vector3 targetDirection = jointPositions[i+1]- jointPositions[i];
            
            if(targetDirection != Vector3.zero)
            {
           
           Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

           joints[joints.Length-1].position = jointPositions[jointPositions.Length-1];
           
           joints[joints.Length-1].rotation = Target.rotation;
            //     if (i == 0)
            //     {
            //         Vector3 flatDirection = new Vector3(targetDirection.x, 0 , targetDirection.z);
            //     joints[i].rotation = Quaternion.LookRotation(flatDirection);
            //     }
            //     else
            //     {
            //            joints[i].transform.localRotation = Quaternion.FromToRotation(Vector3.down,targetDirection);//pentru a controla directia de mers
            //     }

            
            
            
            // // joints[i].rotation = targetRotation;
            // }
            }
       
        
        
        }
    }
}

