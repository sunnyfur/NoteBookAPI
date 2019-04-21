using NotebookClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotebookClient.ViewModel
{
    public class PersonVeiwModel: BaseCl
    {
        Person person;
        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged("Person");
            }
        }
       public PersonVeiwModel( Person person)
        {
            Person = person;
        }
    }
}
