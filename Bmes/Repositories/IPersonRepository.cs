using System.Collections.Generic;
using Bmes.Models.Shared;

namespace Bmes.Repositories
{
    public interface IPersonRepository
    {
        Person FindPersonById(long id);
        IEnumerable<Person> GetAllPeople();
        void SavePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}
