using System;
using System.Collections.Generic;
using System.Text;

namespace BetrackingAPP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Division { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Type { get; set; }
        public string Status { get; set; }
        public string LastLogin { get; set; }
        public string Title { get; set; }
        public int WorkCountry { get; set; }
        public int Gnr { get; set; }
        public int WorkState { get; set; }
        public int WorkCity { get; set; }
        public int Privilege { get; set; }
        public string Payroll { get; set; }
        public string Round_Up { get; set; }
        public Assignment[] Assignments { get; set; }

    }

    public class Assignment
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int Division { get; set; }
    }

}
