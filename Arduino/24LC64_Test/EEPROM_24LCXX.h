/*  
  EEPROM_24LCXX - Classe gestione EEPROM I2C Microchip serie 24LCXX 
  Copyright (C) 2022 DrVector
*/

#ifndef EEPROM_24LCXX_h
#define EEPROM_24LCXX_h

#include "Arduino.h"

class EEPROM_24LCXX {
  private:
    // Indirizzo del chip
    uint8_t _address;
    // Densità memoria in Kbit
    uint8_t _density;

    void (*dataReadCallback)(byte data);

  public:
    // Costruttore
    EEPROM_24LCXX(uint8_t address, uint8_t density);

    void onDataRead(void (*cb)(byte data));

    void ReadAll();
    byte ReadByte(uint16_t dataAddress);
    void WriteByte(uint16_t dataAddress, byte data);
    void WriteBuffer(uint16_t dataAddress, byte data[], uint16_t size);
};

#endif