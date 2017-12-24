/*! \file */ 
#include <ZumoMotors.h>
#include <ZumoReflectanceSensorArray.h>
#include <Pushbutton.h>
#include <QTRSensors.h>

#define trigPin 2
#define echoPin 4
#define ledPin 13

Pushbutton button(ZUMO_BUTTON);
ZumoMotors motors;
ZumoReflectanceSensorArray reflectanceSensors;

//!This defines the sensor array
int sensorArr[6];

//!This is the speed the robot should run at 
int speed = 100;

//!this is the colour threshold for the sensors
int threshold = 300;

 //! This should be false until you want to override it to control the system manually
boolean overrideAutoRun = false;

//!this should be true until the program needs to auto solve the map
boolean runningMaze = true;

//!this is used to turn the corners
boolean pause = false; 

//!This is used to indicate if the system should return
boolean returning = false; 

//!This array is used as a list in order to work out the turns the arduino is taking
char path[100];

//!This is used to keep track of the actual length of the array above
int pathLength =0;

//!This is used to keep track of the rooms, a is left with item, d is right with item, c is left with no item, v is right with no item
int roomList[100];

//!This is used to keep track of the actual length of the array above
int roomNo = 0;

//!This is going to be storing dupes and extra info above to construct an idea of the actual corridor in order to allow for optomization. chars used are
//!r = right, l = left, w = wall, b = 180 degree turn (end of corridor), z = end of corridor, k = room on left, l= room on right
char returnList[200];

//!This indicates what indicies of the returnList array we're on
int returnLoc = 0;

//! this is used to pass stuff from the override controller to the room checker
bool isRoomPopped = false; 

void setup()
{
  Serial.begin(9600);
  connectToProgram();
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

//!!This controls the override of the system
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
       FinishedOverride = true;
       checkChar('n');
      break;
      case 'v'://This is the room case
      if (!doneVBefore){
        isRoomPopped = checkItem();
        doneVBefore = true;
        Serial.println("Please move the zumo back into the corridor, it has checked the room");
      }else {
        FinishedOverride = true;
        break;
      }
      break;
      default:
        break; 
    } 
  }
}


//TODO, found a way to do this, basically what i'm going to need to do is upon every wall or pause start a new corridor
//I get 48 of them by doing (int) to a char which is less than 48. Therefore i can eep a track of all of them and just search
//them for populated rooms. This will let me know where they start and end. Due to the number i can also keep track of
//the ones coming off them, i.e. if it includes a back i can know that it's an end corridor, while if i dont i know it's a connector
//Need to change data collection for this assumption
//This will also allow me to do the go back thing without a scanning of each direction i turn, because ones with back go back to the original corridor
//and if that corridor has no other turns it can make it means that it's done, so we know it can go back to the start of the corridor

void returnToCorridor(){
  
}

void optomizeRoute(){
  /*char finalRoute[100];
  int finalRouteLoc;
  char subCorridor[50];
  int subCorridorLoc;
  bool inSubCorridor = false;
  int[10] poppedRoomsList;
  int poppedRoomsLoc;
  for(int i = 0; i <= returnLoc ; i++){
    if (returnList[i] == 'z'){//!everytime we start a new sub corridor we want to set side corridor to true
      inSubCorridor = !inSubCorridor;
    }
    if (inSubCorridor){//!while we still are in the sub corridor add it to the sub corridor list
      subCorridor[subCorridorLoc] = returnList[i];
      subCorridorLoc += 1;
    }
    if (!inSubCorridor && subCorridorLoc > 0){//!once we're out of the sub corridor
      bool rooms = false;
      for(int f = 0; f <= subCorridorLoc; f++){//!check to see if we have any rooms which had items in them
        if (subCorridor[f] == 1){
          rooms = true;
          poppedRoomsList[poppedRoomsLoc] = f;
          poppedRoomsLoc += 1;
        }
      }
      //!r = right, l = left, w = wall, b = 180 degree turn (end of corridor), z = end of corridor, k = room on left, l= room on right
      if (rooms){//! if we do have rooms with items in them
        char[50] tempRoute = SubCorridor;
        for (int f = 0;f <=subCorridorLoc; f++){
          if (subCorridor){
            
          }
        }
      }
      subCorridor = new SubCorridor[50];
      subCorridorLoc = 0;
    }
  }*/
}

//!This should be the code to handle which direction we're going too after a pause
void runPause ( char val){
  stop();
     if (val == 'd'){//corridor right
       path[pathLength] = 'd';
       pathLength += 1;
       Serial.println("Please turn me  right"); 
            checkChar('o');     
     }else if (val == 'a'){//corridor left
       path[pathLength] = 'a';
       pathLength += 1;
       Serial.println("Please turn me left");
            checkChar('o');
     }else if (val =='b'){//roomLeft
      roomList[roomNo] = checkRoom('a');
      roomNo += 1;
      checkChar('n');
     }else if (val == 'm'){//roomRight
      roomList[roomNo] = checkRoom('d');
      roomNo += 1;
            checkChar('n');
     }else if (val == 'r'){
      Serial.println("|This functionality is not implemented, please return via override to the start of the corridor");
      returnToCorridor();
           ControllerOverride();
                 checkChar('n');
     }
}
//!This should check the room
char checkRoom(char leftRight){
  Serial.println("|Going to search  room No |");
  Serial.println(roomNo+1);
  if (leftRight == 'm'){
      Serial.println("| on the right after being moved into position|");
  }else {
    Serial.println("| on the left after being moved into position|");
  } 
  ControllerOverride();
  if (isRoomPopped){
    Serial.println("The room is populated, that's bad!");
    if (leftRight == 'a'){
      return 'a';//left with item
    }else {
      return 'd';//right with item
    }
  }else {
   Serial.println("The room is not populated"); 
   if (leftRight == 'a'){
    return 'c';//left no item
   }else {
    return 'v';//right no item
   }
  }
  checkChar('n');
  return;
}

//!This runs until the serial array is connected to via the c# program 
void connectToProgram(){
  while (true){
      char val = Serial.read();
      if (val == 'c'){
        Serial.println('d');
        delay(1000);
        return;
      }
  }
}

//!This should check the char input and then modify the booleans to control the flow if appropriate
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

//!This will calibrate the sensors by asking the user to place it facing the black line then putting it in the normal spot
void calibrate(){
  //inform the user that we want to start calibrating then wait for button
  delay(100);
    reflectanceSensors.init();
    //digitalWrite(ledPin, HIGH);
    Serial.println("Please place the zumo facing a black line in order to allow for calibration|"); 
    Serial.println("Then please press the button on the zumo once that is done");
    button.waitForButton();
    //Calibrate the sensors
    for (int i = 1; i < 7; i++){
      if (i % 2 == 0){
        motors.setLeftSpeed(-50);
        motors.setRightSpeed(-50);
      }else{
        motors.setLeftSpeed(50);
        motors.setRightSpeed(50);
      }   
      unsigned long currentMillis = millis() + 2000;
      while (millis() < currentMillis ){
        
              reflectanceSensors.calibrate();
      }
      stop();      
    } 
    stop();
    digitalWrite(ledPin, LOW);
    Serial.println("Please place the zumo in the correct starting position|");
    Serial.println("Press the button when ready to start");
    button.waitForButton();
    runningMaze = true;
    //Stop and wait to be put into the ready position
}

//!This should be the default run if we're using the system 
void runMaze(){
    for (byte i = 0; i < 6; i++)
      {
        reflectanceSensors.readCalibrated(sensorArr);
      }
      if ( sensorArr[0] > threshold && sensorArr[5] > threshold)//if we've encountered a wall
        {
          stop();
          Serial.println("w|");//as specified in the spec, we tell them the corridor no, which is pathlength +1 (as for human readability they wont care about the 0th indice)
          //!for human counting
          Serial.println(pathLength+1);
          checkChar('p');//This will pause the run
          delay(200);
          return;
        }
      else if (sensorArr[0] > threshold){//if we're running into a wall on the left
        adjustLeft();
      }else if (sensorArr[5] > threshold) {//if we're running into a wall on the right
        adjustRight();
      }
     w();
}
//!This should adjust the zumo left to stay within the walls
void adjustLeft(){
  motors.setLeftSpeed(speed*2);
}
//!This should adjust the zumo right to stay within the walls
void adjustRight(){
  motors.setRightSpeed(speed*2);
}
//!Moves the robot foward
void w (){
  motors.setLeftSpeed (speed);
  motors.setRightSpeed(speed);
}
//!Moves the robot left
void a(){
  motors.setLeftSpeed((speed*2)*-1);
  motors.setRightSpeed(speed*2);
}

//!moves the robot right
void d(){
  motors.setLeftSpeed(speed*2);
  motors.setRightSpeed((speed*2)*-1);
}
//!Moves the robot backwards  
void s(){
  motors.setLeftSpeed(-speed);
  motors.setRightSpeed(-speed);
}

void stop(){
  motors.setLeftSpeed(0);
  motors.setRightSpeed(0);
}

//!This should be a 90 degree turn (or as close as possible) for simplicities sake towards the right
void turnRight(){
  motors.setLeftSpeed(-200);
  motors.setRightSpeed(200);
  delay(400);
  stop();
}
//!This should be a 90 degree turn (or as close as possible) for simplicities sake towards the left
void turnLeft(){
  motors.setLeftSpeed(200);
  motors.setRightSpeed(-200);
  delay(400);
  stop();
}
//! returns true if there's an item within 10cm's
bool checkItem(){
  Serial.println("Checking the room");
  w();
  delay (200);
  stop();
  long duration, distance;
    pinMode(trigPin, OUTPUT);
    digitalWrite(trigPin, LOW);  
    delayMicroseconds(4);
    digitalWrite(trigPin, HIGH);
    delayMicroseconds(20); 
    digitalWrite(trigPin, LOW);
      pinMode(echoPin, INPUT);
    duration = pulseIn(echoPin, HIGH);
  distance = (duration/2) / 29.1;
  if (distance<10&& distance != 0){//if distance is within 10 cms and is not 0, as duration not picking up anything for a decent period will be 0 due to timeout
    return true;
  }
  return false;
}


