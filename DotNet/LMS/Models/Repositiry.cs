using System.Data.SqlClient;
using System.Data;
using LMS.Models;
using System.Collections;

namespace LMS.Controllers;

public class Repository
    {
        public static List<LoginModel> adminList = new List<LoginModel>();

        public static List<LeaveTableModel> leaveTableList = new List<LeaveTableModel>();



        public static List<LoginModel> GetAdminDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                Console.WriteLine("Entered Datebase");
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("select * from AdminDetails",connection);
                SqlDataReader sqlReader=sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    LoginModel admin = new LoginModel();
                    admin.Username=sqlReader["UserName"].ToString();
                    admin.Password=sqlReader["Password"].ToString();
                    
                    adminList.Add(admin);                   
                }
                }                
            } 

             catch (SqlException exception)
            {                  
                Console.WriteLine("Datebase error"+exception);               
            }
            return adminList;
        }

        public static Tuple<int,string> AdminLogin(LoginModel admin)
        {
            string userName = admin.Username;
            string password = admin.Password;
            List<LoginModel> adminList = GetAdminDetails();
            foreach(LoginModel item in adminList)
            {
                if(String.Equals(userName,item.Username) && String.Equals(password,item.Password))
                {
                    int check = 1;
                    return Tuple.Create(check,userName);
                }
            }

            return Tuple.Create(2,"");
        }

         public static int AddAdmin(LoginModel admin)
        {
            bool userNameflag = true;

            List<LoginModel> adminList = GetAdminDetails();

            foreach(LoginModel item in adminList)
            {
                if(String.Equals(admin.Username,item.Username))
                {
                    userNameflag = false;
                }
            }

            if(userNameflag == true)
            {
                if(String.Equals(admin.Password,admin.RePassword))
                {
                        using(SqlConnection connection = new SqlConnection(getConnection()))
                            {
                                connection.Open();
                                
                                SqlCommand insertCommand = new SqlCommand("Insert into AdminDetails (UserName,Password) values('"+admin.Username+"','"+admin.Password+"');",connection);

                                insertCommand.ExecuteNonQuery();
                            }
                            System.Console.WriteLine("inserted");
                            return 4;
                }
                System.Console.WriteLine("Password Wrong");
                return 2;
            }
            System.Console.WriteLine("UserName Exist");
            return 1;
        }

        public static List<LoginModel> GetEmployeeDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                Console.WriteLine("Entered Datebase");
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("select * from EmployeeDetails",connection);
                SqlDataReader sqlReader=sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    LoginModel admin = new LoginModel();
                    admin.EmpId = (int)sqlReader["EmployeeId"];
                    admin.Username=sqlReader["Username"].ToString();
                    admin.Password=sqlReader["Password"].ToString();
                    admin.ManId= (int)sqlReader["ManagerId"];
                    
                    adminList.Add(admin);                   
                }
                }                
            } 

             catch (SqlException exception)
            {                  
                Console.WriteLine("Datebase error"+exception);               
            }
            return adminList;
        }

        public static int AddEmployee(LoginModel Employee)
        {

            bool userNameflag = true;
            bool empIdflag = true;

            List<LoginModel> adminList = GetAdminDetails();

            foreach(LoginModel item in adminList)
            {
                if(String.Equals(Employee.Username,item.Username))
                {
                    userNameflag = false;
                }
            }

            foreach(LoginModel item in adminList)
            {
                    if(Equals(Employee.EmpId,item.EmpId))
                    {
                        empIdflag = false;
                    }
            }

            if(userNameflag && empIdflag == true)
            {
                if(String.Equals(Employee.Password,Employee.RePassword))
                {
                        using(SqlConnection connection = new SqlConnection(getConnection()))
                            {
                                connection.Open();
                                
                                SqlCommand insertCommand = new SqlCommand("Insert into EmployeeDetails (EmployeeId,UserName,Password,ManagerId) values('"+Employee.EmpId+"','"+Employee.Username+"','"+Employee.Password+"','"+Employee.ManId+"');",connection);

                                insertCommand.ExecuteNonQuery();
                            }
                            System.Console.WriteLine("inserted");
                            return 4;
                }
                System.Console.WriteLine("Password Wrong");
                return 2;
            }
            System.Console.WriteLine("UserName or Employee ID Exist");
            return 1;
        }

        public bool IsValidManager(LoginModel employee)
        {
                int result =0;
                SqlConnection connection = new SqlConnection(getConnection());
                SqlCommand command =connection.CreateCommand();
                command.CommandType=CommandType.StoredProcedure;
                command.CommandText="isvalidmanager";
                command.Parameters.AddWithValue("@ManagerName",employee.Username);
                command.Parameters.AddWithValue("@Password",employee.Password);
                connection.Open();
                var reader = command.ExecuteReader();   
                if (reader.Read())
                {
                    result = (int)reader["Result"];
                }
                if(result == 1){
                    connection.Close();
                    return true;
                }                
                connection.Close();
                return false;            
        }

         public  bool IsValidEmployee(LoginModel employee)
        {
                int result =0;
                SqlConnection connection = new SqlConnection(getConnection());
                SqlCommand command =connection.CreateCommand();
                command.CommandType=CommandType.StoredProcedure;
                command.CommandText="isValidemployee";
                command.Parameters.AddWithValue("@EmployeeName",employee.Username);
                command.Parameters.AddWithValue("@Password",employee.Password);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = (int)reader["Result"];
                }
                if(result == 1){
                    connection.Close();
                    return true;
                }                
                connection.Close();
                return false;       
        }



        public static List<LoginModel> GetManagerDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(getConnection()))
                {
                Console.WriteLine("Entered Datebase");
                connection.Open();
                SqlCommand sqlCommand=new SqlCommand("select * from ManagerDetails",connection);
                SqlDataReader sqlReader=sqlCommand.ExecuteReader();
                while (sqlReader.Read())
                {
                    LoginModel admin = new LoginModel();
                    admin.ManId = (int)sqlReader["ManagerId"];
                    admin.Username=sqlReader["Managername"].ToString();
                    admin.Password=sqlReader["Password"].ToString();;
                    
                    adminList.Add(admin);                   
                }
                }                
            } 

             catch (SqlException exception)
            {                  
                Console.WriteLine("Datebase error"+exception);               
            }
            return adminList;
        }

        public static int GetID(string userName){
             using(SqlConnection connection = new SqlConnection(getConnection()))
                {
                    connection.Open();
                    SqlCommand Cmnd = new SqlCommand($"SELECT EmployeeId FROM EmployeeDetails where Username='{userName}' ", connection);
                    int CountID = Convert.ToInt32(Cmnd.ExecuteScalar());
                    System.Console.WriteLine(CountID);
                    return CountID;
                }
        }

        public static int AddManager(LoginModel Man)
        {       

            bool userNameflag = true;
            bool empIdflag = true;

            List<LoginModel> adminList = GetManagerDetails();

            foreach(LoginModel item in adminList)
            {
                if(String.Equals(Man.Username,item.Username))
                {
                    userNameflag = false;
                }
            }

            foreach(LoginModel item in adminList)
            {
                    if(Equals(Man.ManId,item.ManId))
                    {
                        empIdflag = false;
                    }
            }

            if(userNameflag && empIdflag == true)
            {
                if(String.Equals(Man.Password,Man.RePassword))
                {
                        using(SqlConnection connection = new SqlConnection(getConnection()))
                            {
                                connection.Open();
                                
                                SqlCommand insertCommand = new SqlCommand("Insert into ManagerDetails (ManagerId,Managername,Password) values('"+Man.ManId+"','"+Man.Username+"','"+Man.Password+"');",connection);

                                insertCommand.ExecuteNonQuery();
                            }
                            System.Console.WriteLine("inserted");
                            return 4;
                }
                System.Console.WriteLine("Password Wrong");
                return 2;
            }
            System.Console.WriteLine("UserName or Manager ID Exist");
            return 1;
        }

        public static void RequestLeave(RequestLeaveModel request)
        {       

                        using(SqlConnection connection = new SqlConnection(getConnection()))
                            {
                                connection.Open();
                                
                                SqlCommand insertCommand = new SqlCommand("insert into RequestedLeaves(EmployeeId,FromDate,ToDate,LeaveType,NumberOfDays,Decision) values('"+request.EmpId+"','"+request.fromDate+"','"+request.toDate+"','"+request.leaveType+"','"+request.numberOfDays+"','"+request.Description+"');",connection);

                                insertCommand.ExecuteNonQuery();
                            }
                            System.Console.WriteLine("inserted Request Leaves");
  
        }

        public static List<RequestLeaveModel> GetRequestLeave()
        {
                using(SqlConnection connection = new SqlConnection(getConnection()))
                {
                List<RequestLeaveModel> userlist1= new List<RequestLeaveModel>();
                Console.WriteLine("Entered Datebase");
                connection.Open();

               SqlDataAdapter dataAdapter = new SqlDataAdapter("select*from RequestedLeaves",connection);
               DataTable dataTable = new DataTable();

              dataAdapter.Fill(dataTable);

            foreach (DataRow dr in dataTable.Rows)
            {
                RequestLeaveModel request = new RequestLeaveModel
                {
                    Requestid = Convert.ToInt32(dr["RequestId"]),
                    EmpId = Convert.ToInt32(dr["EmployeeId"]),
                    fromDate = dr["FromDate"].ToString(),
                    toDate = dr["ToDate"].ToString(),
                    leaveType = Convert.ToString(dr["LeaveType"]),
                    numberOfDays = dr["NumberOfDays"].ToString(),
                    Description = dr["Decision"].ToString(),
                    Status = Convert.ToString(dr["Status"])
                };


                userlist1.Add(request);                   
                }
                 return userlist1;
                }                
            
        }  

         public static int updateStatus(int id,string status)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(getConnection()))
                {
                    connection.Open();
                    SqlCommand updateCommand = new SqlCommand($"Update RequestedLeaves set Status='{status}' where RequestId={id}",connection);
                    updateCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch(SqlException exception)
            {
                System.Console.WriteLine(exception.Message);
                return 2;
            }
        }


        public static string? getConnection()
        {
        var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json",optional:true,reloadOnChange:true);
        IConfiguration configuration = build.Build();
        string? connectionString = Convert.ToString(configuration.GetConnectionString("DefaultConnection"));
        return connectionString;
        }
    }


