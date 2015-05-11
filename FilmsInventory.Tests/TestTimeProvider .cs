using System;
using FilmsInventory.Utils;

namespace FilmsInventory.Tests
{
    public class TestTimeProvider : TimeProvider
    {
        private DateTime nowDateTime;
        
        public TestTimeProvider()
        {
            this.nowDateTime = new DateTime(2015, 1, 1);
        }

        public override DateTime UtcNow
        {
            get { return this.nowDateTime; }
        }

        public void SetNowDate(DateTime nowDateTime)
        {
            this.nowDateTime = nowDateTime;
        }
    }
}
