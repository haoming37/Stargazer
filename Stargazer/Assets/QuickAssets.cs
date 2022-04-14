using System;
using System.Collections.Generic;
using System.Text;

namespace Stargazer.Assets
{
    public static class QuickAssets
    {
        public static Vent GetVent(byte type)
        {
            if (type == 0) return MapAssets.GetAsset(0).transform.GetComponentInChildren<Vent>();
            else if (type == 1) return MapAssets.GetAsset(2).transform.GetComponentInChildren<Vent>();

            return MapAssets.GetAsset(0).transform.GetComponentInChildren<Vent>();
        }
    }
}
