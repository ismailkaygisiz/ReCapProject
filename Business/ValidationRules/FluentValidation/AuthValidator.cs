using Core.Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator : AbstractValidator<UserForRegisterDto>
    {
        public AuthValidator()
        {
            RuleFor(a => a.Email).NotEmpty().NotNull().EmailAddress();

            RuleFor(a => a.FirstName).NotEmpty().NotNull();

            RuleFor(a => a.LastName).NotEmpty().NotNull();

            RuleFor(a => a.Password).NotEmpty().NotNull();
            RuleFor(a => a.Password).MinimumLength(8).WithMessage("Şifre En Az 8 Karakter Olmalıdır");
            RuleFor(a => a.Password).Must(UpperChar).WithMessage("Şifre En Az Bir Adet Büyük Harf İçermelidir");
            RuleFor(a => a.Password).Must(LowerChar).WithMessage("Şifre En Az Bir Adet Küçük Harf İçermelidir");
            RuleFor(a => a.Password).Must(NumberChar).WithMessage("Şifre En Az Bir Adet Sayı İçermelidir");
            // RuleFor(a => a.Password).Must(SpecialChar).WithMessage("Şifre En Az Bir Adet Özel Karakter İçermelidir");

        }

        private bool UpperChar(string arg)
        {
            char[] upperCaseChacters = new char[]
            {
                'Q','W','E','R','T','Y','U','I',
                'O','P','Ğ','Ü','A','S','D','F',
                'G','H','J','K','L','Ş','İ','Z',
                'X','C','V','B','N','M','Ö','Ç',
            };

            return ForController(arg, upperCaseChacters);
        }

        private bool LowerChar(string arg)
        {
            char[] lowerCaseChacters = new char[]
            {
                'q','w','e','r','t','y','u','ı',
                'o','p','ğ','ü','a','s','d','f',
                'g','h','j','k','l','ş','i','z',
                'x','c','v','b','n','m','ö','ç',
            };

            return ForController(arg, lowerCaseChacters);
        }

        private bool NumberChar(string arg)
        {
            char[] numberCharacters = new char[]
            {
                '0','1','2','3','4',
                '5','6','7','8','9',
            };

            return ForController(arg, numberCharacters);
        }


        private bool SpecialChar(string arg)
        {
            char[] specialCharacters = new char[]
            {
                '@', '/', '\\', '_', '-','!','^',
                '+','$','%','½','&','{','}',
                '[',']','(',')','=','?','*',
                '€',';',':',',','.','<','>',
                '"','\'','#'
            };

            return ForController(arg, specialCharacters);
        }


        private bool ForController(string arg, char[] chars)
        {
            foreach (char i in arg)
            {
                foreach (char j in chars)
                {
                    if (i == j)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
