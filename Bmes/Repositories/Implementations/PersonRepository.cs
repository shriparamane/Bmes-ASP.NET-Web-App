using System.Collections.Generic;
using Bmes.Database;
using Bmes.Models.Shared;

namespace Bmes.Repositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private BmesDbContext _context;

        public PersonRepository(BmesDbContext context)
        {
            _context = context;
        }

        public Person FindPersonById(long id)
        {
            var note = _context.People.Find(id);
            return note;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            var notes = _context.People;
            return notes;
        }

        public void SavePerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            _context.People.Update(person);
            _context.SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }
    }
}
