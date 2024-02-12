namespace LMS.Models
    {
         
        public class LoginModel
            {
                public string Username { get; set; }

                public int EmpId { get; set; }
                
                public int ManId { get; set; }

                 public string SickLeave { get; set; }
                
                public string PersonalLeave { get; set; }

                public string Password { get; set; }

                public string RePassword  {get; set;}

            }
    }