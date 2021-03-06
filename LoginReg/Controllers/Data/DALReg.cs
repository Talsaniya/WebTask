﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginReg.Models;

namespace LoginReg.Controllers.Data
{
    public class DALReg : IDALReg
    {
      //  SqlConnection cn;
        //SqlCommand cmd;
        private String str;

       // private SqlCommand cmd = null;
       
        private SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=web;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");

        public string addRecord(User details)
        {

            string msg = string.Empty;
            try
            {
                conn.Open();
                str = "select count(*) FROM [Users] Where FirstName='" + details.FirstName + "' ";
                SqlCommand cmd = new SqlCommand(str, conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());


                if (count > 0)
                {
                    msg = "Duplicate";
                    return msg;
                }
                else
                {
                    // int len = Upload.PostedFile.ContentLength;
                    // byte[] pic = new byte[len + 1];
                    //  Upload.PostedFile.InputStream.Read(pic, 0, len);

                    cmd = new SqlCommand("insert into Users  (FirstName,LastName,Email,Mobile,Password,CreatedDate,LastLoginDate,Status) VALUES (@FirstName,@LastName,@Email,@Mobile,@Password,@CreatedDate,@LastLoginDate,@Status)", conn);

                    //cmd.Parameters.AddWithValue("UserId", Id);
                    cmd.Parameters.AddWithValue("@FirstName", details.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", details.LastName);
                    cmd.Parameters.AddWithValue("@Email", details.Email);
                    cmd.Parameters.AddWithValue("@Mobile", details.Mobile);
                    cmd.Parameters.AddWithValue("@Password", details.Password);

                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@LastLoginDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@Status", "Active");
                    //  cmd.Parameters.AddWithValue("@Avatar", "Active");
                    //  cmd.Parameters.AddWithValue("@Avatar", (pic != null && pic.Length > 0) ? pic : null);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    msg = "User Details Inserted Successfully!";

                    //    Response.Redirect("Login.aspx");
                }
                return msg;
            }
            catch (Exception)
            {
                msg = "Error!!!";
                return msg;
            }




        }

    }
}
