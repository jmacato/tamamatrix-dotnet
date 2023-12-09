// See https://aka.ms/new-console-template for more information


using System.Diagnostics;

var FPS_TS = TimeSpan.FromSeconds(1.0 / 15.0);

byte[] rom;
var speedup = false;
var stopDisplay = false;
var aiEnabled = true;
var t = 0;
var eeprom = "tama.eep";
var host = "127.0.0.1";
var romdir = "rom";
// Display display;
var err = false;
// var k = 0;

// Tama tama;


var rand = new Random();

// Argument parsing
for (var i = 1; i < args.Length; i++)
{
    if (args[i] == "-h" && args.Length > i + 1)
    {
        host = args[++i];
    }
    else if (args[i] == "-e" && args.Length > i + 1)
    {
        eeprom = args[++i];
    }
    else if (args[i] == "-r" && args.Length > i + 1)
    {
        romdir = args[++i];
    }
    else if (args[i] == "-n")
    {
        aiEnabled = false;
    }
    else
    {
        Console.WriteLine($"Unrecognized option - {args[i]}");
        err = true;
        break;
    }
}

if (err)
{
    Console.WriteLine("Usage: TamaGame [options]");
    Console.WriteLine("-h host - change tamaserver host address (def 127.0.0.1)");
    Console.WriteLine("-e eeprom.eep - change eeprom file (def tama.eep)");
    Console.WriteLine("-r rom/ - change rom dir");
    Console.WriteLine("-n - disable AI");
    return;
}

// Signal handling would be different in C#, potentially using cancellation tokens or events

var tama = new Tamagotchi(romdir, eeprom);
var lcd = new Lcd();

// rom = LoadRoms(romdir);
// tama = TamaInit(rom, eeprom);
// BenevolentAiInit();
// UdpInit(host);
// TermInit();
// TermRaw(true);

var stopwatch = new Stopwatch();
while (true)
{
    stopwatch.Restart();

    ConsoleKey k = ConsoleKey.None;

    tama.NextStep();
    lcd.Render(tama.dram);
    
    //  tamaRun(tama, FCPU/FPS-1);
    // lcdRender(tama->dram, tama->lcd.sizex, tama->lcd.sizey, &display);
    // udpTick();
    if (aiEnabled)
    {
        // k=benevolentAiRun(&display, 1000/FPS);
    }
    else
    {
        k = 0;
    }

    if (!speedup || (t & 15) == 0)
    {
        lcd.Show();
        
        // udpSendDisplay(&display);
        // tamaDumpHw(tama->cpu);
        // benevolentAiDump();
    }


    if ((k == ConsoleKey.PrintScreen))
    {
        // //If anything interesting happens, make a LCD dump.
        // lcdDump(tama->dram, tama->lcd.sizex, tama->lcd.sizey, "lcddump.lcd");
        // if (stopDisplay) {
        //     tama->cpu->Trace=1;
        //     speedup=0;
        // }
    }

    stopwatch.Stop();
    var elapsed = stopwatch.Elapsed;
    elapsed = FPS_TS - elapsed;
    Console.WriteLine($"Time left in frame: {elapsed} us");
    if (!speedup && elapsed.TotalSeconds > 0)
    {
        Thread.Sleep(elapsed);
    }

    if (Console.KeyAvailable)
        k = Console.ReadKey().Key;

    switch (k)
    {
        case ConsoleKey.NumPad1:
           tama.tamaPressBtn(0);
            break;
        case ConsoleKey.NumPad2:
            tama.tamaPressBtn(  1);
            break;
        case ConsoleKey.NumPad3:
            tama. tamaPressBtn( 2);
            break;
        case ConsoleKey.S:
            speedup = !speedup;
            break;
        case ConsoleKey.D:
            stopDisplay = !stopDisplay;
            break;
    }

    t++;
}


//         udpExit();
//         tamaDeinit(tama);