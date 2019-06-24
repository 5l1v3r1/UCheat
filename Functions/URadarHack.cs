using System.Threading;
using UCheat.Core;
using UCheat.Offsets;
using System;

namespace UCheat.Functions
{
    class URadarHack
    {
        #region Переменные
        protected internal static UCore Core;
        protected internal static bool isActive;
        #endregion

        #region Функции
        /// <summary>
        /// Запуск функции RadarHack
        /// </summary>
        protected static internal void RadarHack()
        {
            while (true)
            {
                if (isActive)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        int entityEnemy = Core.mem.Read<int>(Core.Client_Dll + Offset.dwEntityList + (i - 1) * 0x10);
                        Core.mem.Write<bool>(Core.Client_Dll + Offset.m_bSpottedByMask, true);
                        Thread.Sleep(30);
                    }
                }
            }
        }
    }
    #endregion
}
