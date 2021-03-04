namespace MoonSec
{
    class Lua : IPlatform
    {
        public string name
        {
            get { 
                return "Lua"; 
            }
        }
        public string type
        {
            get
            {
                return "lua";
            }
        }

        public Lua()
        { }
    }
}
