using System;
using System.Collections.Generic;
using System.Text;

namespace BetrackingAPP.Models
{
    public class Timecard
    {
        public int ID { get; set; }
        public int AssignmentID { get; set; }
        public int ConsultorID { get; set; }
        public string TimecardID { get; set; }
        public decimal Mon { get; set; }
        public decimal Tue { get; set; }
        public decimal Wed { get; set; }
        public decimal Thu { get; set; }
        public decimal Fri { get; set; }
        public decimal Sat { get; set; }
        public decimal Sun { get; set; }
        public string MonNote { get; set; }
        public string TueNote { get; set; }
        public string WedNote { get; set; }
        public string ThuNote { get; set; }
        public string FriNote { get; set; }
        public string SatNote { get; set; }
        public string SunNote { get; set; }

        public string Mon_In { get; set; }
        public string Mon_Meal_In { get; set; }
        public string Mon_Meal_Out { get; set; }
        public string Mon_Break1_In { get; set; }
        public string Mon_Break1_Out { get; set; }
        public string Mon_Break2_In { get; set; }
        public string Mon_Break2_Out { get; set; }
        public string Mon_Out { get; set; }


        public string Tue_In { get; set; }
        public string Tue_Meal_In { get; set; }
        public string Tue_Meal_Out { get; set; }
        public string Tue_Break1_In { get; set; }
        public string Tue_Break1_Out { get; set; }
        public string Tue_Break2_In { get; set; }
        public string Tue_Break2_Out { get; set; }
        public string Tue_Out { get; set; }

        public string Wed_In { get; set; }
        public string Wed_Meal_In { get; set; }
        public string Wed_Meal_Out { get; set; }
        public string Wed_Break1_In { get; set; }
        public string Wed_Break1_Out { get; set; }
        public string Wed_Break2_In { get; set; }
        public string Wed_Break2_Out { get; set; }
        public string Wed_Out { get; set; }

        public string Thu_In { get; set; }
        public string Thu_Meal_In { get; set; }
        public string Thu_Meal_Out { get; set; }
        public string Thu_Break_Out { get; set; }
        public string Thu_Break1_In { get; set; }
        public string Thu_Break1_Out { get; set; }
        public string Thu_Break2_In { get; set; }
        public string Thu_Break2_Out { get; set; }
        public string Thu_Out { get; set; }

        public string Fri_In { get; set; }
        public string Fri_Meal_In { get; set; }
        public string Fri_Meal_Out { get; set; }
        public string Fri_Break1_In { get; set; }
        public string Fri_Break1_Out { get; set; }
        public string Fri_Break2_In { get; set; }
        public string Fri_Break2_Out { get; set; }
        public string Fri_Out { get; set; }

        public string Sat_In { get; set; }
        public string Sat_Meal_In { get; set; }
        public string Sat_Meal_Out { get; set; }
        public string Sat_Break1_In { get; set; }
        public string Sat_Break1_Out { get; set; }
        public string Sat_Break2_In { get; set; }
        public string Sat_Break2_Out { get; set; }
        public string Sat_Out { get; set; }

        public string Sun_In { get; set; }
        public string Sun_Meal_In { get; set; }
        public string Sun_Meal_Out { get; set; }
        public string Sun_Break1_In { get; set; }
        public string Sun_Break1_Out { get; set; }
        public string Sun_Break2_In { get; set; }
        public string Sun_Break2_Out { get; set; }
        public string Sun_Out { get; set; }

        public int Submitted { get; set; }
        public string StartingDay { get; set; }
        public string CreatedDate { get; set; }
        public string SubmitDate { get; set; }
        public string SalesForceDate { get; set; }
        public string LEditDate { get; set; }
        public string RejectedDate { get; set; }
        public string ApprovedDate { get; set; }
        public int Dailycount { get; set; }
        public string SalesForceID { get; set; }
        public int Created_By { get; set; }
        public string Name { get; set; }
        public string EndDay { get; set; }
        public decimal suma { get; set; }
        public int? Division { get; set; }
    }
}
