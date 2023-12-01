using System.ComponentModel.DataAnnotations;

namespace LoopMatrix.Enums
{
    public enum ArrayDirectionEnum
    {
        [Display(Name = "Horizontal")]
        Horizontal = 1,

        [Display(Name = "Vertical")]
        Vertical = 2,

        [Display(Name = "Diagonal Up")]
        DiagonalUp = 3,

        [Display(Name = "Diagonal Down")]
        DiagonalDown = 4
    }
}