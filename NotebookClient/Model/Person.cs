using Newtonsoft.Json;
using System;

using System.Collections.Generic;

namespace NotebookClient.Model

{

    public class Person : BaseEntity

    {

        public Person()

        {

            Contacts = new List<Contact>();

        }

        string firstName;
        public string Firstname
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        string secondName;
        public string Secondname
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }

        DateTime birthDay;

        public DateTime BirthDay
        {
            get { return birthDay; }
            set { birthDay = value;
                OnPropertyChanged("BirthDay");
            }
        }

        [JsonIgnore]

        public virtual ICollection<Contact> Contacts { get; set; }

    }

}