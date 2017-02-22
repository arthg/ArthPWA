using ArthPWA.Common.CrudBase;
using ArthPWA.Common;

namespace ArthPWA.Models
{
    public sealed class WeightEntry : ResourceWithId, ITimeMarkable
    {
        public Timestamp Created { get; set; }
        public Timestamp LastModified { get; set; }
        public long Weight { get; set; }

        public WeightEntry()
        {
            Created = new Timestamp
            {
                On = SystemTime.UtcNow()
            };
            LastModified = new Timestamp
            {
                On = SystemTime.UtcNow()
            };
        }
    }
}