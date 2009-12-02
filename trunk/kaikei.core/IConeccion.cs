using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kaikei.core
{
    interface IConeccion
    {
        int sqlInsert();
        int sqlDelete();
        int sqlUpdate();
    }
}
