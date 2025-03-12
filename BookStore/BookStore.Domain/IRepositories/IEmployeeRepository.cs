using BookStore.Domain.Entities;
using BookStore.Domain.Repositories;

namespace BookStore.Domain.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<bool> CreateEmployee(Employee employee);
    }
}
