using AgirSaglam.Model.Models;
using FluentValidation;

namespace AgirSaglam.Api.Code.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(k => k.UserName).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(k => k.UserName).EmailAddress().WithMessage("Hatalı email adresi");
            RuleFor(k => k.Password).Length(6, 15).WithMessage("Şifre en az 6, en çok 15 karakter olabilir");
            RuleFor(k => k.Password).Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
