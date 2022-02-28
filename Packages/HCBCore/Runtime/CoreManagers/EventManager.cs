using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using HCB.Utilities;

namespace HCB.Core
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDataChange = new UnityEvent();
        public static CurrencyEvent OnCurrencyInteracted = new CurrencyEvent();
        public static UnityEvent OnPlayerHit = new UnityEvent();
        public static UnityEvent OnPancakeCooked = new UnityEvent();
        public static UnityEvent OnPancakeCooking = new UnityEvent();
        public static UnityEvent OnMovementStart = new UnityEvent();
        public static UnityEvent OnMovementStop = new UnityEvent();

        #region Editor
        public static UnityEvent OnLevelDataChange = new UnityEvent();
        #endregion
    }

    public class PlayerDataEvent : UnityEvent<PlayerData> { }
    public class CurrencyEvent : UnityEvent<ExchangeType, int> { }
}