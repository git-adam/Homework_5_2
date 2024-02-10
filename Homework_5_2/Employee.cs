using System;

namespace Homework_5_2
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? DismissDate { get; set; }
        public int VacationDaysNumber { get; set; }
        public string Comments { get; set; }

        //Pozycja w firmie, adres
    }
}
