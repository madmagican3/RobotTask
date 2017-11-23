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

void setup()
{
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  Serial.begin(9600);
}

void w (){
  //This sets the motor to run foward
  motors.setLeftSpeed (speed);
  motors.setRightSpeed(speed);
}
void a(){
  //run the following motor foward
  motors.setLeftSpeed((speed*2)*-1);
  motors.setRightSpeed(speed*2);
}
void d(){
  motors.setLeftSpeed(speed*2);
  motors.setRightSpeed((speed*2)*-1);
}
void avoidObstacle(){
 Serial.write("avoiding");
 motors.setLeftSpeed(speed*2);
  motors.setRightSpeed(-speed*2);
  delay (1000);
  motors.setLeftSpeed(0);
  motors.setRightSpeed(0);
  }
void s(){
  motors.setLeftSpeed(-speed);
  motors.setRightSpeed(-speed);
}
void y(){
   motors.setLeftSpeed (speed- (speed *0.75));
  motors.setRightSpeed(speed); 
}
void u(){
    motors.setLeftSpeed (speed);
  motors.setRightSpeed(speed - (speed *0.75));
}
void h(){
    motors.setLeftSpeed (-speed - (speed *0.75));
  motors.setRightSpeed(-speed);
}
void j(){
    motors.setLeftSpeed (-speed);
  motors.setRightSpeed(-speed - (speed * 0.75));
}
void stop(){
  motors.setLeftSpeed(0);
  motors.setRightSpeed(0);
}
bool checkObstacle(){
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
   if (checkObstacle()){
   
      switch (val){
      case 'd': d();
      break;
      case 's': s();
      break;
      case 'a':a();
      break;
      case 'w':w();
      break;
      case 'y':y();
      break;
      case 'u':u();
      break;
      case 'h':h();
      break;
      case 'j':j();
      break;
      case 'z':stop();
      break;
      default:
      break;
      
  }}else{
    avoidObstacle();
  }

  delay(150);
  
}
