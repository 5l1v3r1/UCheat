using System.Threading;
using UCheat.Core;
using UCheat.Offsets;

namespace UCheat.Functions
{
    class UTriggerBot
    {
        #region Переменные
        protected internal static UCore Core;
        protected internal static bool isActive;
        #endregion

        #region Функции
        /// <summary>
        /// Запуск функции TriggerBot
        /// </summary>
        protected static internal void TriggerBot()
        {
            while (true)
            {
                if (isActive)
                {
                    int localPlayer = Core.mem.Read<int>(Core.Client_Dll + Offset.dwLocalPlayer);
                    int myTeam = Core.mem.Read<int>(localPlayer + Offset.m_iTeamNum);
                    int PlayerInCross = Core.mem.Read<int>(localPlayer + Offset.m_iCrosshairId);
                    if (PlayerInCross > 0 && PlayerInCross <= 64)
                    {
                        int PtrToPic = Core.mem.Read<int>(Core.Client_Dll + Offset.dwEntityList + (PlayerInCross - 1) * 16);
                        int PICTeam = Core.mem.Read<int>(PtrToPic + Offset.m_iTeamNum);
                        if (PICTeam != myTeam)
                        {
                            Core.mem.Write(Core.Client_Dll + Offset.dwForceAttack, 4);
                            Thread.Sleep(0x30);
                            Core.mem.Write(Core.Client_Dll + Offset.dwForceAttack, 5);
                        }
                    }
                }
            }
        }
    }
    #endregion
}
