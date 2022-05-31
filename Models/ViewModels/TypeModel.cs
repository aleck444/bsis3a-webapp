using System.Collections.Generic;
using aleck3a_webapp.Models;


namespace aleck3a_webapp.Models.ViewModels
{
    public class TypeModel
    {
        public Type Type { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}