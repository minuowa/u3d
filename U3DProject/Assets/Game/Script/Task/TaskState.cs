using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum TaskState
{
    None,           ///不可接取
    CanGet,         ///可接取
    Doing,          ///正在做
    CanComit,       ///可交付
    Done,           ///已交付
}
