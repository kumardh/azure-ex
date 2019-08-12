using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace GetRequiredPeriod
{
    public static class GetRequiredPeriod
    {
        [FunctionName("FuncGetRequiredPeriod")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));
            if (req.Body == null) throw new ArgumentNullException(nameof(req));

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) throw new ArgumentNullException(nameof(requestBody));

            var evaluationRequest = JsonConvert.DeserializeObject<EvaluationRequest>(requestBody);

            if (evaluationRequest == null) throw new ArgumentNullException(nameof(evaluationRequest));
            if (evaluationRequest.EvaluationPeriod == null) throw new ArgumentNullException(nameof(evaluationRequest.EvaluationPeriod));

            return (ActionResult)new OkObjectResult(GetRequiredPeriods(evaluationRequest));
        }

        public static IEnumerable<RequiredPeriod> GetRequiredPeriods(EvaluationRequest evaluation)
        {
            return new RequiredPeriod[]
            {
                new RequiredPeriod(){ StartDate = evaluation.EvaluationPeriod.StartDate, EndDate = evaluation.EvaluationPeriod.EndDate, CvgType = CvgType.WI, Peril= Peril.Fire, RiskType = RiskType.Lapse},
                new RequiredPeriod(){ StartDate = evaluation.EvaluationPeriod.StartDate, EndDate = evaluation.EvaluationPeriod.EndDate, CvgType = CvgType.WI, Peril= Peril.Fire, RiskType = RiskType.Gap},
                new RequiredPeriod(){ StartDate = evaluation.EvaluationPeriod.StartDate, EndDate = evaluation.EvaluationPeriod.EndDate, CvgType = CvgType.SO, Peril= Peril.Fire, RiskType = RiskType.Lapse},
                new RequiredPeriod(){ StartDate = evaluation.EvaluationPeriod.StartDate, EndDate = evaluation.EvaluationPeriod.EndDate, CvgType = CvgType.SO, Peril= Peril.Fire, RiskType = RiskType.Gap},
            };
        }
    }
}
