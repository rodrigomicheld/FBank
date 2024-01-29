using Azure.Data.Tables;
using Azure.Storage.Blobs;
using FBank.Function.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Text;

namespace FBank.Function
{
    public static class OrchestrationFunctionPix
    {
        [Function("PixStart")]
        public static async Task<HttpResponseData> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("PixStart");

            string corpoRequisicao = await new StreamReader(req.Body).ReadToEndAsync();
            var pixTransferOperation = JsonConvert.DeserializeObject<PixTransferOperation>(corpoRequisicao);

            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(OrchestrationFunctionPix), pixTransferOperation);

            logger.LogInformation($"Iniciada orquestração de solicitação de Pix com ID = '{instanceId}'.");

            return client.CreateCheckStatusResponse(req, instanceId);
        }


        [Function(nameof(OrchestrationFunctionPix))]
        public static async Task<List<string>> RunOrchestrator(
                [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(OrchestrationFunctionPix));
            logger.LogInformation("Iniciada validação para transferencia do Pix.");

            var pixTransferOperation = context.GetInput<PixTransferOperation>();

            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>(nameof(TransferPix), pixTransferOperation));
            outputs.Add(await context.CallActivityAsync<string>(nameof(PixQueue), pixTransferOperation));
            outputs.Add(await context.CallActivityAsync<string>(nameof(PixBlob), pixTransferOperation));
            outputs.Add(await context.CallActivityAsync<string>(nameof(PixEntity), pixTransferOperation));

            return outputs;
        }

        [Function("PixQueue")]
        [QueueOutput("pix-operation-queue")]
        public static string PixQueue(
        [ActivityTrigger] PixTransferOperation pixTransferOperation,
        FunctionContext context)
        {
            var logger = context.GetLogger("PixQueue");

            string meesage = JsonConvert.SerializeObject(pixTransferOperation);

            logger.LogInformation($"Mensagem enviada para a fila. {meesage}");

            return meesage;
        }

        [Function("PixBlob")]
        public static string PixBlob([ActivityTrigger] PixTransferOperation pixTransferOperation, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("PixBlob");

            var blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("pix-comprovante");

            string blobName = $"pixTransferOperation_{pixTransferOperation.AccountNumberFrom}_{DateTime.Now:dd-MM-yyyy-HH-mm-ss}.txt";
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine("Pix Processado com sucesso: ");
                writer.WriteLine($"Account From: {pixTransferOperation.AccountNumberFrom}");
                writer.WriteLine($"Account To: {pixTransferOperation.AccountNumberTo}");
                writer.WriteLine($"Value: {pixTransferOperation.Value}");

                writer.Flush();
                stream.Position = 0;

                blobClient.Upload(stream, true);
            }

            logger.LogInformation($"Comprovante pix gerado com sucesso no Blob Storage : {blobName}");

            return $"Gerado Pix no Blob com sucesso!";
        }

        [Function("PixEntity")]
        public static string PixEntity([ActivityTrigger] PixTransferOperation pixTransferOperation, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("PixEntity");

            TableClient tableClient = new TableClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "PixTransferencia");
            tableClient.CreateIfNotExistsAsync();
            var pixId = Guid.NewGuid().ToString();
            TableEntity tableEntity = new TableEntity()
            {
                {"PartitionKey","pixTransferencia"},
                {"RowKey", pixId},
                {"accountNumberFrom", pixTransferOperation.AccountNumberFrom},
                {"accountNumberTo", pixTransferOperation.AccountNumberTo},
                {"value",pixTransferOperation.Value}
            };
            tableClient.AddEntityAsync(tableEntity);
            logger.LogInformation("Incluido Pix na tabela ID {0}", pixId);

            return $"Incluido Pix na tabela com sucesso!";
        }

        [Function("TransferPix")]
        public static async Task<string> TransferPix(
               [ActivityTrigger] PixTransferOperation transf)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            try
            {
                var apiBase = Environment.GetEnvironmentVariable("FBankAPI");

                var body = "{\n  \"accountNumberTo\":" + transf.AccountNumberTo + ",\n  \"accountNumberFrom\":" + transf.AccountNumberFrom + ",\n  \"value\": " + transf.Value.ToString(nfi) + "\n}";
                var client = new RestClient(apiBase + "/Transaction/TransferPix");

                var request = new RestRequest();
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var response = client.Post(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return "transferencia concluida com sucesso";
                }
                else
                {
                    return "Erro ao tentar transferir, tente novamente daqui um cadin";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}