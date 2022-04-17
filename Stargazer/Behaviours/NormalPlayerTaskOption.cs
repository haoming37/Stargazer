using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Behaviours
{
    public class NormalPlayerTaskOption : MonoBehaviour
    {
        public bool IsStargazerCustomTask;
        public NormalPlayerTask Task;
        

        public void Start()
        {
            Task = gameObject.GetComponent<NormalPlayerTask>();
            IsStargazerCustomTask = Module.CustomTaskTypes.IsCustomTaskTypes(Task.TaskType);
        }
    }
}
