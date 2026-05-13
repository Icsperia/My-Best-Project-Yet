using UnityEngine;
using MQTTnet;
using MQTTnet.Client;
using System.Threading.Tasks;
using System;
using UnityEngine.InputSystem;

public class MQTTUnityPublisher : MonoBehaviour
{
    public ArticulationBody rotativeBase;
    public ArticulationBody verticalArm;
    public ArticulationBody upDownSegment;

    public InputActionReference xButton;

        public InputActionReference mainTriggerLeft;

    public string brokerIp = "10.104.183.112";
    public int brokerPort = 1883;

    private IMqttClient mqttClient;
    

    public float sensitivity = 500f; 
    public float publishDelay = 0.05f; 
    public int deadzone = 1; 

    private float lastRawX, lastRawY, lastRawZ;
    private float nextPublishTime;


  void   OnEnable()
    {
        if(xButton.action!=null)
        xButton.action.Enable();

             if(mainTriggerLeft.action!=null)
     mainTriggerLeft.action.Enable();
    
    }
    async void Start()
    {
        await ConnectToBroker();
       
        lastRawX = rotativeBase.jointPosition[0];
        lastRawY = verticalArm.jointPosition[0];
        lastRawZ = upDownSegment.jointPosition[0];
    }

async void Update()
{
    if (mqttClient == null || !mqttClient.IsConnected) return;


    await SendNoozleValues();

 
    if (Time.time >= nextPublishTime)
    {
            await SendNoozleValues();
        
        nextPublishTime = Time.time + publishDelay;
        await SendRelativeData();
        

        if (xButton.action.ReadValue<float>() > 0.5f)
        {
            await SendBoolValues();
        }
    }
}
    async Task ConnectToBroker()
    {
        var factory = new MqttFactory();
        mqttClient = factory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(brokerIp, brokerPort)
            .WithClientId("UnityPublisher_Relative")
            .Build();

        await mqttClient.ConnectAsync(options);
        Debug.Log("Conectat la Broker MQTT");
    }

    async Task SendRelativeData()
    {
        
        float curX = rotativeBase.jointPosition[0];
        float curY = verticalArm.jointPosition[0];
        float curZ = upDownSegment.jointPosition[0];

      
        int deltaX = Mathf.RoundToInt((curX - lastRawX) * sensitivity);
        int deltaY = Mathf.RoundToInt((curY - lastRawY) * sensitivity);
        int deltaZ = Mathf.RoundToInt(-(curZ - lastRawZ) * sensitivity);

    
        if (Mathf.Abs(deltaX) >= deadzone) 
        {
            await PublishInt("rotativeBase/topic", deltaX);
            lastRawX = curX;
        }

        if (Mathf.Abs(deltaY) >= deadzone)
        {
            await PublishInt("verticalArm/topic", deltaY);
            lastRawY = curY;
        }

        if (Mathf.Abs(deltaZ) >= deadzone)
        {
            await PublishInt("upDownSegment/topic", deltaZ);
            lastRawZ = curZ;
        }

    
   
    }

    async Task SendBoolValues()
    {
            if(xButton.action != null)

        {
        
        float buttonValue = xButton.action.ReadValue<float>();
        await PublishFloat("armHome/topic", buttonValue);
        Debug.Log(buttonValue );
            
   
        }

    }
    async Task PublishInt(string topic, int value)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(value.ToString())
            .Build();
        await mqttClient.PublishAsync(message);
    }

   
      async Task PublishFloat(string topic, float value)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(value.ToString())
            .Build();
        await mqttClient.PublishAsync(message);
    }

private bool nozzleIsActive = false;
private bool wasPressedLastFrame = false; 

async Task SendNoozleValues()
{
    if (mainTriggerLeft.action == null) return;


    float triggerValue = mainTriggerLeft.action.ReadValue<float>();
    
  
    bool isCurrentlyPressed = triggerValue > 0.5f;

    if (isCurrentlyPressed && !wasPressedLastFrame)
    {
        nozzleIsActive = !nozzleIsActive; 
        wasPressedLastFrame= true;
        float valueToSend = nozzleIsActive ? 1.0f : 0.0f;

        await PublishFloat("noozle/topic", valueToSend);
        Debug.Log("Toggle Nozzle: " + (valueToSend));

    }

  
if (!isCurrentlyPressed)
    {
        wasPressedLastFrame = false;
    }
}
}