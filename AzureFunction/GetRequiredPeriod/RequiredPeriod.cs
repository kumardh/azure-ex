using System;
using System.Collections.Generic;
using System.Text;

namespace GetRequiredPeriod
{
    public class RequiredPeriod
    {
        public Peril Peril { get; set; }

        public CvgType CvgType { get; set; }

        public RiskType RiskType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}

