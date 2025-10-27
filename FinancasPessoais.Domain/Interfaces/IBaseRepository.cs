using FinancasPessoais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> RemoveAsync(TEntity entity);
    }

}
