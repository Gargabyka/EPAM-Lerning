using AsyncAwaitBlazor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitBlazor.Interfaces
{
    public interface IPersonController
    {
        Task<List<Person>> GetAllPerson();

        Task AddPerson(string name, string surname, int age);
        Task ChangePerson(Person changePerson);
        Task DeletePerson(int id);
    }
}
