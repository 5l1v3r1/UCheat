using System;
using System.Threading;
using UCheat.Core;
using UCheat.Offsets;

namespace UCheat.Functions
{
    class UWallHack
    {
        #region Переменные
        protected internal UCore Core;
        protected internal bool isActive;
        #endregion

        #region Структуры
        /// <summary>
        /// Структура для настройки
        /// </summary>
        private struct GlowSet
        {
            public float r;//Красный
            public float g;//Зеленый
            public float b;//Синий
            public float a;//Альфа (Прозрачность)
            public bool rwo;//Делать обводу?
        }
        /// <summary>
        /// Локальный цвет команды
        /// </summary>
        GlowSet localTeam = new GlowSet()
        {
            r = 0,
            g = 255,
            b = 0,
            a = 200,

            rwo = true,
        };
        /// <summary>
        /// Цвет другой команды (к примеру врага)
        /// </summary>
        GlowSet enemyTeam = new GlowSet()
        {
            r = 255,
            g = 0,
            b = 0,
            a = 200,

            rwo = true,
        };
        #endregion

        #region Функции
        /// <summary>
        /// Функция для старта отрисовки игроков
        /// </summary>
        protected internal void DrawWallHack()
        {
            while (true)
            {
                if (isActive)
                {
                    int localPlayer = Core.mem.Read<int>(Core.Client_Dll + Offset.dwLocalPlayer);
                    int isLocalTeam = Core.mem.Read<int>(localPlayer + Offset.m_iTeamNum);
                    if (!Core.mem.Read<bool>(localPlayer + Offset.m_bDormant))
                    {
                        for (int i = 1; i <= 64; i++)
                        {
                            int entityList = Core.mem.Read<int>(Core.Client_Dll + Offset.dwEntityList + (i - 1) * 0x10);
                            int entityTeam = Core.mem.Read<int>(entityList + Offset.m_iTeamNum);
                            int glowIndex = Core.mem.Read<int>(entityList + Offset.m_iGlowIndex);

                            if (entityTeam != 0 && entityTeam != isLocalTeam)
                            {
                                DrawGlow(glowIndex, localTeam);
                            }
                            else
                            {
                                DrawGlow(glowIndex, enemyTeam);
                            }
                        }
                    }
                    Thread.Sleep(30);
                }
            }
        }

        /// <summary>
        /// Начать рисование
        /// </summary>
        /// <param name="pGlowIn">Пропуск для отрисовки</param>
        /// <param name="col">Информация для отрисовки</param>
        private void DrawGlow(int pGlowIn, GlowSet col)
        {
            int GlowObject = Core.mem.Read<int>(Core.Client_Dll + Offset.dwGlowObjectManager);
            Core.mem.Write((GlowObject + (pGlowIn * 0x38) + 8), col.r);
            Core.mem.Write((GlowObject + (pGlowIn * 0x38) + 4), col.g);
            Core.mem.Write((GlowObject + (pGlowIn * 0x38) + 12), col.b);
            Core.mem.Write((GlowObject + (pGlowIn * 0x38) + 0x10), col.a);
            Core.mem.Write((GlowObject + (pGlowIn * 0x38) + 0x24), col.rwo);
        }
        #endregion
    }
}
