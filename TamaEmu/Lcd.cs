public class Lcd
{
    private Display display = new Display();
    public const short ICON_INFO = (1 << 0);
    public const short ICON_FOOD = (1 << 1);
    public const short ICON_TOILET = (1 << 2);
    public const short ICON_DOORS = (1 << 3);
    public const short ICON_FIGURE = (1 << 4);
    public const short ICON_TRAINING = (1 << 5);
    public const short ICON_MEDICAL = (1 << 6);
    public const short ICON_IR = (1 << 7);
    public const short ICON_ALBUM = (1 << 8);
    public const short ICON_ATTENTION = (1 << 9);

    //tama lcd is 48x32
    public void Render(byte[] ram, int sx = 32, int sy = 48)
    {
        int x, y;
        int b, p;
        for (y = 0; y < sy + 1; y++)
        {
            for (x = 0; x < sx; x++)
            {
                if (y >= 16)
                {
                    p = x + (sy - y - 1) * sx;
                }
                else
                {
                    p = x + (sy - (15 - y) - 1) * sx;
                }

                b = ram[p / 4];
                b = (b >> ((3 - (p & 3)) * 2)) & 3;
                if (y < 32 && x < 48) display.p[y, x] = b;
            }
        }

        y = 1;
        display.icons = 0;
        for (x = 19; x < 29; x++)
        {
            b = ram[x / 4];
            b = (b >> ((3 - (x & 3)) * 2)) & 3;
            if (b != 0) display.icons |= y;
            y <<= 1;
        }
    }

    public class Display
    {
        public int[,] p = new int[32, 48]; //[32][48];
        public int icons;
    };


    public void Show()
    {
        int i;
        int x, y;
        string[] icons =
        {
            "INFO", "FOOD", "TOILET", "DOORS", "FIGURE",
            "TRAINING", "MEDICAL", "IR", "ALBUM", "ATTENTION"
        };
        string[] grays = { "█", "▓", "░", " " };
        Console.WriteLine("\x33[45;1H\x33[1J\x33[1;1H");

        for (y = 0; y < 32; y++)
        {
            for (x = 0; x < 48; x++)
            {
                Console.Write("{0}{1}", grays[display.p[y, x] & 3], grays[display.p[y, x] & 3]);
            }

            Console.Write('\n');
        }

        Console.Write(">>> ");
        i = display.icons;
        for (x = 0; x < 10; x++)
        {
            if ((i & 1) == 1) Console.Write("{0} ", icons[x]);
            i >>= 1;
        }

        Console.Write("<<<\n");
    }
}