using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.Model.Bindingmodel
{
    public class ReaderBindingModel : BaseBindingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public int PositionId { get; set; }
        public int UnitId { get; set; }
        public string Note { get; set; }
    }

    public class ReaderBindingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DayOfBirth { get; set; }
        public string Position { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
        public int? PositionId { get; set; }
        public int? UnitId { get; set; }
    }

    public class ReaderBindingforBorrower
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
