using System;

namespace ArthPWA.Common
{
    /// <summary>
    /// Helper to abstract DateTime.Now, and make it replaceable for tests
    /// http://ayende.com/blog/3408/dealing-with-time-in-tests
    /// In prod code use SystemTime.Now() instead of DateTime.Now
    /// In tests:
    /// SystemTime.Now = () => new DateTime(2000,1,1);
    /// NOTE: make sure in teardown to restore to SystemTime.Now = () => DateTime.Now;
    /// </summary>
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;

        static SystemTime()
        {
            ResetToDefault();
        }

        public static void ResetToDefault()
        {
            Now = () => DateTime.Now;
            UtcNow = () => DateTime.UtcNow;
        }
    }
}