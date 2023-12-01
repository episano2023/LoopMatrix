namespace LoopMatrix.Helpers
{
    public class ProgramHelper
    {
        public static string GetDefaultFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\matrix.txt");
        }
    }
}