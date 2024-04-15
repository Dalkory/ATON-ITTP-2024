namespace ATON_ITTP_2024.Utilities
{
    public struct DateDifference
    {
        private DateTime fromDate;
        private DateTime toDate;
        private int dayIncrement = 0;
        private int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public int Year;
        public int Month;
        public int Day;

        public DateDifference(DateTime d1, DateTime d2)
        {
            if (d1 > d2)
            {
                this.fromDate = d2;
                this.toDate = d1;
            }
            else
            {
                this.fromDate = d1;
                this.toDate = d2;
            }

            dayIncrement = 0;
            if (this.fromDate.Day > this.toDate.Day)
            {
                dayIncrement = this.monthDay[this.fromDate.Month - 1];
            }

            if (dayIncrement == -1)
            {
                if (DateTime.IsLeapYear(this.fromDate.Year))
                {
                    dayIncrement = 29;
                }
                else
                {
                    dayIncrement = 28;
                }
            }

            if (dayIncrement != 0)
            {
                Day = (this.toDate.Day + dayIncrement) - this.fromDate.Day;
                dayIncrement = 1;
            }
            else
            {
                Day = this.toDate.Day - this.fromDate.Day;
            }

            if ((this.fromDate.Month + dayIncrement) > this.toDate.Month)
            {
                this.Month = (this.toDate.Month + 12) - (this.fromDate.Month + dayIncrement);
                dayIncrement = 1;
            }
            else
            {
                this.Month = (this.toDate.Month) - (this.fromDate.Month + dayIncrement);
                dayIncrement = 0;
            }

            this.Year = this.toDate.Year - (this.fromDate.Year + dayIncrement);
        }
    }
}
