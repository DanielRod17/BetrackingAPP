﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BetrackingAPP.Models
{
    public class Reports
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AssignmentID { get; set; }
        public int ConsultorID { get; set; }
        public int Type_of_travel { get; set; }
        public int Flight { get; set; }
        public int State_From { get; set; }
        public int City_From { get; set; }
        public int From_Departure { get; set; }
        public DateTime? Arrival_Date { get; set; }
        public int State_To { get; set; }
        public int City_To { get; set; }
        public int To_Departure { get; set; }
        public int Return_State_From { get; set; }
        public int Return_City_From { get; set; }
        public int Return_State_To { get; set; }
        public int Return_City_To { get; set; }
        public int Seating_preference { get; set; }
        public DateTime? Hotel_Arrival_Date { get; set; }
        public DateTime? Hotel_Departure_Date { get; set; }
        public int Smoking { get; set; }
        public string Special_Needs { get; set; }
        public int Ground_Method { get; set; }
        public string Emeral_Club_Number { get; set; }
        public DateTime? Car_Rental_Pickup_Date { get; set; }
        public int Car_Rental_Time { get; set; }
        public int Car_Size_Preference { get; set; }
        public DateTime? Car_Rental_Return { get; set; }
        public int Car_Rental_Return_Time { get; set; }
        public string Reason_Request { get; set; }
        public int Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Emergency_Contact { get; set; }
        public string SalesForceID { get; set; }
        public string AssignmentName { get; set; }
        public int ExpensesQty { get; set; }
        public string StatusName { get; set; }
        public string ProjectName { get; set; }
        public string city_from_name { get; set; }
        public string state_from_name { get; set; }
        public string country_from_name { get; set; }
        public string city_to_name { get; set; }
        public string state_to_name { get; set; }
        public string country_to_name { get; set; }
        public string rcity_from_name { get; set; }
        public string rstate_from_name { get; set; }
        public string rcountry_from_name { get; set; }
        public string rcity_to_name { get; set; }
        public string rstate_to_name { get; set; }
        public string rcountry_to_name { get; set; }
        public Expense[] Expenses { get; set; }

        public File[] Files { get; set; }
    }

    public class Expense
    {
        public int ID { get; set; }
        public int TravelID { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public decimal Quantity { get; set; }
        public int Currency { get; set; }
        public int Billable { get; set; }
        public int Refundable { get; set; }
        public string Attachments { get; set; }
        public decimal DOF { get; set; }
        public string SalesForceID { get; set; }
        public string CategoryName { get; set; }
        public decimal ValorFinal { get; set; }
        public bool DescriptionOn { get; set; }
        public bool RefundableOn { get; set; }
        public bool MxOn { get; set; }
    }

    public class File
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Size { get; set; }
        public string URL { get; set; }

    }

}
