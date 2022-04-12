using System;
using System.Collections.Generic;
using System.Text;

namespace Stargazer
{
    public static class Helpers
    {
        static public bool PlayingModMap()
        {
            return PlayerControl.GameOptions.MapId >= 5;
        }
    }
}
