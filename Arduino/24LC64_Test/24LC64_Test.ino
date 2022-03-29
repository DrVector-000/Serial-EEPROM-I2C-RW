/*  
  24LC64_Test.ino - Test di lettura e scrittura serial I2C EEPROM Microchip 24LC64
  Copyright (C) 2022 DrVector
*/

#include <Wire.h>
#include "EEPROM_24LCXX.h"

//******************************************************************************************************************//
//* Costanti
//******************************************************************************************************************//
#define CTR_CODE 0b1010
#define SLV_ADDR 0b000

//******************************************************************************************************************//
//* Variabili globali
//******************************************************************************************************************//
bool serialEcho = false;
EEPROM_24LCXX eeprom = EEPROM_24LCXX((CTR_CODE << 3) + SLV_ADDR, 64);

//******************************************************************************************************************//
//* Handler evento OnDataRead
//******************************************************************************************************************//
char buf[2];
void OnDataReadEvent(byte data) {
    //sprintf(buf, "%02x", data);
    //Serial.print(String(buf) + " ");
    Serial.write(data);
}

//******************************************************************************************************************//
//* Setup
//******************************************************************************************************************//
void setup() {
  // Inizializza comunicazione seriale
  Serial.begin(115200);

  // Inizializza libreria Wire
  eeprom.onDataRead(OnDataReadEvent);
}

//******************************************************************************************************************//
//* Ciclo principale
//******************************************************************************************************************//
void loop() {
  // Lettura comandi in ingresso dalla porta seriale
  String s = ReadSerialComand();

  // Parsing dei comandi
  ParseComands(s);
}

//******************************************************************************************************************//
//* Parsing dei comandi
//******************************************************************************************************************//
void ParseComands(String s) {
  String params[10];

  s.toUpperCase();
  if (s != "") {
    if (serialEcho) {
      Serial.println(s);
    }

    String comand = GetComand(s);
    // Serial.println("COMAND: " + comand);
    //**********************************************
    // ECHO
    //**********************************************
    if (comand == "ECHO") {
      GetComandParams(s, params);
      // Serial.println("PARAM: " + params[0]);
      if (params[0] == "1") {
        serialEcho = true;
      }
      else if (params[0] == "0") {
        serialEcho = false;
      }
      else if (params[0] == "?") {
        Serial.println("+ECHO=" + String(serialEcho));
      }
    }
    //**********************************************
    // VERSION - Firmware version
    //**********************************************
    if (comand == "VERSION") {
      GetComandParams(s, params);
      // Serial.println("PARAM: " + params[0]);
      if (params[0] == "?") {
        Serial.println("+VERSION=1.00");
      }
    }
    //**********************************************
    // READALL - Read all
    //**********************************************
    if (comand == "READALL") {
      eeprom.ReadAll();
      Serial.println();
    }
    //**********************************************
    // READBYTE - Read Byte
    //**********************************************
    if (comand == "READBYTE") {
      GetComandParams(s, params);
      // Serial.println("PARAM: " + params[0]);
      if (params[0] != "") {
        eeprom.ReadByte(params[0].toInt());
        Serial.println();
      }
    }
    //**********************************************
    // WRITEBYTE - Write Byte
    //**********************************************
    if (comand == "WRITEBYTE") {
      GetComandParams(s, params);
      //Serial.println("PARAM: " + params[0] + ", " + params[1]);
      if ((params[0] != "") && (params[1] != "")) {
        eeprom.WriteByte(params[0].toInt(), params[1].toInt());
      }
    }
    //**********************************************
    // WRITEBUFFER - Write Buffer
    //**********************************************
    if (comand == "WRITEBUFFER") {
      GetComandParams(s, params);
      //Serial.println("PARAM: " + params[0] + ", " + params[1]);
      if ((params[0] != "") && (params[1] != "")) {
        int dataAddress = params[0].toInt();
        int size = params[1].toInt();
        byte data[size];
        int index = 0;
        for (;;) {
          if (Serial.available() > 0) {
            data[index] = Serial.read();

            index++;
            if (index == size) {
              break;
            }
          }
        }
        eeprom.WriteBuffer(dataAddress, data, size);
      }
    }
  }
}

// Ritorna la stringa di comando
String GetComand(String s) {
  String comand = "";

  int i = s.indexOf('=');
  return s.substring(0, i);
}

// Ritorna lista parametri
void GetComandParams(String s, String(&params)[10]) {
  int index = s.indexOf('=');
  String par = s;
  par.remove(0, index + 1);
  
  for (int i = 0; i < 10; i++) {
    index = par.indexOf(',');
    if (index == -1) {
      int l = par.length();
      if (l > 0) {
        params[i] = par;
      }
      return;
    }
    else {
      params[i] = par.substring(0, index);
      par.remove(0, index + 1);
    }
  }
}

//******************************************************************************************************************//
//* Lettura comandi in ingresso dalla porta seriale
//******************************************************************************************************************//
// Variabili globali
char receivedChars[64] = "";
int rcIndex = 0;

String ReadSerialComand(){
  while (Serial.available()) {
    if (rcIndex > 63) rcIndex = 0;
    
    // Legge carattere dalla seriale
    char rc = Serial.read();
    // Carattere di fine comando
    if (rc == '\n' or rc == '\r') {
      receivedChars[rcIndex] = '\0';
      rcIndex = 0;
      return receivedChars;
    }
    else {
      // Nuovo carattere
      receivedChars[rcIndex] = rc;
      rcIndex++;
    }
  }
  
  return "";
}
