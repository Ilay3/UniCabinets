using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCabinet.Core.Models.ViewModel.Department;

public class DepartmentAddEditVM
{
    public int? Id { get; set; }
    public string DepartmentName { get; set; }
    public int? FacultyId { get; set; }
    public string FacultyName { get; set; }
}

