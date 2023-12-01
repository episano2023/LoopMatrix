namespace LoopMatrix.DTOs
{
    public class DataCheckingDTO
    {
        public MaxMatchDTO MaxMatchDTO { get; set; }
        public string[,] Matrix { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
        public int IValue { get; set; }
        public int JValue { get; set; }
        public string Value { get; set; }
    }
}