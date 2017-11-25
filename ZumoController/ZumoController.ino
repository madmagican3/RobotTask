#include <ZumoMotors.h>

/*
 * This example uses the ZumoMotors library to drive each motor on the Zumo
 * forward, then backward. The yellow user LED is on when a motor should be
 * running forward and off when a motor should be running backward. If a
 * motor on your Zumo has been flipped, you can correct its direction by
 * uncommenting the call to flipLeftMotor() or flipRightMotor() in the setup()
 * function.
 */

#define trigPin 4
#define echoPin 3

ZumoMotors motors;

int speed = 100;
boolean override = false; // This should be false until you want to override it to control the system manually
boolean started = true;//this should be true until the program needs to auto solve the map
boolean pause = false; //this is used to turn the corners
boolean returning = false; //This is used to indicate if the system should return

void setup()
{
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  Serial.begin(9600);
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

void loop()
{
   char val = Serial.read();
   if (returning){// if we're returning
    
   }
   if (started){//if we're on the starting point
    if (val == 'p'){
      started = false;
    }
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
