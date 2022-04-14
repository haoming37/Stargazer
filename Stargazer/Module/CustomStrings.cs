using System;
using System.Collections.Generic;
using System.Text;

namespace Stargazer.Module
{
    public class CustomSystemTypes
    {
        static private Dictionary<string, CustomSystemTypes> stringDic = new Dictionary<string, CustomSystemTypes>();
        static private Dictionary<int, CustomSystemTypes> intDic = new Dictionary<int, CustomSystemTypes>();
        static private int availableId = 128;

        private int Id;
        private string Name;

        public static CustomSystemTypes RegisterSystemTypes(string name)
        {
            if (stringDic.ContainsKey(name)) return stringDic[name];
            return new CustomSystemTypes(name,availableId++);
        }

        private CustomSystemTypes(string name,int id)
        {
            Name = name;
            Id = id;

            stringDic[name] = this;
            intDic[id] = this;
        }

        public string GetTranslatedString()
        {
            return "Default";
        }

        public static bool IsCustomSystemTypes(SystemTypes systemTypes)
        {
            return intDic.ContainsKey((int)systemTypes);
        }

        public static CustomSystemTypes? GetCustomSystemTypes(SystemTypes systemTypes)
        {
            if (IsCustomSystemTypes(systemTypes)) return intDic[(int)systemTypes];
            return null;
        }
    }

    public class CustomStrings
    {
        static private Dictionary<string, CustomStrings> stringDic = new Dictionary<string, CustomStrings>();
        static private Dictionary<int, CustomStrings> intDic = new Dictionary<int, CustomStrings>();
        static private int availableId = 128;

        private int Id;
        private string Name;

        public static CustomStrings RegisterStrings(string name)
        {
            if (stringDic.ContainsKey(name)) return stringDic[name];
            return new CustomStrings(name, availableId++);
        }

        private CustomStrings(string name, int id)
        {
            Name = name;
            Id = id;

            stringDic[name] = this;
            intDic[id] = this;
        }

        public string GetTranslatedString()
        {
            return "Default";
        }

        public static bool IsCustomStrings(StringNames stringNames)
        {
            return intDic.ContainsKey((int)stringNames);
        }

        public static CustomStrings? GetCustomStrings(StringNames stringNames)
        {
            if (IsCustomStrings(stringNames)) return intDic[(int)stringNames];
            return null;
        }
    }
}
