using System.Collections.Generic;
using RetroClash.Files.CsvHelpers;
using RetroClash.Logic.Enums;

namespace RetroClash.Files.CsvReader
{
    public class Gamefiles
    {
        private readonly List<DataTable> _dataTables = new List<DataTable>();

        public Gamefiles()
        {
            if (Csv.Gamefiles.Count <= 0) return;
            for (var i = 0; i < Csv.Gamefiles.Count; i++)
                _dataTables.Add(new DataTable());
        }

        public DataTable Get(Gamefile index)
        {
            return _dataTables[(int)index - 1];
        }

        public DataTable Get(int index)
        {
            return _dataTables[index - 1];
        }

        public void Initialize(Table table, int index)
        {
            _dataTables[index] = new DataTable(table, index);
        }

        public void Dispose()
        {
            _dataTables.Clear();
        }
    }
}
