#pragma warning disable CS0169
public class i2ceeprom : I2cDev
{
    public i2ceeprom(string filename)
    {
        mem = new byte[65536];
        Array.Fill(mem, byte.MaxValue);
        File.ReadAllBytes(filename).CopyTo(mem, 0);
    } 
    
    int adr;

    byte[] mem;

    public override byte writeCb(byte byteNo, byte val)
    {
        // I2cEeprom* e = (I2cEeprom*)dev;
        if (byteNo == 1)
        {
            adr = val << 8;
        }
        else if (byteNo == 2)
        {
            adr |= val;
        }
        else
        {
            int page = adr & 0xFFE0;
//		printf("I2CEEprom write: (%d) addr %02X val %02X\n", byteNo-2, adr, byte);
            mem[adr] = val;
            adr++;
            //Simulate in-page rollover
            adr = page | (adr & 0x1F);
        }

        return 1;
    }

    public override byte readCb(byte byteNo)
    {
        byte r;
        r = mem[adr];
//	printf("I2cEEprom read: addr %02x val %02x\n", adr, r);
        adr++;
        adr &= 0xffff;
        return r;
    }
 
}