using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UCheat.Core;
using UCheat.Offsets;

namespace UCheat.Functions
{
    class UNoFlash
    {
        #region Переменные
        protected internal static UCore Core;
        protected internal static bool isActive;
        #endregion

        #region Функции
        /// <summary>
        /// Функция для старта NoFlash
        /// </summary>
        protected internal static void NoFlash()
        {
            while (true)
            {
                if (isActive)
                {
                    int localPlayer = Core.mem.Read<int>(Core.Client_Dll + Offset.dwLocalPlayer);
                    int NoFlashIndex = Core.mem.Read<int>(localPlayer + Offset.m_flFlashDuration);
                    if (NoFlashIndex != 0)
                        Core.mem.Write(localPlayer + Offset.m_flFlashDuration, 0);
                    Thread.Sleep(30);
                }
            }
        }
        #endregion
    }
}
