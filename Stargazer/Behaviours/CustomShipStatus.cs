using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnhollowerRuntimeLib;

namespace Stargazer.Behaviours
{
    public class CustomShipStatus : MonoBehaviour
    {
        public List<Action<NormalPlayerTask>> TaskInitializerList = new List<Action<NormalPlayerTask>>();

        static public CustomShipStatus Instance;

        static CustomShipStatus()
        {
            ClassInjector.RegisterTypeInIl2Cpp<CustomShipStatus>();
        }

        public CustomShipStatus(IntPtr ptr) : base(ptr)
        {
            enabled = true;

        }
    }
}
