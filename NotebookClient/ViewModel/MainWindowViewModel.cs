
using NotebookClient.Model;
using NotebookClient.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotebookClient.ViewModel
{
    public class MainWindowViewModel:BaseCl
    {
        const string _baseAddress = "http://localhost:54619/";
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        RelayCommand updateCommand;

        ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                OnPropertyChanged("People");
            }
        }
        Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson;}
            set
            {
                selectedPerson = value;
                OnPropertyChanged("SelectedPerson");
            }
        }


        public MainWindowViewModel()
        {
           
           
        }
        public void Update()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                response = client.GetAsync("api/People").Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsAsync<ObservableCollection<Person>>().Result;

                    People = res;
                }
            }
        }
            
         public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                (updateCommand = new RelayCommand((o) =>
                {
                    Update();

                }));
            }
        }
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new RelayCommand((o) =>
                {
                    PersonView personView = new PersonView();
                    PersonVeiwModel personViewModel  = new PersonVeiwModel(new Person ());
                    personView.DataContext = personViewModel;
                    if (personView.ShowDialog() == true)
                    {
                        Person  person  = personViewModel.Person;

                        using (var client = new HttpClient())

                        {

                            client.BaseAddress = new Uri(_baseAddress);

                            client.DefaultRequestHeaders.Accept.Clear();

                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = client.PostAsJsonAsync("api/People", person).Result;

                        }
                        Update();

                    }

                }));
            }
        }
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                (editCommand = new RelayCommand((SelectedPerson) =>
                {
                    if (SelectedPerson == null) return;
                    Person person = SelectedPerson as Person;
                    PersonView personView = new PersonView();
                    PersonVeiwModel personViewModel = new PersonVeiwModel(person);
                    personView.DataContext = personViewModel;
                    if (personView.ShowDialog() == true)
                    {
                        person = personViewModel.Person;

                        using (var client = new HttpClient())

                        {

                            client.BaseAddress = new Uri(_baseAddress);

                            client.DefaultRequestHeaders.Accept.Clear();

                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = client.PutAsJsonAsync("api/People"+person.Id, person).Result;

                        }
                        Update();

                    }

                }));
            }
        }
        private void Delete(int delete)

        {

            using (var client = new HttpClient())

            {
               
                client.BaseAddress = new Uri(_baseAddress);

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync("api/People/" + delete).Result;

            }

        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                (deleteCommand = new RelayCommand((SelectedPerson) =>
                {
                    if (SelectedPerson == null) return;
                    Person person = SelectedPerson as Person;
                    Delete(person.Id);   
                    Update();

                  

                }));
            }
        }




    }
}
