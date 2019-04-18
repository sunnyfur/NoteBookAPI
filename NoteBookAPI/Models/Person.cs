using Newtonsoft.Json;
using System;

using System.Collections.Generic;

namespace NotebookAPI.Models

{

    public class Person : BaseEntity

    {

        public Person()

        {

            Contacts = new List<Contact>();

        }

        public string Firstname { get; set; }

        public string Secondname { get; set; }

        public DateTime BirthDay { get; set; }

        [JsonIgnore]

        public virtual ICollection<Contact> Contacts { get; set; }

    }

}