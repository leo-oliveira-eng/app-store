using Catalog.Domain.Authors.Models;
using System.Threading.Tasks;

namespace Catalog.Domain.Authors.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
    }
}
