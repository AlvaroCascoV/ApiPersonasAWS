using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using ApiPersonasAWS.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ApiPersonasAWS;

public class Functions
{
    private List<Persona> personasList;
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
        this.personasList = new List<Persona>();
        Persona p = new Persona
        {
            IdPersona = 1,
            Nombre = "Juan",
            Email = "juan@example.com",
            Edad = 30
        };
        this.personasList.Add(p); 
        p = new Persona
        {
            IdPersona = 2,
            Nombre = "María",
            Email = "maria@example.com",
            Edad = 21
        };
        this.personasList.Add(p); 
        p = new Persona
        {
            IdPersona = 3,
            Nombre = "Pedro",
            Email = "pedro@example.com",
            Edad = 32
        };
        this.personasList.Add(p); 
        p = new Persona
        {
            IdPersona = 4,
            Nombre = "Sofia",
            Email = "sofia@example.com",
            Edad = 15
        };
        this.personasList.Add(p);
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <remarks>
    /// This uses the <see href="https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md">Lambda Annotations</see> 
    /// programming model to bridge the gap between the Lambda programming model and a more idiomatic .NET model.
    /// 
    /// This automatically handles reading parameters from an APIGatewayProxyRequest
    /// as well as syncing the function definitions to serverless.template each time you build.
    /// 
    /// If you do not wish to use this model and need to manipulate the API Gateway 
    /// objects directly, see the accompanying Readme.md for instructions.
    /// </remarks>
    /// <param name="context">Information about the invocation, function, and execution environment</param>
    /// <returns>The response as an implicit <see cref="APIGatewayProxyResponse"/></returns>
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public IHttpResult Get(ILambdaContext context)
    {
        context.Logger.LogInformation("Handling the 'Get' Request");
        string json = JsonConvert.SerializeObject(this.personasList);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/find/{id}")]
    public IHttpResult GetById(ILambdaContext context, int id)
    {
        Persona persona = this.personasList.FirstOrDefault(p => p.IdPersona == id);
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Post, "/post")]
    public IHttpResult Post(ILambdaContext context,[FromBody] Persona persona)
    {
        string json = JsonConvert.SerializeObject(persona);
        return HttpResults.Ok(json);
    }
}
