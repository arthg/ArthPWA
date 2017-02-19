using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Castle.Core.Internal;

namespace ArthPWA.Tests.Helpers
{
    public static class RandomData
    {
        //parts of the random wrapper functionality is taken from Jon Skeet :)
        //http://csharpindepth.com/Articles/Chapter12/Random.aspx

        private static class RandomProvider
        {
            private static int _seed = Environment.TickCount;

            private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() =>
                new Random(Interlocked.Increment(ref _seed))
            );

            public static Random Random
            {
                get { return RandomWrapper.Value; }
            }
        }

        // because we new System.Net.Mail.MailMessage in tests and it will throw exception when invalid email format specified
        public static string GetEmailAddress()
        {
            return string.Format("{0}@example.com", GetString(10));
        }

        public static string GetString(int size)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * RandomProvider.Random.NextDouble() + 65)));
                stringBuilder.Append(ch);
            }

            return stringBuilder.ToString();
        }

        public static string GetDigits(int size)
        {
            const string DIGITS = "0123456789";
            var stringBuilder = new StringBuilder();
            Enumerable.Range(0, size).ForEach(i => stringBuilder.Append(DIGITS[RandomProvider.Random.Next(0, DIGITS.Length)]));
            return stringBuilder.ToString();
        }

        public static DateTime GetDateTime()
        {
            var randomYear = RandomProvider.Random.Next(2014, 3000);
            var randomMonth = RandomProvider.Random.Next(1, 12);
            return new DateTime
                (
                randomYear,
                randomMonth,
                RandomProvider.Random.Next(1, DateTime.DaysInMonth(randomYear, randomMonth)),
                RandomProvider.Random.Next(0, 23), //h
                RandomProvider.Random.Next(0, 59), //m
                RandomProvider.Random.Next(0, 59) //s
                );
        }

        public static int GetInt()
        {
           return RandomProvider.Random.Next();
        }

        public static int GetInt(int min, int max)
        {
            return RandomProvider.Random.Next(min, max);
        }

        public static decimal GetDecimal()
        {
            return GetInt()/100m;
        }

        public static decimal GetDecimal(decimal min, decimal max)
        {
            if (max > Int32.MaxValue)
            {
                max = Int32.MaxValue;
            }
            return GetInt((int)min, (int)(max)) / 100m;
        }

        public static byte[] GetByteArray(int length)
        {
            var byteList = new List<byte>();
            for (var ii = 0; ii < length; ii++)
            {
                byteList.Add((byte)GetInt(0,255));
            }
            return byteList.ToArray();
        }
    }
}