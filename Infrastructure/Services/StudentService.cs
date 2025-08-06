using Domain.Models;
using Infrastructure.Interefaces;
using Npgsql;
using System.Data;

namespace Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly string _connectionString = "Server=localhost;Database=AdoNet-Practice-Db;Username=postgres;Password=12345;";

    public bool CreateStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            const string query = @"
                INSERT INTO Students (firstname, lastname, birthday, address, level) 
                VALUES (@firstname, @lastname, @birthday, @address, @level) 
                RETURNING Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", student.FirstName);
            command.Parameters.AddWithValue("@lastname", student.LastName);
            command.Parameters.AddWithValue("@birthday", student.Birthday);
            command.Parameters.AddWithValue("@address", student.Address );
            command.Parameters.AddWithValue("@level", student.Level);

            var newId = command.ExecuteScalar();
            student.Id = Convert.ToInt32(newId);
            if (newId == null)
            {
                Console.WriteLine("Something went wrong");
                return false;
            }
            Console.WriteLine("Student created successfully with id : " + newId);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }

    public bool UpdateStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = @"
                UPDATE Students 
                SET firstname = @firstname, lastname = @lastname, birthday = @birthday, address = @address, level = @level
                WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", student.Id);
            command.Parameters.AddWithValue("@firstname", student.FirstName);
            command.Parameters.AddWithValue("@lastname", student.LastName);
            command.Parameters.AddWithValue("@birthday", student.Birthday);
            command.Parameters.AddWithValue("@address", student.Address);
            command.Parameters.AddWithValue("@level", student.Level);

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Something went wrong");
                return false;
            }

            Console.WriteLine("Student updated successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Something went wrong");
        }
    }

    public bool DeleteStudent(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "DELETE FROM Students WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Something went wrong");
                return false;
            }

            Console.WriteLine("Student deleted successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return false;
        }
    }

    public Student GetStudentById(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "SELECT Id, firstname, lastname, birthday, address, level FROM Students WHERE Id = @Id;";

            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt32("Id"),
                    FirstName = reader.GetString("firstname"),
                    LastName = reader.GetString("lastname"),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime("birthday")),
                    Address =  reader.GetString("address"),
                    Level = reader.GetInt32("level")
                };
            }

            Console.WriteLine("Student Not Found");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Some thing went  wrong");
        }
    }

    public List<Student> GetAllStudents()
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            const string query = "SELECT Id, firstname, lastname, birthday, address, level FROM Students ORDER BY Id;";

            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            var students = new List<Student>();

            while (reader.Read())
            {
                var student = new Student
                {
                    Id = reader.GetInt32("Id"),
                    FirstName = reader.GetString("firstname"),
                    LastName = reader.GetString("lastname"),
                    Birthday = DateOnly.FromDateTime(reader.GetDateTime("birthday")),
                    Address = reader.GetString("address"),
                    Level = reader.GetInt32("level")
                };

                students.Add(student);
            }

            if (students.Count == 0)
            {
                throw new Exception("Students Not Found");
            }
            
            Console.WriteLine($"{students.Count} - Student found!");
            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" {ex.Message}");
            throw new Exception("Some thing went  wrong");
        }
    }
}