using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSec
{
    static class PlatformCollection
    {
        static Dictionary<Platform, IPlatform> _Plats;
        public static Dictionary<Platform, IPlatform> GetPlatforms()
        {
            if (_Plats == null)
            {
                _Plats = new Dictionary<Platform, IPlatform>();
                _Plats.Add(Platform.Lua, new Lua());
                _Plats.Add(Platform.Roblox, new Roblox());
                _Plats.Add(Platform.CSGO, new CSGO());
            }

            return _Plats;
        }
        public static IPlatform GetPlatform(Platform plat)
        {
            return GetPlatforms()[plat];
        }
    }
}
