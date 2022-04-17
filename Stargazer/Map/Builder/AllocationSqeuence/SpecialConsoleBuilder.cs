using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Stargazer.Map.Builder.AllocationSqeuence
{
    public class SpecialConsoleType
    {
        static public Dictionary<string, SpecialConsoleType> AllTypes = new Dictionary<string, SpecialConsoleType>();

        static public void LoadVanillaTypes()
        {
            AllTypes["Camera"] = new SpecialConsoleType();
            AllTypes["SwitchCamera"] = new SpecialConsoleType();
            AllTypes["Vitals"] = new SpecialConsoleType();
            AllTypes["Admin"] = new SpecialConsoleType();
        }

        public virtual void Build(SpecialConsoleBuilder consoleData,CustomConsole console,Blueprint blueprint,ShipStatus shipStatus){}
    }

    public class SpecialConsoleAdmin : SpecialConsoleType
    {
        public override void Build(SpecialConsoleBuilder consoleData, CustomConsole console, Blueprint blueprint, ShipStatus shipStatus) 
        {
            var mConsole = console.GameObject.AddComponent<MapConsole>();
            mConsole.usableDistance = 0.9f;

            mConsole.Image = console.GameObject.GetComponent<SpriteRenderer>();
            mConsole.Image.material = new Material(blueprint.HighlightMaterial);
        }
    }

    public class SpecialConsoleVitals : SpecialConsoleType
    {
        public override void Build(SpecialConsoleBuilder consoleData, CustomConsole console, Blueprint blueprint, ShipStatus shipStatus)
        {
            var vConsole = console.GameObject.AddComponent<SystemConsole>();
            vConsole.usableDistance = 0.9f;
            

            vConsole.Image = console.GameObject.GetComponent<SpriteRenderer>();
            vConsole.Image.material = new Material(blueprint.HighlightMaterial);
        }
    }

    public class SpecialConsoleBuilder : AllocationSequence
    {
        public string ConsoleType;
        public string ConsoleId;

        public override void Build(Blueprint blueprint, ShipStatus shipStatus)
        {
            if (!SpecialConsoleType.AllTypes.ContainsKey(ConsoleType)) return;
            SpecialConsoleType consoleType = SpecialConsoleType.AllTypes[ConsoleType];

            if (!blueprint.Consoles.ContainsKey(ConsoleId)) return;
            CustomConsole console = blueprint.Consoles[ConsoleId];

            consoleType.Build(this, console, blueprint, shipStatus);
        }

        public SpecialConsoleBuilder(string consoleType,string consoleId)
        {
            ConsoleType = consoleType;
            ConsoleId = consoleId;
        }
    }
}
