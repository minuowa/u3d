using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Config
{
    public class TaskData:Record<TaskData>
    {
        public static string filename = "config/TaskData";
        public List<TaskChild> childs;
    }
}
