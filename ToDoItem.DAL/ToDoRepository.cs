using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ToDoItem.Domain.Abstractions;
using ToDoItem.Domain.Models;


namespace ToDoItem.DAL
{
    public class ToDoRepository : IToDoItemRepository
    {
        private readonly ToDoConfiguration _options;

        public ToDoRepository(IOptions<ToDoConfiguration> options)
        {
            _options = options.Value;
        }

        public int AddToDoItem(ToDoData toDoData)
        {
            string script = @"
                            INSERT INTO ToDo(Name,IsCompleted)
                            VALUES(@name,@isCompleted);
                            SELECT CAST(scope_identity() AS int)";
            int paymentDetailId = 0;

            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(script, conn))
                {
                    comm.Parameters.AddWithValue("@name", toDoData.Name);
                    comm.Parameters.AddWithValue("@isCompleted", toDoData.IsCompleted);                    
                    paymentDetailId = (int)comm.ExecuteScalar();
                    conn.Close();
                }
            }
            return paymentDetailId;
        }

        public bool DeleteToDoItem(int id)
        {
            throw new NotImplementedException();
        }

        public List<ToDoData> GetLists()
        {
            List<ToDoData> toDoData = new List<ToDoData>();
            string script = @"SELECT ListId,Name,IsCompleted FROM ToDo";

            DataTable dataTable = new DataTable();
            SqlDataReader reader;
            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(script, conn))
                {
                    reader = comm.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                    conn.Close();
                }
            }

            toDoData = GenerateClassObject(dataTable);
            return toDoData;
        }

        public bool UpdateToDoItem(ToDoData toDoData)
        {
            string script = @"UPDATE ToDo SET Name=@name,IsCompleted=@isCompleted                                
                                WHERE ListId=@id";
            int numberOfRowsAffected = 0;

            using (SqlConnection conn = new SqlConnection(_options.ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(script, conn))
                {
                    comm.Parameters.AddWithValue("@id", toDoData.ListId);
                    comm.Parameters.AddWithValue("@name", toDoData.Name);
                    comm.Parameters.AddWithValue("@isCompleted", toDoData.IsCompleted);
                    numberOfRowsAffected = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return numberOfRowsAffected > 0 ? true : false;
        }

        private static List<ToDoData> GenerateClassObject(DataTable dataTable)
        {
            List<ToDoData> toDoDatas = new List<ToDoData>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    ToDoData toDoData = new ToDoData();
                    toDoData.ListId = dataTable.Columns.Contains("ListId")
                                                    ? Convert.ToInt32(row["ListId"]) : 0;
                    toDoData.IsCompleted = dataTable.Columns.Contains("IsCompleted")
                                                    ? Convert.ToBoolean(row["IsCompleted"]) : false;
                    toDoData.Name = dataTable.Columns.Contains("Name")
                                                    ? Convert.ToString(row["Name"]) : string.Empty;


                    toDoDatas.Add(toDoData);
                }
            }

            return toDoDatas.Count > 0 ? toDoDatas : new List<ToDoData>();
        }
        }
    }

