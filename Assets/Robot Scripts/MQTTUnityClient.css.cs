using UnityEngine;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using UnityEditor.PackageManager;
using System;

public class MQTTUnityClient : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private MqttClient clientUnity;
    public string brokerIp = "10.206.197.112";
    public string topic = "robot/coodonate";
    
    void Start()
    {
        clientUnity = new MqttClient(brokerIp);
        clientUnity.MqttMsgPublishReceived += onMessageReceived;

    
        clientUnity.Connect(brokerIp);

        clientUnity.Subscribe(new string[] {topic}, new byte[] {MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE});
    
    }

    void onMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string message =  Encoding.UTF8.GetString(e.Message);
        Debug.Log("Data received"+message);
    }
    // Update is called once per frame
    void OQuit()
    {
        if(clientUnity!=null && clientUnity.IsConnected)
        clientUnity.Disconnect();
    }

    void Update()
    {
        
    }
}
