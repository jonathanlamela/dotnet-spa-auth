namespace DotNetSpaAuth.Validators
{
    using DotNetSpaAuth.Dtos;
    using FluentValidation;

    public class SigninRequestValidator : AbstractValidator<SigninRequest>
    {
        public SigninRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Il campo email è obbligatorio")
                .EmailAddress().WithMessage("Inserisci un indirizzo email valido");

            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Il campo nome è obbligatorio")
                .MinimumLength(2).WithMessage("Il nome deve contenere almeno due caratteri");

            RuleFor(x => x.Lastname)
                .NotEmpty().WithMessage("Il campo cognome è obbligatorio")
                .MinimumLength(2).WithMessage("Il cognome deve contenere almeno due caratteri");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Il campo password è obbligatorio")
                .MinimumLength(8).WithMessage("La password deve essere lunga almeno 8 caratteri")
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")
                .WithMessage("La password deve contenere almeno una lettera maiuscola, un numero e un carattere speciale.");
        }
    }


}
