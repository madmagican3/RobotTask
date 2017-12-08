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

int sensorArr[6];//This defines the sensor array
int speed = 100;//This is the speed the robot should run at 
int threshold = 500;//this is the colour threshold for the sensors
boolean overrideAutoRun = false; // This should be false until you want to override it to control the system manually
boolean runningMaze = true;//this should be true until the program needs to auto solve the map
boolean pause = false; //this is used to turn the corners
boolean returning = false; //This is used to indicate if the system should return
char path[100];//This array is used as a list in order to work out the turns the arduino is taking
int pathLength =0;//This is used to keep track of the actual length of the array above
int roomList[100];//This is used to keep track of the rooms, a is left with item, d is right with item, c is left with no item, v is right with no item
int roomNo = 0;//This is used to keep track of the actual length of the array above

void setup()
{
  Serial.begin(9600);
  connectToProgram();
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  //define the pins and then calibrate the program
  calibrate();
}

void loop()
{
   char val = Serial.read();
   checkChar(val);
   if (returning){// if we're returning
    
   }
   
   if (overrideAutoRun){//if we've overidden the default system to control the robot manually
      ControllerOverride();
   }
   
   if (runningMaze){//if we want it to run the maze normally
    runMaze();
   }
   
   if (pause){// if we're pausing to get it to check a corridor or a door
     runPause(val);
   }
   
    delay(150);
}
//This controls the override of the system
void ControllerOverride(){
  bool FinishedOverride = false;
  bool doneVBefore = false;
  while (!FinishedOverride){
     char val = Serial.read();
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
      case 'c': //this is the corridor case
      finishedOverride = true;
      break;
      case 'v'://This is the room case
      if (!doneVBefore){
        checkItem();
      }else {
        finishedOverride = true;
      }
      break;
      default:FinishedOverride = true;
        break; 
    } 
  }
}

//This should be the code to handle which direction we're going too after a pause
void runPause ( char val){
     if (val == 'd'){//corridor right
       path[pathLength] = 'd';
       pathLength += 1;
       Serial.println("Please turn me  right");      
     }else if (val == 'a'){//corridor left
       path[pathLength] = 'a';
       pathLength += 1;
       Serial.println("Please turn me left");
     }else if (val =='b'){//roomLeft
      roomList[roomNo] = checkRoom('a');
      roomNo += 1;
      checkChar('n');
     }else if (val == 'm'){//roomRight
      roomList[roomNo] = checkRoom('d');
      roomNo += 1;
      checkChar('n');
     }
}
//This should check the room
char checkRoom(char leftRight){
  Serial.println("Now searching room No |");
  Serial.println(roomNo);
  if (leftRight == 'm'){
      Serial.println("| on the right|");
  }else {
    Serial.println("| on the left|");
  }
  if (checkItem()){
    Serial.println("There is a person in there, that's bad!");
    if (leftRight == 'a'){
      return 'a';//left with item
    }else {
      return 'd';//right with item
    }
  }else {
   Serial.println("Nothing here boss"); 
   if (leftRight == 'a'){
    return 'c';//left no item
   }else {
    return 'v';//right no item
   }
  }
}

//This runs until the serial array is connected to via the c# program 
void connectToProgram(){
  while (true){
      char val = Serial.read();
      if (val == 'c'){
        Serial.println('d');
        delay(1000);
        return;
      }
      delay(50);
  }
}

//This should check the char input and then modify the booleans to control the flow if appropriate
void checkChar(char val){
   if (val == 'p'){// if the value is p we want to start the pause
      stop();
      Serial.println("|h|");//pass a non human readable string to be transformed in the program
      pause = true;
      returning = false;
      runningMaze = false;
      overrideAutoRun = false;
   } else if (val == 'o'){//If the value is o we want to start the overide
      overrideAutoRun = true;
      returning = false;
      runningMaze = false;
      pause = false;
   }else if (val == 'n'){//if the value is n we want to return to doing the run
      runningMaze = true;
      returning = false;
      pause = false;
      overrideAutoRun = false;
   }else if (val == ';'){// if the value is ; we want to start returning 
      runningMaze = false;
      returning = true;
      pause = false;
      overrideAutoRun = false;
   }
}

//This will calibrate the sensors by asking the user to place it facing the black line then putting it in the normal spot
void calibrate(){
  //inform the user that we want to start calibrating then wait for button
  delay(100);
    reflectanceSensors.init();
    digitalWrite(ledPin, HIGH);
    Serial.println("Please place the zumo facing a black line in order to allow for calibration|"); 
    Serial.println("Then please press the button on the zumo once that is done");
    button.waitForButton();
    //Calibrate the sensors
    for (int i = 1; i < 7; i++){
      delay (1500);      
      reflectanceSensors.calibrate();
      if (i % 2 == 0){
        motors.setLeftSpeed(-50);
        motors.setRightSpeed(-50);
      }else{
        motors.setLeftSpeed(50);
        motors.setRightSpeed(50);
      }      
    } 
    stop();
    digitalWrite(ledPin, LOW);
    Serial.println("Please place the zumo in the correct starting position|");
    Serial.println("Press the button when ready to start");
    button.waitForButton();
    runningMaze = true;
    //Stop and wait to be put into the ready position
}

//This should be the default run if we're using the system 
void runMaze(){
  reflectanceSensors.readCalibrated(sensorArr);
    for (byte i = 0; i < 6; i++)
      {
        reflectanceSensors.readLine(sensorArr);
        if (sensorArr[i] > threshold)//if we've encountered a wall
        {
          Serial.println("w|");//as specified in the spec, we tell them the corridor no, which is pathlength +1 (as we dont start on the 0th indice
          //for human counting
          Serial.println(pathLength);
          delay(200);
          checkChar('p');//This will pause the run
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

void turnRight(){//This should be a 90 degree turn (or as close as possible) for simplicities sake towards the right
  motors.setLeftSpeed(-200);
  motors.setRightSpeed(200);
  delay(400);
  stop();
}
void turnLeft(){//This should be a 90 degree turn (or as close as possible) for simplicities sake towards the left
  motors.setLeftSpeed(200);
  motors.setRightSpeed(-200);
  delay(400);
  stop();
}

bool checkItem(){// returns true if there's an item within 10cm's
  w();
  delay (200);
  stop();
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


