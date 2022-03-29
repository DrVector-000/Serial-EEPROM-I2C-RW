/*  
  EEPROM_24LCXX - Classe gestione EEPROM I2C Microchip serie 24LCXX
  Copyright (C) 2022 DrVector
*/

#include "EEPROM_24LCXX.h"
#include <Wire.h>

//******************************************************************************************************************//
//* Costruttore
//******************************************************************************************************************//
EEPROM_24LCXX::EEPROM_24LCXX(uint8_t address, uint8_t density) {
  _address = address;
  _density = density;

  // Inizializza libreria Wire
  Wire.begin();
}

//******************************************************************************************************************//
//* Imposta la funzione di callback
//******************************************************************************************************************//
void EEPROM_24LCXX::onDataRead(void (*cb)(byte data))
{
  dataReadCallback = cb;
}

//******************************************************************************************************************//
//* Legge tutta la ROM
//******************************************************************************************************************//
void EEPROM_24LCXX::ReadAll() {
  // Posizione indirizzo 0
  Wire.beginTransmission(_address);
  Wire.write(0);
  Wire.write(0);
  Wire.endTransmission();

  // Lettura blocchi di 16 Bytes
  for (int i = 1; i <= _density * 8; i++) {
    Wire.requestFrom((int)_address, 16);
    while (!Wire.available());

    for (int j = 1; j <= 16; j++) {
      byte data = Wire.read();
      if (dataReadCallback) {
        dataReadCallback(data);
      }
    }
  }  
}

//******************************************************************************************************************//
//* Legge un byte
//******************************************************************************************************************//
byte EEPROM_24LCXX::ReadByte(uint16_t dataAddress) {
  Wire.beginTransmission(_address);
  Wire.write((int)(dataAddress >> 8));
  Wire.write((int)(dataAddress & 0xFF));
  Wire.endTransmission();

  Wire.requestFrom((int)_address, 1);
  delay(1);
  byte data = 0;
  if (Wire.available()) {
    data = Wire.read();
    if (dataReadCallback) {
      dataReadCallback(data);
    }
  }
  return data;
}

//******************************************************************************************************************//
//* Scrive un byte
//******************************************************************************************************************//
void EEPROM_24LCXX::WriteByte(uint16_t dataAddress, byte data) {
  Wire.beginTransmission(_address);
  Wire.write((int)(dataAddress >> 8));
  Wire.write((int)(dataAddress & 0xFF));

  Wire.write(data);
  Wire.endTransmission();
}

//******************************************************************************************************************//
//* Scrive un array di byte (dimensione massima del buffer di pagina 32 byte)
//* La dimensione massima pare sia compresa dei 2 byte dell'indirizzo
//******************************************************************************************************************//
void EEPROM_24LCXX::WriteBuffer(uint16_t dataAddress, byte data[], uint16_t size) {
  if (sizeof(data) > 30) {
    return;
  }

  Wire.beginTransmission(_address);
  Wire.write((int)(dataAddress >> 8));
  Wire.write((int)(dataAddress & 0xFF));

  for (int i = 0; i < size; i++) {
    Wire.write(data[i]);
  }

  Wire.endTransmission();
}
