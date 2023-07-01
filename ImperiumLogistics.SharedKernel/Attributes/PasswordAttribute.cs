namespace ImperiumLogistics.SharedKernel.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            string password = value.ToString();

            // Add your password validation rules here
            if (password.Length < 8 || password.Length > 20)
                return false;

            if (!ContainsUpperCase(password))
                return false;

            if (!ContainsLowerCase(password))
                return false;

            if (!ContainsDigit(password))
                return false;

            if (!ContainsSpecialCharacter(password))
                return false;

            return true;
        }

        private bool ContainsUpperCase(string value)
        {
            return value.Any(char.IsUpper);
        }

        private bool ContainsLowerCase(string value)
        {
            return value.Any(char.IsLower);
        }

        private bool ContainsDigit(string value)
        {
            return value.Any(char.IsDigit);
        }

        private bool ContainsSpecialCharacter(string value)
        {
            return value.Any(c => !char.IsLetterOrDigit(c));
        }
    }

}
