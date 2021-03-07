using System.Collections.Generic;

namespace MoonSec
{
    static class ByteCodeCollection
    {
        static Dictionary<Bytecode, string> _List;
        public static Dictionary<Bytecode, string> GetList()
        {
            if (_List == null)
            {
                _List = new Dictionary<Bytecode, string>();
                _List.Add(Bytecode.Arabic, "Arabic");
                _List.Add(Bytecode.Letters1, "Letters1");
                _List.Add(Bytecode.Symbols1, "Symbols1");
                _List.Add(Bytecode.Russian, "Russian");
                _List.Add(Bytecode.Whitespace, "Whitespace");
                _List.Add(Bytecode.Chinese, "Chinese");
                _List.Add(Bytecode.Emoji, "Emoji");
            }
            return _List;
        }

        public static string GetBytecode(Bytecode bc)
        {
            return _List[bc];
        }
    }
}
