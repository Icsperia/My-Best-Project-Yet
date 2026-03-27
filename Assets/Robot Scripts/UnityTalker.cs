using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std; // Asigură-te că ai generat std_msgs în meniul Robotics!

public class MaxArmCommander : MonoBehaviour
{
    ROSConnection ros;
    public string topicName = "cmd_robot";

    void Start()
    {
        // Inițializează conexiunea
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<StringMsg>(topicName);

            

            StringMsg msg = new StringMsg("Hello there");
        ros.Publish(topicName, msg);
    }

    void Update()
    {
        // Exemplu: Apasă tasta 'W' pentru a mișca brațul la o poziție fixă
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     // Format: "X, Y, Z, Angle, Pump"
        //     // Mergi la X=0, Y=150, Z=100, Unghi=0, Pompa=1 (Pornită)
        //     SendRobotCommand("0,150,100,0,1");
        // }

        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     // Mergi la X=0, Y=150, Z=50, Unghi=0, Pompa=0 (Oprită)
        //     SendRobotCommand("0,150,50,0,0");
        // }


        // Debug.Log("Trimis spre ROS 2: ");

       

    }

    void SendRobotCommand(string command)
    {
        StringMsg msg = new StringMsg(command);
        ros.Publish(topicName, msg);
        Debug.Log("Trimis spre ROS 2: " + command);
    }
}