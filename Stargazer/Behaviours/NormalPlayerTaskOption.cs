using System;
using System.Collections.Generic;
using System.Text;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using UnhollowerBaseLib.Attributes;

namespace Stargazer.Behaviours
{
    [System.Serializable]
    public class NormalPlayerTaskOption : MonoBehaviour
    {
        static NormalPlayerTaskOption()
        {
            ClassInjector.RegisterTypeInIl2Cpp<NormalPlayerTaskOption>();
        }


        public NormalPlayerTaskOption(IntPtr ptr) : base(ptr)
        {
            enabled = true;
        }
        
        public int InitializerId;


        public void Initialize()
        {
            if (InitializerId<0 || Behaviours.CustomShipStatus.Instance.TaskInitializerList.Count<=InitializerId) return;
            Behaviours.CustomShipStatus.Instance.TaskInitializerList[InitializerId].Invoke(gameObject.GetComponent<NormalPlayerTask>());
        }
    }
}
