public class I2cBus
{
    public const int I2C_IDLE = 0;
    public const int I2C_B0 = 1;
    public const int I2C_B7 = 8;
    public const int I2C_ACK = 9;

    int state;
    byte val;
    int dirOut;
    int oldScl;
    int oldSda;
    int adr;
    byte byteCnt;
    int oldOut;
    private Dictionary<int, I2cDev> dev = new();

    public void i2cAddDev(I2cDev dev_in, int addr)
    {
        dev.Add(addr / 2, dev_in);
    }


    void i2cFree()
    {
        // free(b);
    }

    int i2cHandle(int scl, int sda)
    {
        int ret = oldOut;
//	printf("I2c state: %d\n", state);
        if (((oldScl & scl) == 1) && !((oldSda & sda) == 1))
        {
            //Stop condition.
            state = I2C_IDLE;
            ret = 1;
//		printf("I2C: stop\n");
        }
        else if ((oldScl & scl) == 1 && (oldSda == 1 && sda != 1))
        {
            //Start condition
            state = I2C_B0;
            byteCnt = 0;
            dirOut = 0;
//		printf("I2C: start\n");
            ret = 1;
        }
        else if (!(oldScl ==1) && (scl == 1) && oldSda == sda && state != I2C_IDLE)
        {
            ret = 1;
            //Clock up: bit is clocked in or out.
            if (dirOut == 1)
            {
                if (state != I2C_ACK)
                {
                    if (state == I2C_B0)
                    {
                        //Fetch new byte from dev
                        if (dev.ContainsKey(adr / 2))
                        {
                            val = dev[adr / 2].readCb(byteCnt);
                        }
                        else
                        {
                            //No such dev
                            val = 0xff;
                        }
                    }

                    if (!((val & 0x80) == 1)) ret = 0;
                    val <<= 1;
                    state++;
                }
                else
                {
                    //ToDo: read ack, send to dev
                    byteCnt++;
                    state = I2C_B0;
                }
            }
            else
            {
                if (state != I2C_ACK)
                {
                    //Receiving bit of byte
                    val <<= 1;
                    if (sda == 1) val |= 0x1;
                    state++;
                }
                else
                {
                    //Byte is in.
//				printf("I2C: got byte %d val 0x%02X\n", byteCnt, val);
                    if (byteCnt == 0)
                    {
                        //Address byte
                        adr = val;
                        if ((adr & 1) == 1) dirOut = 1;
                        if (dev.ContainsKey(adr / 2)) ret = 0;
                        else ret = 1; //Ack if dev is available.
                    }
                    else
                    {
                        //Send byte to dev, grab ack and send to host
                        if (dev.ContainsKey(adr / 2))
                        {
                            ret = (dev[adr / 2].writeCb(byteCnt, val)) == 1 ? 0 : 1;
                        }
                        else
                        {
                            ret = 1;
                        }
                    }

                    byteCnt++;
                    state = I2C_B0;
                }
            }
        }

        oldScl = scl;
        oldSda = sda;
        oldOut = ret;
        return ret;
    }
}