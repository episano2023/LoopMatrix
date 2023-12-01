using LoopMatrix.CustomExtensions;
using LoopMatrix.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        Console.WriteLine("Enter the file path of the matrix (remember that the matrix must be a square, and each value must be separeted by a \",\")." +
            "In case you have not a file with a matrix, just press ENTER key and it will use the default matrix (placed in " +
            $"{ProgramHelper.GetDefaultFilePath()}");

        var matrixPath = MatrixHelper.GetMatrixPath();

        if (!File.Exists(matrixPath))
        {
            Console.WriteLine("File not found");
            return;
        }

        var matrixText = await File.ReadAllTextAsync(matrixPath);

        if (string.IsNullOrEmpty(matrixText))
        {
            Console.WriteLine("Matrix not found");
            return;
        }

        Console.WriteLine(matrixText + Environment.NewLine);

        var matrix = MatrixHelper.BuildMatrix(matrixText);
        if (matrix == null)
        {
            Console.WriteLine("Matrix is not valid. Remember it must have the same count of rows and columns. Also, each value must be separated by a \",\".");
            return;
        }

        var maxMatchDTO = MatrixHelper.FindLongestChainOnMatrix(matrix);

        if (maxMatchDTO.Count == 0)
        {
            Console.WriteLine("No character is repeated more than once");
            return;
        }

        Console.WriteLine($"" +
            $"The longest chain is: {string.Join(", ", Enumerable.Repeat(maxMatchDTO.Value, maxMatchDTO.Count + 1))}.{Environment.NewLine}" +
            $"The direction is {maxMatchDTO.ArrayDirection.GetDisplayName()} and the start position is: x = {maxMatchDTO.XValue}; y = {maxMatchDTO.YValue}"
        );
    }
}