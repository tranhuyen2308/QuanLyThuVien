using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Model.Bindingmodel
{
    public class BorrowerBindingModel : BaseBindingModel
    {
        public int Id { get; set; }
        public int ReaderId { get; set; }
        public string BookId { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string Note { get; set; }
    }

    public class BorrowerBindingViewModel
    {
        public int ReaderId { get; set; }
        public string ReaderName { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public DateTime? BorrowDate { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public string Unit { get; set; }
        public string Position { get; set; }
        public int? UnitId { get; set; }
        public int? PositionId { get; set; }
    }
}
