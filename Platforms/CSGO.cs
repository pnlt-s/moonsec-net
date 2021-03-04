namespace MoonSec
{
    class CSGO : IPlatform
    {
        public string name { 
            get { return "Counter Strike: Global Offensive"; }  
        }
        public string type {
            get { return "csgo"; }
        }
        public CSGO()
        { }
    }
}
