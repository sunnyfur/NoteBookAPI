
using NotebookClient.Model;
using NotebookClient.View;
using System;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotebookClient.ViewModel
{
    public class MainWindowViewModel:BaseCl
    {
        const string _baseAddress = "http://localhost:54619/";
        RelayCommand addCommand;
        RelayCommand editCommand;
        RelayCommand deleteCommand;
        RelayCommand updateCommand;
        RelayCommand getContactsCommand;
        RelayCommand addContactCommand;
        RelayCommand editContactCommand;
        RelayCommand deleteContactCommand;


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
        ObservableCollection<Contact> contacts;
        public ObservableCollection<Contact> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                OnPropertyChanged("Contacts");
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

        Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged("SelectedContact");
            }
        }


        public MainWindowViewModel()
        {
           
           
        }

        #region Commands
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
        private void GetContacts()
        {
            if (SelectedPerson == null) { Contacts = null; return; }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                response = client.GetAsync("api/Contacts/" + SelectedPerson.Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    Contacts = response.Content.ReadAsAsync<ObservableCollection<Contact>>().Result;
                }
            }
        }
        public RelayCommand GetContactsCommand
        {
            get
            {
                return getContactsCommand ??
                (getContactsCommand = new RelayCommand((o) =>
                {
                    
                    GetContacts();

                }));
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
                    GetContacts();
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
        public RelayCommand AddContactCommand
        {
            get
            {
                return addContactCommand ??
                (addContactCommand = new RelayCommand((o) =>
                {
                    if (SelectedPerson == null) return;
                    ContactView  contactView = new ContactView();
                    ContactViewModel contactViewModel = new ContactViewModel(new Contact (), _baseAddress);
                    contactView.DataContext = contactViewModel;
                    if (contactView.ShowDialog() == true)
                    {
                        Contact contact = contactViewModel.Contact;
                        contact.PersonId = SelectedPerson.Id;
                        using (var client = new HttpClient())

                        {
                            client.BaseAddress = new Uri(_baseAddress);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PostAsJsonAsync("api/Contacts", contact).Result;
                        }
                        Update();
                    }
                }));
            }
        }
        public RelayCommand EditContactCommand
        {
            get
            {
                return editContactCommand ??
                (editContactCommand = new RelayCommand((SelectedContact) =>
                {
                    if (SelectedContact == null) return;
                    Contact contact = (Contact)SelectedContact;
                    ContactView contactView = new ContactView();
                    ContactViewModel contactViewModel = new ContactViewModel( contact, _baseAddress);
                    contactView.DataContext = contactViewModel;
                    
                    if (contactView.ShowDialog() == true)
                    {
                         contact = contactViewModel.Contact;
                        contact.ContactTypeId = contact.ContactType.Id; 
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(_baseAddress);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            HttpResponseMessage response = client.PutAsJsonAsync("api/Contacts/" + contact.Id, contact).Result;
                        }
                        Update();
                    }
                }));
            }
        }
        public RelayCommand DeleteContactCommand
        {
            get
            {
                return deleteContactCommand ??
                (deleteContactCommand = new RelayCommand((SelectedContact) =>
                {
                    if (SelectedContact == null) return;
                    Contact  contact = SelectedContact as Contact;
                    using (var client = new HttpClient())
                    {

                        client.BaseAddress = new Uri(_baseAddress);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = client.DeleteAsync("api/Contacts/" +contact.Id ).Result;
                    }
                    Update();

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
                            HttpResponseMessage response = client.PutAsJsonAsync("api/People/"+person.Id, person).Result;
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


        #endregion

    }
}
