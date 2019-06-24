using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using UCheat.Core;
using UCheat.Functions;

namespace UCheat
{
    class MainThread
    {
        public static int x = 0;
        public static string[,] polygon = new string[5, 5];
        public static string[] tabs = new string[5] { "Aim Bot :", "Wall Hack :", "No Flash :", "Trigger Bot :", "Radar Hack :" };
        public static bool[] setts = new bool[5] { false, false, false, false, false }; //0 - aimbot; 1 - wall hack, 2 - no flash, 3 - trigger bot, 4 - radar hack
        public static UWallHack uWH = new UWallHack();
        private static UCore Core = new UCore();

        public static void Render()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("   ///////////////////////////////");
            Console.WriteLine("  //////  Uni Cheat v0.1A  //////");
            Console.WriteLine(" //////  Author Gastline  //////");
            Console.WriteLine("///////////////////////////////");
            Console.ResetColor();
            Console.WriteLine("\n///////////////////////////////");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (polygon[i, j] == tabs[i])
                    {
                        bool active = setts[i];
                        Console.Write(polygon[i, j]);
                        if (active)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" ON");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" OFF");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.Write(polygon[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("///////////////////////////////");
            Console.ResetColor();
        }
        public static void Update(int dir, bool init)
        {
            if (!init)
            {
                int[] delta_x = new int[4] { -1, 1, -1, 1 };
                int _X = delta_x[dir] + x;
                if (_X > 4 || _X < 0)
                    return;
                polygon[x, 0] = "";
                x = _X;
                polygon[_X, 0] = "->";
            }
            else
            {
                polygon[0, 1] = tabs[0];
                polygon[1, 1] = tabs[1];
                polygon[2, 1] = tabs[2];
                polygon[3, 1] = tabs[3];
                polygon[4, 1] = tabs[4];
            }
            Console.Clear();
            UAimBot.isActive = setts[0];
            uWH.isActive = setts[1];
            UNoFlash.isActive = setts[2];
            UTriggerBot.isActive = setts[3];
            URadarHack.isActive = setts[4];
            Render();
            return;
        }

       /* public static string sendPacket(string name, string password, string dataBase)
        {
            WebRequest request = WebRequest.Create("https://localMachine/cheatsbase/login.php");
            request.Method = "POST";
            string data = $"name={name}&password={password}&data={dataBase}";
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = request.GetResponse();
            string str = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            response.Close();
            return str;
        }
        private static string GetData()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    var req = WebRequest.Create("http://checkip.dyndns.org");
                    string reqstring;

                    using (var reader = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        reqstring = reader.ReadToEnd();
                    }
                    string[] a = reqstring.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    string ip = a3[0];
                    DateTime utcDate = DateTime.UtcNow;
                    macAddresses += $"\n{utcDate}|   Mac: {nic.GetPhysicalAddress().ToString()} Ip: {ip} NamePC: {Environment.UserName}";
                    break;
                }
            }
            return macAddresses;
        }
        public static string sendPacket()
        {
            WebRequest request = WebRequest.Create("https://localMachine/cheatsbase/getVersion.php");
            request.Method = "POST";

            WebResponse response = request.GetResponse();
            string str = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            response.Close();
            return str;
        }
        */

        [DllImport("user32.dll")]
        static extern int SetWindowText(IntPtr hWnd, string text);
        protected internal static void hideInProcess()
        {
            using (System.Diagnostics.Process hideProcess = System.Diagnostics.Process.GetCurrentProcess())
            {
                SetWindowText(hideProcess.MainWindowHandle, "Paint");
            }
        }
        /*public static void Download(string adress, string name)
        {
            WebClient wb = new WebClient();
            Directory.CreateDirectory(@"C:/Uni");
            string save_path = @"C:/Uni" + @"/";
            ShortCut.Create(save_path + name, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"/сheat.lnk", "", "uniCheat");
            wb.DownloadFile(adress, save_path + name);
        }
        */
        public static void Main()
        {
            System.Threading.Thread.Sleep(3000);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("   ///////////////////////////////");
            Console.WriteLine("  //////  Uni Cheat v0.1A  //////");
            Console.WriteLine(" //////  Author ArtemiyG  //////");
            Console.WriteLine("///////////////////////////////");
            Console.ResetColor();
            Console.WriteLine("\n///////////////////////////////");

            //Проверка обновления чита
            /*if (Directory.Exists(@"C:/Uni"))
            {
                if (!File.Exists(@"C:/Uni/dataSave.us"))
                {
                    Console.WriteLine("Обновление 0.1:\n1. Обновлена база игры (для анти детекта)");
                    File.Create(@"C:/Uni/dataSave.us");
                }
            }
            if (sendPacket() != "0.1")
            {
                Console.WriteLine("Было найдено обновление, включите Uni_Downloader.exe чтобы обновиться!\nТекущая версия 0.1  ");
                Console.Read();
            }*/

            Console.Title = "Paint";
            bool start = true;
            hideInProcess();
            while (start)
            {
                Console.Write("[] Добро пожаловать, введите имя пользователя: ");
                string name = Console.ReadLine();
                Console.Write("[] Введите пароль: ");
                string password = Console.ReadLine();
                if (true)//(sendPacket(name, password, GetData()).IndexOf("correct") > -1)
                {
                    start = false;
                    Core.GetClientDLL();
                    Core.GetEngineDll();
                    UNoFlash.Core = URadarHack.Core = UTriggerBot.Core = UAimBot.Core = uWH.Core = Core;
                    Thread nFlash = new Thread(new ThreadStart(UNoFlash.NoFlash));
                    Thread wHack = new Thread(new ThreadStart(uWH.DrawWallHack));
                    Thread tBot = new Thread(new ThreadStart(UTriggerBot.TriggerBot));
                    //Thread aBot = new Thread(new ThreadStart(UAimBot.AimBotSmooth));//Сглаживаемое
                    Thread aBot = new Thread(new ThreadStart(UAimBot.AimBot));
                    Thread rHack = new Thread(new ThreadStart(URadarHack.RadarHack));
                    nFlash.Start();
                    wHack.Start();
                    tBot.Start();
                    rHack.Start();
                    aBot.Start();
                    break;
                }
                else
                {
                    Console.WriteLine("[] Произошла ошибка, возможно не верный логин или пароль!");
                }
            }

            Console.CursorVisible = false;
            Console.SetWindowPosition(0, 0);
            polygon[0, 0] = "->";
            Update(0, true);
            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                {
                    Update(0, false);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    Update(1, false);
                }
                else if (key == ConsoleKey.Enter)
                {
                    setts[x] = !setts[x];
                    Update(2, true);
                }
            }
        }
    }
    /*private static UCore Core = new UCore();
    private static UWallHack uWH = new UWallHack();
    //private UNoFlash uFlash = new UNoFlash();

    private static void Main()
    {
        Core.GetClientDLL();
        Core.GetEngineDll();
        UNoFlash.Core = URadarHack.Core = UTriggerBot.Core = UAimBot.Core = uWH.Core = Core;
        Thread nFlash = new Thread(new ThreadStart(UNoFlash.NoFlash));
        Thread wHack = new Thread(new ThreadStart(uWH.DrawWallHack));
        Thread tBot = new Thread(new ThreadStart(UTriggerBot.TriggerBot));
        Thread aBot = new Thread(new ThreadStart(UAimBot.AimBotSmooth));
        Thread rHack = new Thread(new ThreadStart(URadarHack.RadarHack));
        nFlash.Start();
        wHack.Start();
        //tBot.Start();
        rHack.Start();
        aBot.Start();
    }
    */
    static class ShellLink
    {
        [ComImport,
        Guid("000214F9-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IShellLinkW
        {
            [PreserveSig]
            int GetPath(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszFile,
                int cch, ref IntPtr pfd, uint fFlags);

            [PreserveSig]
            int GetIDList(out IntPtr ppidl);

            [PreserveSig]
            int SetIDList(IntPtr pidl);

            [PreserveSig]
            int GetDescription(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszName, int cch);

            [PreserveSig]
            int SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszName);

            [PreserveSig]
            int GetWorkingDirectory(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszDir, int cch);

            [PreserveSig]
            int SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszDir);

            [PreserveSig]
            int GetArguments(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszArgs, int cch);

            [PreserveSig]
            int SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszArgs);

            [PreserveSig]
            int GetHotkey(out ushort pwHotkey);

            [PreserveSig]
            int SetHotkey(ushort wHotkey);

            [PreserveSig]
            int GetShowCmd(out int piShowCmd);

            [PreserveSig]
            int SetShowCmd(int iShowCmd);

            [PreserveSig]
            int GetIconLocation(
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder pszIconPath, int cch, out int piIcon);

            [PreserveSig]
            int SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszIconPath, int iIcon);

            [PreserveSig]
            int SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszPathRel, uint dwReserved);

            [PreserveSig]
            int Resolve(IntPtr hwnd, uint fFlags);

            [PreserveSig]
            int SetPath(
                [MarshalAs(UnmanagedType.LPWStr)]
                string pszFile);
        }

        [ComImport,
        Guid("00021401-0000-0000-C000-000000000046"),
        ClassInterface(ClassInterfaceType.None)]
        private class shl_link { }

        internal static IShellLinkW CreateShellLink()
        {
            return (IShellLinkW)(new shl_link());
        }
    }
    public static class ShortCut
    {
        public static void Create(
            string PathToFile, string PathToLink,
            string Arguments, string Description)
        {
            ShellLink.IShellLinkW shlLink = ShellLink.CreateShellLink();

            Marshal.ThrowExceptionForHR(shlLink.SetDescription(Description));
            Marshal.ThrowExceptionForHR(shlLink.SetPath(PathToFile));
            Marshal.ThrowExceptionForHR(shlLink.SetArguments(Arguments));

            ((System.Runtime.InteropServices.ComTypes.IPersistFile)shlLink).Save(PathToLink, false);
        }
    }
}
