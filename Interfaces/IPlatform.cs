namespace MoonSec
{
    public interface IPlatform
    {
        string name { get; }
        string type { get; }

        string ToString() {
            return name;
        }
    }
}
