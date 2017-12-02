#include <ZumoMotors.h>
#include <ZumoReflectanceSensorArray.h>
#include <Pushbutton.h>
#include <QTRSensors.h>

#define trigPin 4
#define echoPin 3
#define ledPin 13

Pushbutton button(ZUMO_BUTTON);
ZumoMotors motors;
ZumoReflectanceSensorArray reflectanceSensors;

int sensorArr[6];
int speed = 100;
int threshold = 10000;//this is the colour threshold for the sensors
boolean override = false; // This should be false until you want to override it to control the system manually
boolean started = true;//this should be true until the program needs to auto solve the map
boolean pause = false; //this is used to turn the corners
boolean returning = false; //This is used to indicate if the system should return

void setup()
{
  Serial.begin(9600);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  calibrate();
}

void loop()
{
   char val = Serial.read();
   if (returning){// if we're returning
    
   }
   if (started){//if we're on the starting point
    if (val == 'p'){
      started = false;
      stop();
      return;
    }
    start();
   }
   if (override){//if we've overidden the default system to control the robot manually
     switch (val){
      case 'd': d();
      break;
      case 's': s();
      break;
      case 'a':a();
      break;
      case 'w':w();
      break;
      case 'z':stop();
      break;
      default:
      break; 
   } 
   }


   if (pause){// if we're pausing to get it to check a corridor or a door
     if (val == 'd'){//right
       
     }else if (val == 'a'){//left
      
     }
   }
   if (val == 'l'){// if the value is l we want to start the pause
    pause = true;
   }
   
   
  delay(150);
  
}

void calibrate(){
    reflectanceSensors.init();
    Serial.println("Please place the zumo in the correct location then press the button to initiate the search and rescue operation");
    reflectanceSensors.calibrate();
    for (int i = 1; i <= 6; i++){
      if (i % 2 == 0){
        motors.setLeftSpeed(-400);
        motors.setLeftSpeed(-400);
      }else{
        motors.setLeftSpeed(400);
        motors.setLeftSpeed(400);
      }
      delay (200);
    }
    button.waitForButton();
}

void start(){
  reflectanceSensors.readCalibrated(sensorArr);
      for (byte i = 0; i < 6; i++){
        Serial.println(sensorArr[i]);
      }
      delay( 1000);
  return;
    for (byte i = 0; i < 6; i++)
      {
        reflectanceSensors.readLine(sensorArr);
        Serial.println(sensorArr[i]);
        if (sensorArr[i] > threshold)
        {
          stop();
          Serial.println("Wall detected");
          returning = true;
          started = false;
          return;
        }
      }
     w();
}

void w (){//foward
  motors.setLeftSpeed (speed);
  motors.setRightSpeed(speed);
}

void a(){//left
  motors.setLeftSpeed((speed*2)*-1);
  motors.setRightSpeed(speed*2);
}

void d(){//right
  motors.setLeftSpeed(speed*2);
  motors.setRightSpeed((speed*2)*-1);
}
  
void s(){//backwards
  motors.setLeftSpeed(-speed);
  motors.setRightSpeed(-speed);
}

void stop(){
  motors.setLeftSpeed(0);
  motors.setRightSpeed(0);
}

bool checkObstacle(){// returns true if there's an obstacle within 10cm's
  long duration, distance;
  digitalWrite(trigPin, LOW);  
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10); 
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH);
  distance = (duration/2) / 29.1;
  Serial.println(distance);
  if (distance<10){
    return false;
  }
  return true;
}


