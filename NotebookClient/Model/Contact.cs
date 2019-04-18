using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotebookClient.Model

{
    public class Contact : BaseEntity

    {
        public string Value { get; set; }
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }

    }

}