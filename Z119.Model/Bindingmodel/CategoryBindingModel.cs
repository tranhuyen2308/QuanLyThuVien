using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Model.Bindingmodel
{
    public class CategoryBindingModel : BaseBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecializedId { get; set; }
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecializedId { get; set; }
        public string Specialized { get; set; }
    }
}
