using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Student_Affairs.Models.StudentAffairViewModels
{
    public class AssignedSubjectData
    {
        public int SubjectID { get; set; }  
        public string SubjectName { get; set; }
        public bool Assigned { get; set; }  
    }
}
