using System;
using System.Collections.Generic;
using System.Text;

namespace GetEvaluationRequest
{
    public class EvaluationRequest
    {
        public Guid LoanKey { get; set; }

        public int GlobalPropertyId { get; set; }

        public EvaluationPeriod EvaluationPeriod { get; set; }
    }
}
