import socket
import time
from umqttsimple import MQTTClient


class esp32MQTTSubscriber:

        def __init__(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
               self.CLIENT_ID=CLIENT_ID
               self.MQTT_SERVER=MQTT_SERVER
               self.MQTT_PORT=MQTT_PORT
              
        
    


        
        
        def identificationInfoSubs(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
                self.client = MQTTClient(CLIENT_ID, MQTT_SERVER, MQTT_PORT)
        
        def connectToBroker(self):
                try:
                        print('Connect to Mqtt broker...')
                        self.client.connect()
                        print('Connected')
                except Exception as e:
                        print('Failed to connect',e)
                        time.sleep(2)
                        self.connectToBroker()
        
        def sub(self,topic):
            
                self.client.subscribe(topic)
                while True:
                        if True:
                                self.client.wait_msg()
                        else:
                                self.client.check_msg()
                                time.sleep(1)
        def CallBack(topic, msg):
                print('Received message on topic:', topic)
                print('Response:', msg)

               
               