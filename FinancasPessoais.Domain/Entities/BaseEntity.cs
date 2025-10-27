using System;

namespace FinancasPessoais.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreationDate { get; private set; } = DateTime.Now;
    }
}
