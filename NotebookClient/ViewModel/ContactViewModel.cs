using NotebookClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotebookClient.ViewModel
{
    public class ContactViewModel : BaseCl
    {
        Contact  contact;
        List<ContactType> contactTypes;
        public IEnumerable<ContactTypeViewModel > ContactTypeViewModels { get; private set; }

        //public ObservableCollection<ContactType> ContactTypes
        //{
        //    get { return contactTypes; }
        //    set
        //    {
        //        contactTypes = value;
        //        OnPropertyChanged("ContactTypes");
        //    }
        //}
        string _baseAddress;
        public Contact  Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnPropertyChanged("Contact");
            }
        }
        public ContactViewModel(Contact contact, string baseAddress)
        {
                _baseAddress = baseAddress;
            GetContactTypes();
            var contactTypeViewModels = new List<ContactTypeViewModel >();
            foreach (var contactType in contactTypes)
            {
                contactTypeViewModels.Add(new ContactTypeViewModel(contactType));
            }
            ContactTypeViewModels = contactTypeViewModels;
            Contact = contact;
       


        }

       
        public void GetContactTypes()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                response = client.GetAsync("api/ContactTypes").Result;
                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsAsync<List<ContactType>>().Result;

                    contactTypes = res;
                }
            }
        }
    }
}