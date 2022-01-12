using AsyncAwaitBlazor.Context;
using AsyncAwaitBlazor.Interfaces;
using AsyncAwaitBlazor.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwaitBlazor.Controller
{
    public class PersonController : IPersonController
    {
        private readonly PersonContext _context;

        public PersonController(PersonContext  context)
        {
            _context = context;
        }

        public async Task AddPerson(string name, string surname, int age)
        {
            var person = new Person()
            {
                Name = name,
                Surname = surname,
                Age = age
            };

            _context.Person.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task ChangePerson(Person changePerson)
        {
            //var person = GetAllPerson();

            var person = _context.Person.SingleOrDefault(x => x.Id == changePerson.Id);
            //var result = person.SingleOrDefault(x => x.Id == changePerson.Id);

            person.Name = changePerson.Name;
            person.Surname = changePerson.Surname;
            person.Age = changePerson.Age;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if(person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Person>> GetAllPerson()
        {
            return await _context.Person.ToListAsync();
        }
    }
}
