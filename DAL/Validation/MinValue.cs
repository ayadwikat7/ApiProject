using System.ComponentModel.DataAnnotations;

namespace KASHPE.PL.Validation
{
    public class MinValue : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int val)
            {
                if (val > 10)
                    return true;
            }

            return false;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{name} is invalid";
        }

    }
}
