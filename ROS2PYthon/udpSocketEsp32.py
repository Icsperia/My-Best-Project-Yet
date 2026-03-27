import socket
import time

unityAddPort= ("192.168.137.1", 5005)
bufferSize = 1024
udpEsp32SocketClient = socket.socket(
family= socket.AF_INET,
type = socket.SOCK_DGRAM)

class udpSocketEsp32:
        def sendCoordonateAndAngle(self, xNozzleCoordonate, yNozzleCoordonate,zNozzleCoordonate,pos_servo_1,pos_servo_2,pos_servo_3, nozzleAngle, timeSleep):

            
                leftServoAngle = pos_servo_1.get_position(1)
                rightServoAngle = pos_servo_2.get_position(2)
                rotativeServoAngle = pos_servo_3.get_position(3)

                coodonatesAndAngles = "{},{},{},{},{},{},{}".format(
                xNozzleCoordonate, 
                yNozzleCoordonate, 
                zNozzleCoordonate, 
                leftServoAngle, 
                rightServoAngle, 
                rotativeServoAngle, 
                nozzleAngle
        )
                udpEsp32SocketClient.sendto( coodonatesAndAngles.encode('utf-8'), unityAddPort)

                time.sleep(timeSleep)