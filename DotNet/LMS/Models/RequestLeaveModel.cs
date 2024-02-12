
namespace LMS.Models
    {
         
        public class RequestLeaveModel
            {
                public int Requestid { get; set; }
                public int EmpId { get; set; }
                public string fromDate { get; set; }

                public string toDate { get; set; }
                
                public string leaveType { get; set; }

                 public string numberOfDays { get; set; }
                
                public string Description { get; set; }

                public string Status { get; set; }

                public string LeaveResult { get; set; }

    }
    }