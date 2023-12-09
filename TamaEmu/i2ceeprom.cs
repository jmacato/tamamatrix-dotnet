#pragma warning disable CS0169
public class i2ceeprom : I2cDev
{
    public i2ceeprom(string filename)
    {
        if (!File.Exists(filename))
        {
            var mem = new byte[ushort.MaxValue];
            Array.Fill(mem, byte.MaxValue);
            File.WriteAllBytes(filename, mem);
        }
        
        backing = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
        backing.Seek(0, SeekOrigin.Begin);
    } 
    
    int adr;

    private readonly FileStream backing;

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
            
            backing.Seek(adr, SeekOrigin.Begin);
            backing.WriteByte(val);

//		printf("I2CEEprom write: (%d) addr %02X val %02X\n", byteNo-2, adr, byte);
            adr++;
            //Simulate in-page rollover
            adr = page | (adr & 0x1F);
        }

        return 1;
    }

    public override byte readCb(byte byteNo)
    {
        byte r;
        
        backing.Seek(adr, SeekOrigin.Begin);
        r = (byte)backing.ReadByte();
        
//	printf("I2cEEprom read: addr %02x val %02x\n", adr, r);
        adr++;
        adr &= 0xffff;
        return r;
    }
 
}