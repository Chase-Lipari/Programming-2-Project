using System;
using System.Collections.Generic;
using System.Text;

namespace Project2P6
{
    class TimeStamp
    {
        public const int MAXHOURS = 999999999;
        public const int SECONDSINHOUR = 3600;
        public const int SECONDSINMINUTE = 60;
        public const int MINUTEINHOUR = 60;
        public const int MIN_VALUE = 0;
        private int _hours;
        private int _minutes;
        private int _seconds;
        public int Hours
        {
            get { return _hours; }
            set
            {
                //Option A: One General message
                if (value < 0 || value >= MAXHOURS)
                    throw new ArgumentOutOfRangeException("Hours", "Hours has to be between " + MIN_VALUE + " and " + (MAXHOURS - 1));

                /*Option B: Custom error messages
                if (value < 0 )
                    throw new ArgumentOutOfRangeException("Hours", "Hours has to be at least " + MIN_VALUE);
                if (value >= MAXHOURS)
                    throw new ArgumentOutOfRangeException("Hours", "Hours has to be less than " + MAXHOURS);
                */

                _hours = value;
            }
        }

        public int Minutes
        {
            get
            {
                return _minutes;
            }

            set
            {
                if (value < 0 )
                {
                    throw new ArgumentOutOfRangeException("Minutes", string.Format("Minutes has to be above {0} ", MIN_VALUE));
                }else if(value == MINUTEINHOUR){
                    Hours++;
                    value = 0;
                }else if(value > MINUTEINHOUR)
                {
                    Hours += value / MINUTEINHOUR;
                    value %= MINUTEINHOUR;
                }
                    

                _minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return _seconds;
            }
            set
            {
                if (value < 0 || value >= SECONDSINMINUTE)
                    throw new ArgumentOutOfRangeException("Seconds", string.Format("Seconds has to be between {0} and {1}.", MIN_VALUE, SECONDSINMINUTE - 1));

                _seconds = value;
            }
        }


        public TimeStamp()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public TimeStamp(int hours_, int minutes_, int seconds_)
        {
            Hours = hours_;
            Minutes = minutes_;
            Seconds = seconds_;
        }

        public static TimeStamp AddTwoTimeStamps(TimeStamp t1, TimeStamp t2)
        {
            TimeStamp sum = new TimeStamp();

            int totalSeconds = t1.ConvertToSeconds() + t2.ConvertToSeconds();
            sum.ConvertFromSeconds(totalSeconds);

            return sum;
        }

        public void AddSeconds(int sec)
        {
            int totalSeconds = this.ConvertToSeconds() + sec;       //add all seconds toghether
            ConvertFromSeconds(totalSeconds);                       //update the backing fields with the total seconds
        }

        public TimeStamp ConvertFromSeconds(int secondsToConvert)
        {
            //Convert to hours
            Hours = (secondsToConvert / SECONDSINHOUR);      
            //Convert to Minutes
            Minutes = (secondsToConvert % SECONDSINHOUR) / SECONDSINMINUTE;
            //Convert to Seconds
            Seconds = secondsToConvert % SECONDSINMINUTE;
            //Hours %= MAXHOURS;

            return this;
        }

        public int ConvertToSeconds()
        {
            //Two ways to implement it it doesn't matter which one you choose since it is the same outcome. 
            /*    return (_hours * SECONDSINHOUR) + (_minutes * SECONDSINMINUTE) + _seconds; */     //using Backing Fields
            return (Hours * SECONDSINHOUR) + (Minutes * SECONDSINMINUTE) + Seconds;       //or using Properties
        }

        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}", Hours, Minutes, Seconds);
        }

        public override bool Equals(object obj)
        {
            //Version A) This is the quick and dirty way. It is unsafe when obj is null or obj is not a TimeStamp
            //return ((TimeStamp)obj).ConvertToSeconds() == this.ConvertToSeconds();      //this keyword is optional because we are inside the class

            //Version B) The safe way
            //We have to deal with the situation of when obj is null
            if (obj == null) return false;

            TimeStamp otherTS = obj as TimeStamp;    //Typecast obj as TimeStamp.
                                                     //If the sent object is not a TimeStamp, obj will be null.

            if (otherTS != null)
                return otherTS.ConvertToSeconds() == this.ConvertToSeconds();   //If cursor reached here it means obj is a valid TimeStamp. Let's compare it.
            else
                throw new ArgumentException("Object is not an TimeStamp");


        }
        public override int GetHashCode()
        {
            //Requires to install NuGet package Microsoft.Bcl.HashCode 
            return HashCode.Combine(_hours, _minutes, _seconds);
        }

        public void ReadFromConsole()
        {
            Console.Write("Please enter number of hours (0..23): ");
            this.Hours = Input(max: MAXHOURS - 1, min: 0);            //using named arguments

            Console.Write("Please enter number of minutes (0..59): ");
            this.Minutes = Input(0, MINUTEINHOUR - 1);

            Console.Write("Please enter number of seconds (0..59): ");
            this.Seconds = Input(min: 0, max: SECONDSINMINUTE - 1);
        }

        //Private method used internally
        private static int Input(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.Write("Error! Please enter a number between {0}..{1}: ", min, max);
            }

            return input;
        }



        #region Operators

        public static TimeStamp operator +(TimeStamp t1, TimeStamp t2)
        {
            TimeStamp sum = new TimeStamp();

            /* Long way
            int sumSeconds = t1.Seconds + t2.Seconds;
            int sumMinutes = t1.Minutes + t2.Minutes + (sumSeconds / SECONDSINMINUTE);
            int sumHours = t1.Hours + t2.Hours + (sumMinutes / MINUTEINHOUR);

            sum.Seconds = sumSeconds % SECONDSINMINUTE;
            sum.Minutes = sumMinutes % MINUTEINHOUR;
            sum.Hours = sumHours % MAXHOURS;
            */

            //Short way: convert everything to seconds
            int totalSeconds = t1.ConvertToSeconds() + t2.ConvertToSeconds();
            sum.ConvertFromSeconds(totalSeconds);

            return sum;
        }

        public static TimeStamp operator +(TimeStamp t1, int sec)
        {
            TimeStamp sum = new TimeStamp();

            /* Long way
            int sumSeconds = t1.Seconds + sec;
            int sumMinutes = t1.Minutes + (sumSeconds / SECONDSINMINUTE);
            int sumHours = t1.Hours + (sumMinutes / MINUTEINHOUR);

            sum.Seconds = sumSeconds % SECONDSINMINUTE;
            sum.Minutes = sumMinutes % MINUTEINHOUR;
            sum.Hours = sumHours % MAXHOURS;
            */

            //Short way: convert everything to seconds
            int totalSeconds = t1.ConvertToSeconds() + sec;
            sum.ConvertFromSeconds(totalSeconds);

            return sum;
            //or shorter
            //return sum.ConvertFromSeconds(totalSeconds);
        }

        public static bool operator ==(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() == t2.ConvertToSeconds();
        }

        public static bool operator !=(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() != t2.ConvertToSeconds();
        }

        public static bool operator >(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() > t2.ConvertToSeconds();
        }

        public static bool operator <(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() < t2.ConvertToSeconds();
        }

        public static bool operator >=(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() >= t2.ConvertToSeconds();
        }

        public static bool operator <=(TimeStamp t1, TimeStamp t2)
        {
            return t1.ConvertToSeconds() <= t2.ConvertToSeconds();
        }

        #endregion
    }
}
