using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder.Task
{
    public class TaskBuilder
    {
        public static Dictionary<TaskTypes, TaskBuilder> TaskBuilders = new Dictionary<TaskTypes, TaskBuilder>();

        public static void LoadVanillaTaskBuilders()
        {
            TaskBuilder defaultBuilder = new TaskBuilder();
            TaskBuilders[TaskTypes.FixWiring] = defaultBuilder;
        }

        protected void SetUpConsole(CustomConsole cc, ShipStatus shipStatus,Blueprint blueprint, Database.TaskData taskData)
        {
            Console console = cc.GameObject.GetComponent<Console>();

            if (!console)
            {
                console = cc.GameObject.AddComponent<Console>();
                console.checkWalls = true;
                console.usableDistance = 0.9f;
                console.TaskTypes = new TaskTypes[0];
                console.ValidTasks = new UnhollowerBaseLib.Il2CppReferenceArray<TaskSet>(0);
                console.Room = cc.RoomId;

                shipStatus.AllConsoles = Helpers.AddToReferenceArray(shipStatus.AllConsoles, console);

                console.Image = cc.GameObject.GetComponent<SpriteRenderer>();
                console.Image.material = new Material(blueprint.HighlightMaterial);
            }

            if (!console.TaskTypes.Contains(taskData.TaskType))
            {
                var tasks = new List<TaskTypes>(console.TaskTypes);
                tasks.Add(taskData.TaskType);
                console.TaskTypes = tasks.ToArray();
            }

            console.ConsoleId = cc.TaskConsoleId;
        }

        protected void SetUpButton(CustomConsole cc, Blueprint blueprint, Database.TaskData taskData)
        {
            PassiveButton button = cc.GameObject.GetComponent<PassiveButton>();

            if (!button)
            {
                button = cc.GameObject.AddComponent<PassiveButton>();
                button.OnMouseOut = new UnityEngine.Events.UnityEvent();
                button.OnMouseOver = new UnityEngine.Events.UnityEvent();
                button._CachedZ_k__BackingField = 0.1f;
                button.CachedZ = 0.1f;
            }
        }

        protected void SetUpCollider(CustomConsole cc, Blueprint blueprint, Database.TaskData taskData)
        {
            CircleCollider2D collider = cc.GameObject.GetComponent<CircleCollider2D>();

            if (!collider)
            {
                collider = cc.GameObject.AddComponent<CircleCollider2D>();
                collider.radius = 0.4f;
                collider.isTrigger = true;
            }
        }

        //タスクデータを基にタスクを生成します。
        public virtual NormalPlayerTask BuildTask(ShipStatus shipStatus,GameObject taskHolder,Blueprint blueprint, Database.TaskData taskData)
        {
            foreach (var list in taskData.ConsoleList)
            {
                foreach (string id in list)
                {
                    if (!blueprint.Consoles.ContainsKey(id)) continue;
                    var cc = blueprint.Consoles[id];

                    SetUpConsole(cc, shipStatus,blueprint, taskData);
                    SetUpButton(cc, blueprint, taskData);
                    SetUpCollider(cc, blueprint, taskData);
                }
            }

            NormalPlayerTask task;
            if (taskData.TaskType == TaskTypes.ActivateWeatherNodes)
            {
                task = taskHolder.AddComponent<WeatherNodeTask>();
                WeatherNodeTask weatherNodeTask = task.Cast<WeatherNodeTask>();
                weatherNodeTask.Stage2Prefab = Assets.QuickAssets.GetMinigamePrefab(taskData.TaskType,1);
            }
            else if (taskData.TaskType == TaskTypes.PickUpTowels)
                task = taskHolder.AddComponent<TowelTask>();
            else if (taskData.TaskType == TaskTypes.OpenWaterways)
                task = taskHolder.AddComponent<WaterWayTask>();
            else
                task = taskHolder.AddComponent<NormalPlayerTask>();

            task.MaxStep = taskData.MaxSteps;
            task.ShowTaskStep = taskData.ShowTaskSteps;
            task.ShowTaskTimer = taskData.ShowTaskTimer;
            task.TaskTimer = 0;
            task.TimerStarted = NormalPlayerTask.TimerState.NotStarted;
            task.TaskType = taskData.TaskType;
            task.HasLocation = true;
            task.Length = Database.TaskData.ConvertLength(taskData.TaskCategory);
            task.MinigamePrefab = Assets.QuickAssets.GetMinigamePrefab(taskData.TaskType);

            return task;
        }
    }

    public delegate NormalPlayerTask TaskBuilderF(ShipStatus shipStatus, GameObject taskHolder, Blueprint blueprint, Database.TaskData taskData);

    public class SimpleTaskBuilder : TaskBuilder
    {
        private TaskBuilderF Builder;

        public SimpleTaskBuilder(TaskBuilderF taskBuilder)
        {
            Builder = taskBuilder;
        }

        public override NormalPlayerTask BuildTask(ShipStatus shipStatus,GameObject taskHolder, Blueprint blueprint, Database.TaskData taskData)
        {
            return Builder.Invoke(shipStatus,taskHolder, blueprint, taskData);
        }
    }
}
