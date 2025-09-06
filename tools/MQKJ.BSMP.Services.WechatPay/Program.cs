using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQKJ.BSMP.QCloud;
using MQKJ.BSMP.QCloud.Configs;
using MQKJ.BSMP.QCloud.Models.CMQ.Requests;
using MQKJ.BSMP.QCloud.Models.CMQ.Responses;
using MQKJ.BSMP.QCloud.Models.MQAPI;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;

namespace MQKJ.BSMP.Services.WechatPay
{
    class Program
    {
        private static ILogger<Program> _logger;
        //private const string QUEUE_NAME = "mq-wechatpay";
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("app.json");
            var configuration = builder.Build();
            var services = new ServiceCollection()
                .AddLogging(x =>
                {
                    x.SetMinimumLevel(LogLevel.Debug);

                    x.AddNLog(new NLogProviderOptions
                    {
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true
                    });
                });

            services.AddHttpClientService();
            services.Configure<QcloudConfig>(configuration);
            services.AddSingleton<IQCloudApiClient, QCloudApiClient>();
            var provider = services.BuildServiceProvider();
            _logger = provider.GetService<ILoggerFactory>()?.CreateLogger<Program>();
            var config = provider.GetService<IOptions<QcloudConfig>>();
            var client = provider.GetService<IQCloudApiClient>();
            var request = new ReceiveMessageRequest(config?.Value)
            {
                QueueName = config?.Value?.MQ_WechatPay,
                PollingWaitSeconds = 5
            };

            _logger.LogInformation($"request is {JsonConvert.SerializeObject(request)}, url is {request.GetUrl()}");
            _logger.LogWarning(message: $"current queryOrderState url is：{config.Value.MqApiUrl} and queueName is {config.Value.MQ_WechatPay}");

            while (true)
            {
                try
                {
                    var response = client.Execute<ReceiveMessageRequest, ReceiveMessageResponse>(request).GetAwaiter().GetResult();
                    Console.WriteLine($"response is {JsonConvert.SerializeObject(response)}");
                    if (response.Code == 0)
                    {
                        CheckMqOrderState(response, config?.Value, client);
                    }

                    if (response.Code != 0)
                    {
                        _logger.LogWarning($"没有新的消息，1s后查询");
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"运行出错，错误信息：{ex}");
                }
            }
        }

        private static void CheckMqOrderState(ReceiveMessageResponse message, QcloudConfig config, IQCloudApiClient client)
        {
            _logger.LogDebug($"有新的消息，开始购买金币");
            var request = new QueryOrderReqeust(config)
            {
                OutTradeNo = message.MsgBody
            };
            var response = client.Execute<QueryOrderReqeust, QueryOrderResponse>(request).GetAwaiter().GetResult();
            if (response.IsError)
            {
                throw new Exception(response.ErrorMessage);
            }

            _logger.LogInformation($"充值成功");

            DeleteQueueMessage(message, config, client);
        }

        private static void DeleteQueueMessage(ReceiveMessageResponse message, QcloudConfig config, IQCloudApiClient client)
        {
            _logger.LogInformation($"删除消息，消息内容：{JsonConvert.SerializeObject(message)}");
            var request = new DeleteMessageRequest(config)
            {
                QueueName = config.MQ_WechatPay,
                ReceiptHandle = message.ReceiptHandle
            };

            var response = client.Execute<DeleteMessageRequest, DeleteMessageResponse>(request)
                .GetAwaiter()
                .GetResult();

            if (response.Code == 0)
            {
                _logger.LogInformation($"删除消息成功");
            }
            else
            {
                _logger.LogError($"删除失败，失败消息：{response.Message}");
            }
        }
    }
}
