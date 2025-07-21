using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using FraudSys.Infrastructure.Config;
using Microsoft.Extensions.Options;

namespace FraudSys.Infrastructure.Dynamo
{
  public class DynamoDbContext
  {
    public IAmazonDynamoDB Client { get; }

    public DynamoDbContext(IOptions<AWSSettings> options)
    {
      var settings = options.Value;

      var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
      var region = RegionEndpoint.GetBySystemName(settings.Region);

      Client = new AmazonDynamoDBClient(credentials, region);
    }
  }
}
