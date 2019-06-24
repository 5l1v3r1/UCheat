using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading;
using UCheat.Core;
using UCheat.Core.Utils;
using UCheat.Offsets;

namespace UCheat.Functions
{
    class UAimBot
    {
        #region Переменные
        protected internal static UCore Core;
        protected internal static bool isActive;
        private static int _localPlayer;
        private static Vector3 _playerPos;
        private static Vector3 _viewAngles;
        /// <summary>
        /// Наводиться только при клике по мыши?
        /// </summary>
        private static bool isMouse = true;
        #endregion

        #region Функции
        /// <summary>
        /// Старт функции Aim (Стандарт)
        /// </summary>
        protected internal static void AimBot()
        {
            while (true)
            {
                if (isActive)
                {
                    int target = GetTarget();
                    if (target != -1)
                    {
                        Vector3 dst = AngelToTarget(target, 8);
                        SetViewAngel(dst);
                    }
                }
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Старт функции Aim (Сглаженный)
        /// </summary>
        protected internal static void AimBotSmooth()
        {
            while (true)
            {
                if (isActive)
                {
                    int target = GetTarget();
                    int clientState = Core.mem.Read<int>(Core.Engine_Dll + Offset.dwClientState);
                    Vector3 oldYaw = Core.mem.Read<Vector3>(clientState + Offset.dwClientState_ViewAngles);
                    if (target != -1)
                    {
                        Vector3 dst = AngelToTarget(target, 8);
                        if (Core.mem.Read<int>(Core.Client_Dll + Offset.dwForceAttack) == 5)
                            SetViewAngel(Vector3.Lerp(oldYaw, dst, 0.5f));

                    }
                }
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Получение хитбоксов
        /// </summary>
        /// <param name="pTargetIn">цель из которой будем получать</param>
        /// <param name="bone">точка хитбокса (от 1 до 8)</param>
        /// <returns></returns>
        private static Vector3 GetBonePos(int pTargetIn, int bone)
        {
            int matrix = Core.mem.Read<int>(pTargetIn + Offset.m_dwBoneMatrix);
            Vector3 bonePos = new Vector3
            {
                X = Core.mem.Read<float>(matrix + 0x30 * bone + 0xC),
                Y = Core.mem.Read<float>(matrix + 0x30 * bone + 0x1C),
                Z = Core.mem.Read<float>(matrix + 0x30 * bone + 0x2C)
            };
            return bonePos;
        }
        /// <summary>
        /// Дальность угла к цели
        /// </summary>
        /// <param name="pTargetIn">Сама цель для вычисления</param>
        /// <param name="boneIndex">точка вычисления</param>
        /// <returns></returns>
        private static Vector3 AngelToTarget(int pTargetIn, int boneIndex)
        {
            _localPlayer = Core.mem.Read<int>(Core.Client_Dll + Offset.dwLocalPlayer);
            Vector3 position = Core.mem.Read<Vector3>(_localPlayer + Offset.m_vecOrigin);
            Vector3 vecView = Core.mem.Read<Vector3>(_localPlayer + Offset.m_vecViewOffset);
            Vector3 myView = position + vecView;
            Vector3 aimView = GetBonePos(pTargetIn, boneIndex);
            Vector3 dst = myView.CalcAngle(aimView);
            dst = dst.NormalizeAngle();
            return dst;

        }
        /// <summary>
        /// Установка угла 
        /// </summary>
        /// <param name="angle">Угол</param>
        private static void SetViewAngel(Vector3 angle)
        {
            int EngineBase = Core.mem.Read<int>(Core.Engine_Dll + Offset.dwClientState);
            angle = angle.ClampAngle();
            angle = angle.NormalizeAngle();
            Core.mem.Write(EngineBase + Offset.dwClientState_ViewAngles, angle);
        }
        /// <summary>
        /// Получение цели
        /// </summary>
        /// <returns>Если возвращаемое число не равна -1 то цель найдена, или нет</returns>
        private static int GetTarget()
        {
            int currentTarget = -1;
            float misDist = 700;
            int clientState = Core.mem.Read<int>(Core.Engine_Dll + Offset.dwClientState);
            _localPlayer = Core.mem.Read<int>(Core.Client_Dll + Offset.dwLocalPlayer);
            _viewAngles = Core.mem.Read<Vector3>(clientState + Offset.dwClientState_ViewAngles);
            int PlayerTeam = Core.mem.Read<int>(_localPlayer + Offset.m_iTeamNum);
            for (int i = 0; i <= 64; i++)
            {
                _playerPos = Core.mem.Read<Vector3>(_localPlayer + Offset.m_vecOrigin);
                int target = Core.mem.Read<int>(Core.Client_Dll + Offset.dwEntityList + (i - 1) * 0x10);
                Vector3 targetPos = Core.mem.Read<Vector3>(target + Offset.m_vecOrigin);
                int targetTeam = Core.mem.Read<int>(target + Offset.m_iTeamNum);
                int targetHealth = Core.mem.Read<int>(target + Offset.m_iHealth);
                bool targetDormant = Core.mem.Read<bool>(target + Offset.m_bDormant);
                if (targetTeam != PlayerTeam && targetTeam != 0 && !targetDormant && targetHealth != 0 && Vector3.Distance(targetPos, _playerPos) < misDist)
                {
                    Vector3 angleToTarget = AngelToTarget(target, 8);
                    var fov = MathUtils.Fov(_viewAngles, angleToTarget);
                    if (fov <= 20 && Core.mem.Read<bool>(target + Offset.m_bSpottedByMask))
                    {
                        misDist = Vector3.Distance(targetPos, _playerPos);
                        currentTarget = target;
                    }
                }
            }
            return currentTarget;
        }
        #endregion
    }
}