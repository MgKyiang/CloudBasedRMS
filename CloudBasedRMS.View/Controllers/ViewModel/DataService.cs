using CloudBasedRMS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CloudBasedRMS.View.Controllers.ViewModel
{
    public static class DataService
    {
        public static List<DataPoint> GetRandomDataForNumericAxis(int count)
        {
            double y = 50;
            _dataPoints = new List<DataPoint>();
            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);
                _dataPoints.Add(new DataPoint(i, y));
            }
            return _dataPoints;
        }

        public static List<DataPoint> GetRandomDataForCategoryAxis(int count)
        {
            double y = 50;
            DateTime dateTime = new DateTime(2006, 01, 1, 0, 0, 0);
            string label = "";
            _dataPoints = new List<DataPoint>();
            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);
                label = dateTime.ToString("MMM");
                _dataPoints.Add(new DataPoint(y, label));
                dateTime = dateTime.AddYears(1);
            }
            return _dataPoints;
        }
        public static List<DataPoint> GetPopularFoodNameTotalPrice(List<BillFoodItems> billfooditem)
        {
            string Label = string.Empty;
            _dataPoints = new List<DataPoint>();
            foreach(var item in billfooditem)
            {
                Label = item.FoodITemsDetails.Description;
                _dataPoints.Add(new DataPoint(Convert.ToDouble(item.Amount), Label));
            }
            return _dataPoints;
        }
        public static List<DataPoint> GetEmployeeStrength(List<Employee> employee)
        {
            string Label = string.Empty;
            _dataPoints = new List<DataPoint>();
            List<int> rankrepo = new List<int>();
            var ranks = employee.Select(r => r.Rank.Description).Distinct();
            foreach (var item in ranks)
            {
                Label =item;
                _dataPoints.Add(new DataPoint(Convert.ToDouble(employee.Count(c => c.Rank.Description == item)), Label));
            }
                       
            return _dataPoints;
        }
        public static List<DataPoint> GetRandomDataForDateTimeAxis(int count)
        {
            double x = 0;
            double y = 50;
            var dateTime = new DateTime(2006, 01, 10, 0, 0, 0, DateTimeKind.Local);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            _dataPoints = new List<DataPoint>();
            for (int i = 0; i < count; i++)
            {
                y = y + (random.Next(0, 20) - 10);
                x = dateTime.ToUniversalTime().Subtract(epoch).TotalMilliseconds;
                _dataPoints.Add(new DataPoint(x, y));
                dateTime = dateTime.AddDays(1);
                //dateTime = dateTime.AddHours(1);
            }
            return _dataPoints;
        }

        private static Random random = new Random(DateTime.Now.Millisecond);
        private static List<DataPoint> _dataPoints;
    }
}