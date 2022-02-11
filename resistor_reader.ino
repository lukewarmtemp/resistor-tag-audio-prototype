int analogPin = 0;
int raw = 0;
int Vin = 5;
float Vout = 0;
float R1 = 220;
float R2 = 0;
float buffer = 0;

void setup(){
  Serial.begin(9600);
}

void loop(){
  raw = analogRead(analogPin);
  if(raw){
    Serial.print("Vout: ");
    Serial.println(raw);
    delay(1000);
  }
}
