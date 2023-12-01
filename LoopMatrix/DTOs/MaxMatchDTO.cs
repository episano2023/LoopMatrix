using LoopMatrix.Enums;

namespace LoopMatrix.DTOs
{
    public class MaxMatchDTO
    {
        public string Value { get; set; }
        public int Count { get; set; }
        public int XValue { get; set; }
        public int YValue { get; set; }
        public ArrayDirectionEnum ArrayDirection { get; set; }
    }
}