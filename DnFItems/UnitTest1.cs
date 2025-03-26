
using DnFItems.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace DnFItems
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public async Task TestMethod1()
        {
            // 기준 주차 시작일 (2025-01-09 06:00)
            DateTime baseDate = new DateTime(2025, 1, 9, 6, 0, 0);

            // 테스트 데이터 (yyyy-MM-dd HH:mm 형식)
            string dateString = "2025-03-13 07:00";
            int weekNumber = GetWeekNumber(dateString, baseDate);

            Console.WriteLine($"주어진 날짜 {dateString}의 주차는 {weekNumber}주차입니다.");
        }



        public int GetWeekNumber(string dateString, DateTime baseDate)
        {
            // 문자열을 DateTime으로 변환
            if (!DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime targetDate))
            {
                //throw new ArgumentException("잘못된 날짜 형식입니다.");
                return -1;
            }

            // 기준일과의 차이 계산
            TimeSpan difference = targetDate - baseDate;

            // 주차 계산 (0주차부터 시작하므로 +1)
            int weekNumber = (difference.Days / 7) + 1;

            return weekNumber;
        }

    }
}
