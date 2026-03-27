import socket
import datetime


localIp = "0.0.0.0"
localPort = 5005
bufferSize = 1024
unitySocketServer = socket.socket(
family=socket.AF_INET,
type = socket.SOCK_DGRAM
)

unitySocketServer.bind((localIp, localPort))
print("Unity is up and listening")


while True:
    esp32CoordonatesAndAngles, addr = unitySocketServer.recvfrom(bufferSize)
    print('Connection from address', addr)
    date = datetime.datetime.now()

    
   
    CoordonatesAndAngles = esp32CoordonatesAndAngles.decode('utf-8')

    receivedCoordonatesAndAngles = CoordonatesAndAngles.split(',')
    print("Data received", receivedCoordonatesAndAngles)

    msg = "Hello Esp"
    bytesToSend  = str.encode(msg)
    unitySocketServer.sendto(bytesToSend, addr)