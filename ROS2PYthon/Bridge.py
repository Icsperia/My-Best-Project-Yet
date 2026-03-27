import machine
import sys
import select

class Bridge:
    def __init__(self):
        # Aici poți lăsa ce era înainte în Bridge sau să adaugi LED-ul de test
        self.led = machine.Pin(2, machine.Pin.OUT)
        print("Bridge ROS 2 Serial pregătit.")

    def update(self, arm_object, nozzle_object):
        """Această metodă va fi chemată în loop-ul principal al robotului"""
        if select.select([sys.stdin], [], [], 0)[0]:
            line = sys.stdin.readline().strip()
            if line:
                try:
                    # Unity trimite: "x,y,z,angle,pump"
                    parts = line.split(',')
                    if len(parts) == 5:
                        nx, ny, nz = float(parts[0]), float(parts[1]), float(parts[2])
                        angle = int(parts[3])
                        pump = int(parts[4])

                        # Executăm mișcarea pe obiectele primite ca argument
                        arm_object.set_position((nx, ny, nz), 20)
                        nozzle_object.set_angle(angle)
                        if pump == 1: nozzle_object.on()
                        else: nozzle_object.off()
                        
                        print(f"Unity Cmd: {nx},{ny},{nz}")
                except:
                    print("Eroare format date Unity")