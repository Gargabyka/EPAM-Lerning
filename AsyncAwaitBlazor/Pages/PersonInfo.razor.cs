using AsyncAwaitBlazor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncAwaitBlazor.Pages
{
    public partial class PersonInfo
    {
        private List<Person> person = new List<Person>();

        private int Id;
        private string Name;
        private string Surname;
        private int Age;


        protected async override Task OnInitializedAsync()
        {
            GetAllPerson();
        }

        private async void GetAllPerson()
        {
            person.Clear();

            person = await controller.GetAllPerson();

            StateHasChanged();
        }

        private void UpdateCreatePerson()
        {
            if (Id == 0)
            {
                AddPerson();
                ClearField();
            }
            if (Id != 0)
            {
                UpdatePerson();
                ClearField();
            }
        }


        private async Task AddPerson()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Surname) && Age != 0)
            {
                await controller.AddPerson(Name, Surname, Age);
                ClearField();
                GetAllPerson();
            }
        }

        private async Task UpdatePerson()
        {
            var person = new Person()
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            await controller.ChangePerson(person);

        }

        private void CompletionFields(int id, string name, string surname, int age)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
        }

        private async Task DeletePerson(int id)
        {
            await controller.DeletePerson(id);
            GetAllPerson();
        }

        private void ClearField()
        {
            Id = 0;
            Name = string.Empty;
            Surname = string.Empty;
            Age = 0;
        }
    }
}
