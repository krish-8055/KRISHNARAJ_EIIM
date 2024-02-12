namespace LMS.Models
    {
         
        public class LeaveTableModel
            {
                public int EmployeeId { get; set; }
                
                public int SickLeave { get; set; }

                public int PrevilageLeave { get; set; }

                public int AvailableLeave { get; set; }
    
                public int LeaveTaken { get; set; }

            }
    }