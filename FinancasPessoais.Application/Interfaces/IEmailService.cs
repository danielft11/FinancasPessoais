using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Application.Interfaces
{
    public interface IEmailService
    {
        void SendAccountPayableEmail(AccountPayable accountPayable);
        void SendResetPasswordEmail(string email, string callbackUrl);
    }
}
