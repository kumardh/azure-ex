using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GetEvaluationRequest
{
    public static class GetEvaluationRequest
    {
        [FunctionName("FuncEvaluationRequest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (req == null) throw new ArgumentNullException(nameof(req));
            if (req.Body == null) throw new ArgumentNullException(nameof(req));

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrEmpty(requestBody)) throw new ArgumentNullException(nameof(requestBody));

            var evaluationRequest = JsonConvert.DeserializeObject<EvaluationRequest>(requestBody);

            if (evaluationRequest == null) throw new ArgumentNullException(nameof(evaluationRequest));
            if (evaluationRequest.EvaluationPeriod == null) throw new ArgumentNullException(nameof(evaluationRequest.EvaluationPeriod));
            // Add clamp logic to evaluation period
            evaluationRequest.EvaluationPeriod.StartDate = evaluationRequest.EvaluationPeriod.StartDate < DateTime.Parse("02/10/2018")
                ? DateTime.Parse("02/10/2018")
                : evaluationRequest.EvaluationPeriod.StartDate;

            return (ActionResult)new OkObjectResult(evaluationRequest);
        }
    }   
}
