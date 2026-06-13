import socket
import time
from umqttsimple import MQTTClient


class esp32MQTTHandler:

        def __init__(self):

            self.client = None
            self.fbaseAngle = 0.0
            self.verticalArmAngle=0.0
            self.upDownAngle=0.0
            self.armIniPos = 0.0
            self.noozleValues = 0.0
            self.noozleValues1 = 0.0
            
        
        
        def identificationInfo(self,CLIENT_ID, MQTT_SERVER, MQTT_PORT):
                self.client = MQTTClient(CLIENT_ID, MQTT_SERVER, MQTT_PORT)
                self.client.set_callback(self.CallBack)
        def CallBack(self, topic, msg):
            try:
        
                topic_str = topic.decode()  
                msgReceived= float(msg.decode())
              
        
                if topic_str == "rotativeBase/topic":
                    self.fbaseAngle = msgReceived 
                elif topic_str == "verticalArm/topic":
                    self.verticalArmAngle = msgReceived 
                elif topic_str == "upDownSegment/topic":
                    self.upDownAngle = msgReceived 
                elif topic_str == "armHome/topic":
                    self.armIniPos = msgReceived
                elif topic_str == "noozle/topic":
                    self.noozleValues = msgReceived
                elif topic_str == "noozle1/topic":
                    self.noozleValues1 = msgReceived

            except ValueError:
                print("Eroare conversie float: Nu pot converti '{}'")
            except Exception as e:
                print("Unexpected error in callback:", e)
        
      
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


        def pub(self,topic, msg):
                self.client.publish(topic, msg)
                


               
               




