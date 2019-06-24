using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCheat.Core
{
    class UCore
    {
        #region Переменные
        protected internal Memory mem;
        protected internal int Client_Dll, Engine_Dll;
        #endregion

        #region Функции
        /// <summary>
        /// Получение адреса клиента
        /// </summary>
        protected internal void GetClientDLL()
        {
            try
            {
                Process csgo = Process.GetProcessesByName("csgo")[0];
                mem = new Memory("csgo");
                foreach (ProcessModule module in csgo.Modules)

                {
                    if (module.ModuleName == "client_panorama.dll")
                        Client_Dll = (int)module.BaseAddress;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Получение адреса движка
        /// </summary>
        protected internal void GetEngineDll()
        {
            try
            {
                Process csgo = Process.GetProcessesByName("csgo")[0];
                mem = new Memory("csgo");
                foreach (ProcessModule module in csgo.Modules)

                {
                    if (module.ModuleName == "engine.dll")
                        Engine_Dll = (int)module.BaseAddress;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
