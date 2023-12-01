using LoopMatrix.DTOs;
using LoopMatrix.Enums;

namespace LoopMatrix.Helpers
{
    public class MatrixHelper
    {
        public static string GetMatrixPath()
        {
            var matrixPath = Console.ReadLine();

            return !string.IsNullOrEmpty(matrixPath) ? matrixPath : ProgramHelper.GetDefaultFilePath();
        }

        public static string[,]? BuildMatrix(string matrixText)
        {
            int i = 0;
            try
            {
                var rows = matrixText.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                if (!rows.Any())
                {
                    return null;
                }

                var columnsCount = rows.First().Split(',', StringSplitOptions.RemoveEmptyEntries)?.Length;
                var rowCount = rows.Length;
                if (!columnsCount.HasValue || columnsCount.Value != rowCount)
                {
                    return null;
                }

                string[,] matrix = new string[rows.Length, columnsCount.Value];
                foreach (var row in rows)
                {
                    int j = 0;
                    foreach (var col in row.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        matrix[i, j] = col.Trim();
                        j++;
                    }
                    i++;
                }

                return matrix;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static MaxMatchDTO FindLongestChainOnMatrix(string[,] matrix)
        {
            var maxMatchDTO = new MaxMatchDTO();

            var rowCount = matrix.GetLength(0);
            var columnCount = matrix.GetLength(1);
            var dto = new DataCheckingDTO
            {
                MaxMatchDTO = maxMatchDTO,
                Matrix = matrix,
                RowCount = rowCount,
                ColumnCount = columnCount
            };

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    dto.IValue = i;
                    dto.JValue = j;
                    dto.Value = matrix[i, j];

                    // horizontal checking
                    FindLongestHorizontalChainOnSpecificRow(dto);

                    // vertical checking
                    FindLongestVerticalChainOnSpecificColumn(dto);

                    // diagonal up checking
                    FindLongestDiagonalUpChain(dto);

                    // diagonal down checking
                    FindLongestDiagonalDownChain(dto);
                }
            }

            return maxMatchDTO;
        }

        private static void FindLongestDiagonalDownChain(DataCheckingDTO dto)
        {
            var colIndex = dto.JValue + 1;
            var matchCount = 0;
            for (int x = dto.IValue + 1; x < dto.RowCount; x++)
            {
                if (colIndex >= dto.ColumnCount || dto.Value != dto.Matrix[x, colIndex])
                {
                    break;
                }
                matchCount++;
                colIndex++;
            }

            UpdateMaxMatchDTOIfNecessary(dto, matchCount, ArrayDirectionEnum.DiagonalDown);
        }

        private static void FindLongestDiagonalUpChain(DataCheckingDTO dto)
        {
            var colIndex = dto.JValue + 1;
            var matchCount = 0;
            if (dto.IValue > 0)
            {
                for (int x = dto.IValue - 1; x >= 0; x--)
                {
                    if (colIndex >= dto.ColumnCount || dto.Value != dto.Matrix[x, colIndex])
                    {
                        break;
                    }
                    matchCount++;
                    colIndex++;
                }

                UpdateMaxMatchDTOIfNecessary(dto, matchCount, ArrayDirectionEnum.DiagonalUp);
            }
        }

        private static void FindLongestVerticalChainOnSpecificColumn(DataCheckingDTO dto)
        {
            var startIndex = dto.IValue + 1;
            var matchCount = 0;
            var column = Enumerable.Range(startIndex, dto.ColumnCount - startIndex).Select(x => dto.Matrix[x, dto.JValue]).ToArray();
            for (int k = 0; k < column.Length; k++)
            {
                if (dto.Value != column[k])
                {
                    break;
                }
                matchCount++;
            }

            UpdateMaxMatchDTOIfNecessary(dto, matchCount, ArrayDirectionEnum.Vertical);
        }

        private static void FindLongestHorizontalChainOnSpecificRow(DataCheckingDTO dto)
        {
            var startIndex = dto.JValue + 1;
            var matchCount = 0;
            var row = Enumerable.Range(startIndex, dto.RowCount - startIndex).Select(x => dto.Matrix[dto.IValue, x]).ToArray();
            for (int k = 0; k < row.Length; k++)
            {
                if (dto.Value != row[k])
                {
                    break;
                }
                matchCount++;
            }

            UpdateMaxMatchDTOIfNecessary(dto, matchCount, ArrayDirectionEnum.Horizontal);
        }

        private static void UpdateMaxMatchDTOIfNecessary(DataCheckingDTO dto, int matchCount, ArrayDirectionEnum arrayDirection)
        {
            if (matchCount > dto.MaxMatchDTO.Count)
            {
                dto.MaxMatchDTO.Count = matchCount;
                dto.MaxMatchDTO.Value = dto.Value;
                dto.MaxMatchDTO.ArrayDirection = arrayDirection;
                dto.MaxMatchDTO.XValue = dto.IValue;
                dto.MaxMatchDTO.YValue = dto.JValue;
            }
        }
    }
}