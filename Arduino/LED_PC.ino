
int message = 0;   //  This will hold one byte of the serial message
int LEDPin = 2;   //  This is the pin that the led is conected to


void setup()
{  
  Serial.begin(9600);  //set serial to 9600 baud rate
  pinMode(LEDPin, OUTPUT); //initialisation of pin 2
}

void loop(){
  if (Serial.available() > 0) //  Check to see if there is a new message
  { 
    message = Serial.read();    //  Put the serial input into the message
    
    if (message == 'A')  //  If a capitol A is received
    {
      digitalWrite(LEDPin, LOW);
    }
    
    if (message == 'a')  //  If a lowercase a is received
    { 
      digitalWrite(LEDPin, HIGH);
    }
    
    if (message == 'z')  //  If a lowercase z is received
    { 
      Serial.println("HELLO FROM ARDUINO");  // Send back Arduino is here
    }
  }
}

