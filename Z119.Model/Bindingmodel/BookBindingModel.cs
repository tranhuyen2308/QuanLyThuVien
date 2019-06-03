using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Model.Bindingmodel
{
    public class BookBindingModel 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string ReleaseYear { get; set; }
        public int? PageCount { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
        public string Note { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public int SpecialCategoryId { get; set; }
        public int BookShelfId { get; set; }
        public string Status { get; set; }
    }

    public class BookBindingViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string ReleaseYear { get; set; }
        public int? PageCount { get; set; }
        public int? AuthorId { get; set; }
        public string Author { get; set; }
        public int? PublisherId { get; set; }
        public string Publisher { get; set; }
        public string Note { get; set; }
        public string Price { get; set; }
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public int? SpecialCategoryId { get; set; }
        public string SpecialCategory { get; set; }
        public int? BookShelfId { get; set; }
        public string BookShelf { get; set; }
        public string Status { get; set; }
    }
}
