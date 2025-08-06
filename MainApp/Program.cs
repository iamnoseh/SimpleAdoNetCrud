

using Domain.Models;
using Infrastructure.Services;


var studentService = new StudentService();
var newStudent = new Student
{
    FirstName = "Anvar",
    LastName = "Sasidov", 
    Birthday = new DateOnly(2000, 5, 15),
    Address = "Dangara",
    Level = 7
};

var res = studentService.CreateStudent(newStudent);
Console.WriteLine(res);


var students = studentService.GetAllStudents();
foreach (var student in students)
{
    Console.WriteLine(student.FirstName + " || " + student.LastName + " || " + student.Birthday + " || " + student.Address + " || " + student.Level);
}