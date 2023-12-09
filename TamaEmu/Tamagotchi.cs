// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Local

using TamaEmu.Processor;

#pragma warning disable CS0169 // Field is never used

public class Tamagotchi : Processor
{
    const int INT_IRQ = 1;
    const int INT_NMI = 2;
    const int FCPU = 16000000;
    const int R_BANK = 0x3000;
    const int R_CLKCTL = 0x3001;
    const int R_C32KCTL = 0x3002;
    const int R_WDTCTL = 0x3004;
    const int R_RESFL = 0x3005;
    const int R_SPUCTL = 0x3006;
    const int R_WAKEEN = 0x3007;
    const int R_WAKEFL = 0x3008;
    const int R_RESENA = 0x300B;
    const int R_RESCTL = 0x300C;
    const int R_PACFG = 0x3010;
    const int R_PADIR = 0x3011;
    const int R_PADATA = 0x3012;
    const int R_PASTR = 0x3013;
    const int R_PBCFG = 0x3014;
    const int R_PBDIR = 0x3015;
    const int R_PBDATA = 0x3016;
    const int R_PCCFG = 0x3017;
    const int R_PCDIR = 0x3018;
    const int R_PCDATA = 0x3019;
    const int R_TIMBASE = 0x3030;
    const int R_TIMCTL = 0x3031;
    const int R_TM0LO = 0x3032;
    const int R_TM0HI = 0x3033;
    const int R_TM1LO = 0x3034;
    const int R_TM1HI = 0x3035;
    const int R_CARCTL = 0x303C;
    const int R_KEYSCTL = 0x303D;
    const int R_KEYSP1 = 0x303E;
    const int R_KEYSP2 = 0x303F;
    const int R_LCDS1 = 0x3040;
    const int R_LCDS2 = 0x3041;
    const int R_LCDC1 = 0x3042;
    const int R_LCDC2 = 0x3043;
    const int R_LCDSEG = 0x3044;
    const int R_LCDCOM = 0x3045;
    const int R_LCDFRMCTL = 0x3046;
    const int R_LCDBUFCTL = 0x3047;
    const int R_VLCDCTL = 0x3048;
    const int R_PUMPCTL = 0x3049;
    const int R_BIASCTL = 0x304A;
    const int R_MVCL = 0x3051;
    const int R_MIXR = 0x3052;
    const int R_MIXL = 0x3053;
    const int R_SPUEN = 0x3054;
    const int R_SPUINTSTS = 0x3055;
    const int R_SPUINTEN = 0x3056;
    const int R_CONC = 0x3057;
    const int R_MULA = 0x3058;
    const int R_MULB = 0x3059;
    const int R_MULOUTH = 0x305A;
    const int R_MULOUTL = 0x305B;
    const int R_MULACT = 0x305C;
    const int R_AUTOMUTE = 0x305E;
    const int R_DAC1H = 0x3060;
    const int R_DAC1L = 0x3062;
    const int R_SOFTCTL = 0x3064;
    const int R_DACPWM = 0x3065;
    const int R_INTCTLLO = 0x3070;
    const int R_INTCTLMI = 0x3071;
    const int R_INTCLRLO = 0x3073;
    const int R_INTCLRMI = 0x3074;
    const int R_NMICTL = 0x3076;
    const int R_LVCTL = 0x3077;
    const int R_DATAX = 0x3080;
    const int R_DATAY = 0x3081;
    const int R_DATA0XH = 0x3082;
    const int R_DATA0XL = 0x3083;
    const int R_DATA0YH = 0x3084;
    const int R_DATA0YL = 0x3085;
    const int R_DATAXH0 = 0x3086;
    const int R_DATAXL0 = 0x3087;
    const int R_DATAYH0 = 0x3088;
    const int R_DATAYL0 = 0x3089;
    const int R_DATAXLXH = 0x308A;
    const int R_DATAYLYH = 0x308B;
    const int R_DATAXLYH = 0x308C;
    const int R_DATAYLXH = 0x308D;
    const int R_IFFPCLR = 0x3090;
    const int R_IF8KCLR = 0x3093;
    const int R_IF2KCLR = 0x3094;
    const int R_IFTM0CLR = 0x3097;
    const int R_IFTBLCLR = 0x309A;
    const int R_IFTBHCLR = 0x309B;
    const int R_IFTM1CLR = 0x309D;
    const int R_SPICTL = 0x30B0;
    const int R_SPITXSTS = 0x30B1;
    const int R_SPITXCTL = 0x30B2;
    const int R_SPITXDAT = 0x30B3;
    const int R_SPIRXSTS = 0x30B4;
    const int R_SPIRXCTL = 0x30B5;
    const int R_SPIRXDAT = 0x30B6;
    const int R_SPIMISC = 0x30B7;
    const int R_SPIPORT = 0x30BA;
    const int IRQVECT_T0 = 0xFFC0;
    const int IRQVECT_FROSCD2K = 0xFFC6;
    const int IRQVECT_FROSCD8K = 0xFFC8;
    const int IRQVECT_SPU = 0xFFCA;
    const int IRQVECT_SPI = 0xFFCC;
    const int IRQVECT_FP = 0xFFCE;
    const int IRQVECT_T1 = 0xFFD4;
    const int IRQVECT_TBH = 0xFFD8;
    const int IRQVECT_TBL = 0xFFDA;
    const int IRQVECT_NMI = 0xFFFA;


    public class TamaClk
    {
        public int tblDiv, tblCtr;
        public int tbhDiv, tbhCtr;
        public int c8kCtr, c2kCtr;
        public int t0Div, t0Ctr;
        public int t1Div, t1Ctr;
        public int fpCtr;
        public int cpuDiv, cpuCtr;
    }

    public class TamaHw
    {
        public byte bankSel;
        public byte portAdata;
        public byte portALastRead;
        public byte portBdata;
        public byte portCdata;
        public byte portAout;
        public byte portBout;
        public byte portCout;
        public int ticks;
        public short iflags;
        public byte nmiflags;
        public int remCpuCycles;
        public int lastInt;
    }

    public class TamaLcd
    {
        public int sizex;
        public int sizey;
    }
    
    byte[][] roms;

    I2cBus i2cbus;
    i2ceeprom i2ceeprom;
    
    byte[] ram = new byte[1536];
    public byte[] dram = new byte[512];
    byte[] ioreg = new byte[255];
    TamaHw hw;
    TamaLcd lcd;
    TamaClk clk;
    byte btnPressed;
    int btnReleaseTm;
    int btnReads;
    int irnx;


    int PAGECT = 22;

    int BUTTONRELEASETIME = FCPU / 15;

    private const int REGOffset = 0x3000;
    int REG(int x) => ioreg[x - 0x3000];

    public Tamagotchi(string romdir, string eeprom)
    {
        roms = LoadRoms(romdir);
        i2cbus = new I2cBus();
        i2ceeprom = new i2ceeprom(eeprom);
        
        i2cbus.i2cAddDev(i2ceeprom, 0xA0);
        
        // tama->cpu->Rd6502=tamaReadCb;
        // tama->cpu->Wr6502=tamaWriteCb;
        // tama->cpu->User=(void*)tama;

        hw = new TamaHw();
        hw.bankSel=0;
        hw.portAdata=0xf; //7-IR recv, 3-batlo 2-but2 1-but1 0-but0
        hw.portBdata=0xfe;	//0, 1: I2C; 3: IR send,
        hw.portCdata=0xff;
        // Reset6502(tama->cpu);
        ioreg[R_CLKCTL-0x3000]=0x2; //Fosc/8 is default
        irnx=0;
        tamaClkRecalc();
    }


    void tamaDumpHw()
    {
        string[] intfdesc =
        {
            "FP", "SPI", "SPU", "FOSC/8K", "FOSC/2K", "x", "x", "TM0",
            "EX", "x", "TBL", "TBH", "x", "TM1", "x", "x"
        };

        string[] nmidesc = { "LV", "TM1", "x", "x", "x", "x", "x", "NMIEN" };
        string[] tbldiv = { "2HZ", "8HZ", "4HZ", "16HZ" };
        string[] tbhdiv = { "128HZ", "512HZ", "256HZ", "1KHZ" };
        string[] t0diva = { "VSS", "Rosc", "32KHz", "ECLK", "VDD", "x", "x", "x" };
        string[] t0divb = { "VDD", "TBL", "TBH", "EXTI", "2HZ", "8HZ", "32HZ", "64HZ" };
        string[] t1div = { "VSS", "Rosc", "32KHz", "TIMER0" };
        string[] ccdiv = { "/2", "/4", "/8", "/16", "/32", "/64", "/128", "OFF" };

        int i;
        int ien = ioreg[0x70] + (ioreg[0x71] << 8);


        Console.WriteLine($"Ints enabled: (0x%X)", ien);
        for (i = 0; i < 16; i++)
        {
            if ((ien & (1 << i)) != 0)
                Console.WriteLine($"{intfdesc[i]} ");
        }

        Console.WriteLine($"\nInt flags: ");
        for (i = 0; i < 16; i++)
        {
            if ((hw.iflags & (1 << i)) != 0) Console.WriteLine($"{intfdesc[i]}");
        }

        Console.WriteLine($"\nLast active int: ");
        for (i = 0; i < 16; i++)
        {
            if ((hw.lastInt & (1 << i)) != 0) Console.WriteLine($"%s ", intfdesc[i]);
        }

        Console.WriteLine($"\nNMI ena:");
        for (i = 0; i < 8; i++)
        {
            if ((REG(R_NMICTL) & (1 << i)) != 0) Console.WriteLine($"{nmidesc[i]} ");
        }

        Console.WriteLine($"\n");
        Console.WriteLine($"Timebase: tbl: {tbldiv[((REG(R_TIMBASE) >> 2) & 3)]}");
        Console.WriteLine($"tbh {tbhdiv[((REG(R_TIMBASE) >> 0) & 3)]}, ");
        Console.WriteLine($"t0A {t0diva[((REG(R_TIMCTL) >> 5) & 7)]}, ");
        Console.WriteLine($"t0B {t0divb[((REG(R_TIMCTL) >> 2) & 7)]}, ");
        Console.WriteLine($"T1 {t1div[((REG(R_TIMCTL) >> 0) & 3)]}, ");
        Console.WriteLine($"CPU {ccdiv[REG(R_CLKCTL) & 7]}\n");

        Console.WriteLine($"Prescalers: tbl: {clk.tblCtr:D7}/" +
                          $"{clk.tblDiv:D7}, tbh: {clk.tbhCtr:D5}/" +
                          $"{clk.tbhDiv:D5}, c8k: {clk.c8kCtr:D4}, c2k " +
                          $"{clk.c2kCtr:D4}, t0: {clk.t0Ctr:D4}/" +
                          $"{clk.t0Div:D4}, t1: {clk.t1Ctr:D4}/" +
                          $"{clk.t1Div:D4}, cpu: {clk.cpuCtr:D4}/" +
                          $"{clk.cpuDiv:D4}");

        Console.WriteLine("Btn port reads since last press: {0}\n", btnReads);
        Console.WriteLine("Current bank: {0}\n", hw.bankSel);
        Console.WriteLine($"Output bits: A:");
        tamaDumpBin(hw.portAout, 8);
        Console.WriteLine($" B:");
        tamaDumpBin(hw.portBout, 8);
        Console.WriteLine($" C:");
        tamaDumpBin(hw.portCout, 8);
        Console.WriteLine($"\n");
    }

    void tamaDumpBin(int val, int bits)
    {
        int x;
        for (x = bits - 1; x >= 0; --x)
        {
            Console.Write(((val & (1 << x)) != 0) ? 1 : 0);
            Console.WriteLine();
        }
    }


    private const int PageSize = 32 * 1024; // Assuming each ROM page is 32 KB

    public byte[][] LoadRoms(string dir)
    {
        roms = new byte[PAGECT][];
        int noLoaded = 0;

        for (int i = 0; i < PAGECT; i++)
        {
            string fname = Path.Combine(dir, $"p{i}.bin");
            FileStream f = null;

            try
            {
                f = File.OpenRead(fname);
            }
            catch (FileNotFoundException)
            {
                fname = Path.Combine(dir, $"p{i}");
                try
                {
                    f = File.OpenRead(fname);
                }
                catch (FileNotFoundException)
                {
                    Console.Error.WriteLine($"Error: File not found - {fname}");
                    // Newer Tamas have empty pages. Don't exit if one is missing.
                    // Since we're in C#, we won't exit the program; instead we'll continue with the loop.
                    continue;
                }
            }

            // We found a valid file stream.
            roms[i] = new byte[PageSize];
            using (BinaryReader br = new BinaryReader(f))
            {
                if (f.Length > PageSize)
                {
                    // Probably a dump of the entire 6502 address space. Seek to the start of the page.
                    f.Seek(0x4000, SeekOrigin.Begin);
                }

                // Read only PageSize even if the file is larger.
                int bytesRead = br.Read(roms[i], 0, PageSize);
                Console.WriteLine($"ROM loaded: {fname} - {bytesRead} bytes");
                // Example to show the read values (uncomment if needed):
                //Console.WriteLine($"{roms[i][0x3ffc]:X2} {roms[i][0x3ffd]:X2}");
                noLoaded++;
            }
        }

        if (noLoaded < 2)
        {
            Console.WriteLine("Couldn't load ROM pages! Bailing out.");
            Environment.Exit(1); // Equivalent to exit(1) in C, but use with caution in C#
        }

        return roms;
    }


    void tamaClkRecalc()
    {
        int[] tbldiv = { FCPU / 2, FCPU / 8, FCPU / 4, FCPU / 16 };
        int[] tbhdiv = { FCPU / 128, FCPU / 512, FCPU / 256, FCPU / 1000 };
        int[] t0diva = { 0, 1, FCPU / 32767, 1, 0, 0, 0, 0 };
        int[] t0divb = { 0, 0, 0, 0, FCPU / 2, FCPU / 8, FCPU / 32, FCPU / 64 };
        int[] t1div = { 0, 1, 1, 0 }; //HACK! Real table seems to increase T1 too slowly.
        int[] ccdiv = { 2, 4, 8, 16, 32, 64, 128, 0 };

        clk.tblDiv = tbldiv[((REG(R_TIMBASE) >> 2) & 3)];
        clk.tbhDiv = tbhdiv[((REG(R_TIMBASE) >> 0) & 3)];
        clk.t0Div = t0diva[((REG(R_TIMCTL) >> 5) & 7)];
        if (clk.t0Div == 0)
        {
            clk.t0Div = t0divb[((REG(R_TIMCTL) >> 2) & 7)];
        }

        clk.t1Div = t1div[((REG(R_TIMCTL) >> 0) & 3)];
        clk.cpuDiv = ccdiv[REG(R_CLKCTL) & 7];
    }

    public bool Trace { get; set; }

    //feed R_WAKEFL value
    void tamaWakeSrc(byte src)
    {
        ioreg[R_WAKEFL - REGOffset] |= src;
        if (((REG(R_CLKCTL) & 7) == 7) && ((REG(R_WAKEEN)) & src) != 0)
        {
            ioreg[R_CLKCTL - REGOffset] = (byte)((REG(R_CLKCTL) & 0xf8) | 2);
            clk.cpuDiv = 8;
        }
    }

    void tamaToggleBtn(byte btn)
    {
        hw.portAdata ^= (byte)(1 << (btn));
        tamaWakeSrc((1 << 0));
    }

    //0, 1, 2
    public void tamaPressBtn(byte btn)
    {
        if (btnReleaseTm != 0) return;
        tamaToggleBtn(btn);
        btnPressed = btn;
        btnReleaseTm = BUTTONRELEASETIME;
        btnReads = 0;
    }

    //1 - fully implemented
    //2 - ToDo
    //3 - SPU
    static byte[] implemented =
    {
        //	0 1 2 3 4 5 6 7 8 9 A B C D E F
        1, 1, 0, 0, 1, 0, 3, 1, 1, 0, 0, 0, 0, 0, 0, 0, //00
        0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, //10
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //20
        1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //30
        0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 3, 0, 0, 0, 0, 0, //40
        0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 3, 0, //50
        1, 0, 3, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //60
        1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, //70
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //80
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //90
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //A0
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //B0
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //C0
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //D0
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //E0
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, //F0
    };

    private bool bkUnk;

    public void tamaToggleBkunk()
    {
        bkUnk = !bkUnk;
    }

    byte ioRead(int addr)
    {
        if (addr == R_PADATA)
        {
//		Console.WriteLine("PA: %X\n", hw.portAdata);
//		if (bkUnk) cpu.Trace=1;
            btnReads++;
            hw.portALastRead = hw.portAdata;
            return hw.portAdata;
        }
        else if (addr == R_PBDATA)
        {
            return hw.portBdata;
        }
        else if (addr == R_PCDATA)
        {
//		cpu.Trace=1;
            return hw.portCdata;
        }
        else if (addr == R_INTCTLLO)
        {
            return (byte)(hw.iflags & 0xff);
        }
        else if (addr == R_INTCTLMI)
        {
            return (byte)(hw.iflags >> 8);
        }
        else if (addr == R_NMICTL)
        {
            return (byte)((ioreg[R_NMICTL - 0x3000] & 0x80) | hw.nmiflags);
        }
        else if (addr == R_LVCTL)
        {
            return (byte)(ioreg[R_LVCTL - 0x3000] & 0x83); //battery is always full
        }
        else if (addr == 0x3055)
        {
            Trace = true;
            Console.WriteLine($"eek unimplemented ioRd 0x{addr:X4}");
        }
        else
        {
            if (implemented[addr & 0xff] != 1 && bkUnk)
            {
                if (!Trace)
                    Console.WriteLine($"Unimplemented ioRd 0x{addr:X4}\n");
                Trace = true;
            }

            if (implemented[addr & 0xff] == 3)
            {
                if (!Trace) Console.WriteLine($"Unimplemented ioRd 0x{addr:X4}");
                Trace = true;
            }

            return ioreg[addr - 0x3000];
        }

        return 0; //unimplemented
    }


    void ioWrite(int addr, byte val)
    {
        if (addr == R_BANK)
        {
            if (val > 20)
            {
                Console.WriteLine("Unimplemented bank: 0x{0:X2}\n", val);
            }
            else
            {
//			Console.WriteLine("Bank switch %d\n", val);
                hw.bankSel = val;
            }
        }
        else if (addr == R_PADATA)
        {
            //0 - Button 1
            //1 - Button 2
            //2 - Button 3
            //3 - LV detect
            hw.portAout = val;
            //4-7 - SPI for Tamago
        }
        else if (addr == R_PBDATA)
        {
            hw.portBout = val;
            //Port B:
            //0 - I2C SDA
            //1 - I2C SCL
            //2 - IR enable?
            //3 - IR LED
            //4-7 - SPI for Tamago

            // irActive(val & 0x8);
            // hw.portBdata &= ~1;
            // if (i2cHandle(i2cbus, val & 2, val & 1) && (val & 1)) hw.portBdata |= 1;
        }
        else if (addr == R_PCDATA)
        {
            //Probably unused.
            hw.portCout = val;
        }
        else if (addr == R_INTCLRLO)
        {
            short msk = (short)(0xffff ^ (val));
            hw.iflags &= msk;
        }
        else if (addr == R_INTCLRMI)
        {
            short msk = (short)(0xffff ^ (val << 8));
            hw.iflags &= msk;
        }
        else if (addr == R_IFFPCLR)
        {
            hw.iflags &= ~(1 << 0);
        }
        else if (addr == R_IF8KCLR)
        {
            hw.iflags &= ~(1 << 3);
        }
        else if (addr == R_IF2KCLR)
        {
            hw.iflags &= ~(1 << 4);
        }
        else if (addr == R_IFTM0CLR)
        {
            hw.iflags &= ~(1 << 7);
        }
        else if (addr == R_IFTBLCLR)
        {
            hw.iflags &= ~(1 << 10);
        }
        else if (addr == R_IFTBHCLR)
        {
            hw.iflags &= ~(1 << 11);
        }
        else if (addr == R_IFTM1CLR)
        {
            hw.iflags &= ~(1 << 13);
        }
        else if (addr == R_LCDSEG)
        {
            lcd.sizex = (val + 1) * 8;
        }
        else if (addr == R_LCDCOM)
        {
            lcd.sizey = (val + 1);
        }
        else if (addr == R_NMICTL)
        {
            ioreg[addr - 0x3000] = val;
            hw.nmiflags &= val;
        }
        else if (addr == R_TIMBASE || addr == R_TIMCTL || addr == R_CLKCTL)
        {
            ioreg[addr - 0x3000] = val;
            tamaClkRecalc();
            if (clk.cpuDiv == 0 && (REG(R_WAKEFL) == 1 & REG(R_WAKEEN) == 1))
            {
                //Wake up immediately
                ioreg[R_CLKCTL - REGOffset] = (byte)((REG(R_CLKCTL) & 0xf8) | 2);
                clk.cpuDiv = 8;
            }
        }
        else if (addr == R_WAKEFL)
        {
            //Make sure the write _clears_ the flag
            val = (byte)(ioreg[R_WAKEFL - 0x3000] & (~(val)));
        }
        else if (addr >= 0x3080 && addr < 0x3090)
        {
            Console.WriteLine("Data\n");
            Trace = true;
//	} else if (addr==0x3055) {
//		cpu.Trace=1;
//		Console.WriteLine("wuctl unimplemented ioWr 0x%04X 0x%02X\n", addr, val);
        }
        else
        {
            if (implemented[addr & 0xff] != 1 && bkUnk)
            {
                Console.WriteLine("unimplemented ioWr 0x%04X 0x%02X\n", addr, val);
                Trace = true;
            }
        }

        ioreg[addr - 0x3000] = val;
    }

    int tamaHwTick(int gran)
    {
        int t0Tick = 0, t1Tick = 0;
        byte nmiTrigger = 0;
        int ien;

        //Do IR ticks
        // if (irTick(gran, &irnx)) hw.portAdata &= ~0xf0;
        // else 

        hw.portAdata |= 0xf0;
        if (irnx != 0)
        {
            irnx -= gran;
            if (irnx < 0) irnx = 0;
            return gran;
        }

        clk.tblCtr += gran;
        clk.tbhCtr += gran;
        clk.c8kCtr += gran;
        clk.c2kCtr += gran;
        clk.fpCtr += gran;
        if (clk.cpuDiv != 0) clk.cpuCtr += gran;
        if (clk.t0Div != 0) clk.t0Ctr += gran;
        if (clk.t1Div != 0) clk.t1Ctr += gran;

        while (clk.tblCtr >= clk.tblDiv)
        {
            clk.tblCtr -= clk.tblDiv;
            hw.iflags |= (1 << 10);
            if (((REG(R_TIMCTL) >> 2) & 7) == 1) t0Tick++;
            tamaWakeSrc((1 << 1));
        }

        while (clk.tbhCtr >= clk.tbhDiv)
        {
            clk.tbhCtr -= clk.tbhDiv;
            hw.iflags |= (1 << 11);
            if (((REG(R_TIMCTL) >> 2) & 7) == 2) t0Tick++;
            tamaWakeSrc((1 << 3));
        }

        if (clk.c2kCtr >= 2048)
        {
            clk.c2kCtr &= 2047;
            hw.iflags |= (1 << 4);
        }

        if (clk.c8kCtr >= 8192)
        {
            clk.c8kCtr &= 8191;
            hw.iflags |= (1 << 3);
        }

        if (clk.cpuDiv != 0 && (clk.cpuCtr >= clk.cpuDiv))
        {
            hw.remCpuCycles += clk.cpuCtr / clk.cpuDiv;
            clk.cpuCtr = clk.cpuCtr % clk.cpuDiv;
        }

        while (clk.t0Div != 0 && clk.t0Ctr >= clk.t0Div)
        {
            clk.t0Ctr -= clk.t0Div;
            t0Tick++;
        }

        if (clk.fpCtr >= (FCPU / 60))
        {
            hw.iflags |= (1 << 0);
        }

        while (t0Tick > 0)
        {
            ioreg[R_TM0LO - REGOffset]++;
            if (ioreg[R_TM0LO - REGOffset] == 0)
            {
                ioreg[R_TM0HI - REGOffset]++;
                if (REG(R_TM0HI) == 0)
                {
                    hw.iflags |= (1 << 7);
                    if (((REG(R_TIMCTL)) & 3) == 3) t1Tick++;
                    tamaWakeSrc((1 << 2));
                }
            }

            t0Tick--;
        }

        while (clk.t1Div != 0 && clk.t1Ctr >= clk.t1Div)
        {
            clk.t1Ctr -= clk.t1Div;
            t1Tick++;
        }

        while (t1Tick > 0)
        {
            ioreg[R_TM1LO - REGOffset]++;

            if (REG(R_TM1LO) == 0)
            {
                ioreg[R_TM1HI - REGOffset]++;

                if (REG(R_TM1HI) == 0)
                {
                    hw.iflags |= (1 << 13);
                    nmiTrigger |= (1 << 1);
                    tamaWakeSrc((1 << 4));
                }
            }

            t1Tick--;
        }


        //Fire interrupts if enabled
        ien = REG(R_INTCTLLO) | (REG(R_INTCTLMI) << 8);
//	if (ien&hw.iflags) printf("Firing int because of iflags 0x%X\n", (ien&hw.iflags));
        if (((ien & hw.iflags) & (1 << 0)) != 0)
            Int6502( INT_IRQ, IRQVECT_FP);
        if (((ien & hw.iflags) & (1 << 1)) != 0)
            Int6502( INT_IRQ, IRQVECT_SPI);
        if (((ien & hw.iflags) & (1 << 2)) != 0)
            Int6502( INT_IRQ, IRQVECT_SPU);
        if (((ien & hw.iflags) & (1 << 3)) != 0)
            Int6502( INT_IRQ, IRQVECT_FROSCD8K);
        if (((ien & hw.iflags) & (1 << 4)) != 0)
            Int6502( INT_IRQ, IRQVECT_FROSCD2K);
        if (((ien & hw.iflags) & (1 << 7)) != 0)
            Int6502(INT_IRQ, IRQVECT_T0);
        if (((ien & hw.iflags) & (1 << 10)) != 0)
            Int6502( INT_IRQ, IRQVECT_TBL); //seems to be for animation, 2Hz
        if (((ien & hw.iflags) & (1 << 11)) != 0)
            Int6502( INT_IRQ, IRQVECT_TBH);
        if (((ien & hw.iflags) & (1 << 13)) != 0)
            Int6502( INT_IRQ, IRQVECT_T1);
        //debug: remember last irq somewhere
        if ((ien & hw.iflags) != 0) hw.lastInt = (ien & hw.iflags);

        //Fire NMI
        if ((REG(R_NMICTL) & 0x80) != 0)
        {
            //Should be edge triggered. The timer is. An implementation of lv may not be.
            hw.nmiflags |= nmiTrigger;
            if ((REG(R_NMICTL) & nmiTrigger) != 0)
            {
                Int6502( INT_NMI, 0);
            }
        }

        //Handle stupid hackish button release...
        if (btnReleaseTm > 0)
        {
            btnReleaseTm -= gran;
            if (btnReleaseTm <= 0)
            {
                btnReleaseTm = 0;
                tamaToggleBtn(btnPressed);
                Console.WriteLine("Release btn {0:D}\n", btnPressed);
            }
        }

        if (hw.portALastRead != hw.portAdata)
        {
            tamaWakeSrc(1 << 0);
        }

        //Now would be a good time to run the cpu, if needed.
        if (hw.remCpuCycles > 0)
        {
            hw.remCpuCycles = Exec6502( hw.remCpuCycles);
        }

        return gran;
    }

    private int Exec6502( int hwRemCpuCycles)
    {
        throw new NotImplementedException();
    }

    private void Int6502( int intIrq, int irqvectFp)
    {
        throw new NotImplementedException();
    }

    public override byte ReadMemoryValue(int addr)
    {
        byte r=0xff;
        switch (addr)
        {
            case < 0x600:
                r= ram[addr];
                break;
            case >= 0x1000 and < 0x1200:
                r= dram[addr-0x1000];
                break;
            case >= 0x3000 and < 0x4000:
                r=ioRead((short)addr);
                break;
            case >= 0x4000 and < 0xc000:
                r=roms[hw.bankSel][addr-0x4000];
                break;
            case >= 0xc000:
                r=roms[0][addr-0xc000];
                break;
            default:
                Console.WriteLine("emu: invalid read: addr 0x{0:X4}\n", addr);
                Trace= true;
                break;
        }
//	printf("Rd 0x%04X 0x%02X\n", addr, r);
        return r;
}

    public override void WriteMemoryValue(int addr, byte val)
    {
        if (addr<0x600) {
            ram[addr]=val;
        } else if (addr>=0x1000 &&  addr<0x1200) {
            dram[addr-0x1000]=val;
        } else if (addr>=0x3000 &&  addr<0x4000) {
            ioWrite( addr, val);
        } else {
            Console.WriteLine("emu: invalid write: addr 0x{0:X4} val 0x{1:X2}\n", addr, val);
            Trace=true;
        }
    }
 
}