using System;
using System.Collections.Generic;
using System.Text;

namespace Stargazer.Database
{
    public enum TaskCategory
    {
        CommonTask,
        LongTask,
        ShortTask
    }

    public class TaskData
    {
        public List<List<string>> ConsoleList { get; set; }
        int TaskId { get; set; }
        TaskCategory TaskCategory { get; set; }
    }
}
