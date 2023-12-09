public abstract class I2cDev
{
    public abstract byte writeCb( byte byteNo, byte val);

    public abstract byte readCb(byte byteNo);
}