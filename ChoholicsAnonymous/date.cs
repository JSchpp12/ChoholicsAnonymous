using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//crud test
namespace ChoholicsAnonymous
{
    public class Date
    {
        public int Day { get; set; }
        public int Year { get; set; }

        private int month; 
        public int Month
        {
            get { return month; }
            set
            {
                int numYears = 0;
                if (value > 12)
                {
                    numYears = value / 12;
                    month = value % 12;
                    Year += numYears;
                }
                else
                    month = value;
            }
        }

        //default constructor 
        public Date()
        { }

        //converts a date object to the display format MM-DD-YYYY
        public Date(string dateString)
        {
            string tempContainer;
            int conversionCount = 0; //counts number of sub-categories in date string object (-)
            int previousPosition = 0, positionCount = 0;

            for (int i = 0; i < dateString.Length; i++)
            {
                if (dateString[i] == '-' || i == dateString.Length - 1)
                {
                    switch (conversionCount)
                    {
                        case 0:
                            try
                            {
                                tempContainer = dateString.Substring(previousPosition, positionCount);
                                this.Month = Int32.Parse(tempContainer);
                            }
                            catch (FormatException ex)
                            {
                                throw new System.InvalidCastException(ex.Message);
                            }
                            break;
                        case 1:
                            try
                            {
                                tempContainer = dateString.Substring(previousPosition, positionCount);
                                this.Day = Int32.Parse(tempContainer);
                            }
                            catch (FormatException ex)
                            {
                                throw new System.InvalidCastException(ex.Message);
                            }
                            break;
                        case 2:
                            try
                            {
                                tempContainer = dateString.Substring(previousPosition);
                                this.Year = Int32.Parse(tempContainer);
                            }
                            catch (FormatException ex)
                            {
                                throw new System.InvalidCastException(ex.Message);
                            }
                            break;
                        default:
                            throw new System.ArgumentException("An Unknown Error Has Occured");
                    }
                    positionCount = 0;
                    previousPosition = i + 1;
                    conversionCount++;
                }
                else
                    positionCount++;
            }
            if (conversionCount != 3)
                throw new System.ArgumentException("date string is not in a valid format");
        }

        //converts the date object to the correct date string format
        public string convToString()
        {
            string dateString;
            dateString = this.Month.ToString("D2") + "-" + this.Day.ToString("D2") + "-" + this.Year.ToString("D4");
            return dateString;
        }
        //test
    }
}
