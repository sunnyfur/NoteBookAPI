using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotebookClient.Model

{
    public class Contact : BaseEntity

    {
        string value;
        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }
        public int? PersonId { get; set; }

        public Person Person { get; set; }


        public int? ContactTypeId { get; set; }


        public virtual ContactType ContactType { get; set; }

    }

}