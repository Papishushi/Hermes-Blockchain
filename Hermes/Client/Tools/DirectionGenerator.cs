namespace Hermes.Client.Tools
{
    public static class DirectionGenerator
    {
        public const int LENGHT = 16;
        private static readonly char[] symbols = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                                                   'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public static string? CreateDirection()
        {
            byte[] b = new byte[LENGHT];
            Random.Shared.NextBytes(b);
            var i = BitConverter.ToUInt64(b);
            var d = $"HRM{LongToBase(new(symbols), i)}";
            if (d == null) return (null);
            var c = new char[LENGHT - d.Length];
            if (c == null) return (null);
            Array.Fill(c, '0');
            return d.Length < LENGHT ? d + new string(c) : d;
        }

        public static ulong GetHash(string direction) => BaseToLong(new(symbols), direction);

        static string? LongToBase(string? _base, ulong number) => _base?.Length > 1 ?
                                                                    number < (uint)_base.Length ?
                                                                      $"{_base[(int)number]}" :
                                                                      LongToBase(_base, number / (uint)_base.Length) +
                                                                      LongToBase(_base, number % (uint)_base.Length) :
                                                                    null;

        static ulong BaseToLong(string? _base, string? number, int i = 0) => _base?.Length > 1 && number?.Length > 0 ?
                                                                            i < number.Length ?
                                                                              (ulong)(_base.IndexOf(number[i]) * Pow(_base.Length, i)) + BaseToLong(_base, number, i + 1) :
                                                                              (ulong)(_base.IndexOf(number[^1]) * Pow(_base.Length, number.Length - 1)) :
                                                                            0;

        static int Pow(int nmbr, int p)
        {
            if (p == 0) return 1;
            var result = nmbr;
            for (var i = 1; i < p; i++) result *= nmbr;
            return result;
        }
    }
}