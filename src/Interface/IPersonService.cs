using Alfasoft.Models;

namespace Alfasoft.Interface
{
    public interface IPersonService
    {
        Task<List<Person>> GetAllPeopleAsync();
        Task DeletePersonAsync(int id);
        Task<Person> GetPersonByIdAsync(int id);
        Task SoftDeletePersonAsync(int id);
        Task AddOrUpdatePersonAsync(Person person);
    }
}
