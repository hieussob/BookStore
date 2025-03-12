using BookStore.Application.DTOs;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Enum;
using BookStore.Domain.IRepositories;
using BookStore.Infrastructure.Data;

namespace BookStore.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly IAuthService _authService;
        public EmployeeRepository(DBBookContext context, IAuthService authService) : base(context)
        {
            _authService = authService;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            var lUser = new LoginUser
            {
                UserName = employee.Email,
                Password = "Abc123!@#"
            };

            if (await _authService.RegisterUser(lUser, role: Role.User.ToString()))
            {
                employee.Created = DateTime.Now;
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
