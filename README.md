# AdoDbConnection.Net
with this project, you can embed database connections as ADO in the .Net projects rapidly.

-----------------------------------
steps:
  1- add project into solution. 
  2- add AdoDbConnection refrence in your Class by :
  
            using AdoDbConnection;
			
  3- set database connection string.
  
            DBConnection.ServerName = ".";
            DBConnection.DataBbaseName = "test";
            DBConnection.Username = "user";
            DBConnection.Password = "Pass";
			
  4- create object and use:
  
            DBConnection dB = new DBConnection();
            DataSet dataSet = new DataSet();
            dataSet = dB.RunAndGet("select 1");
