
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using static PastryShop.Api.ApiRoutes;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices(typeof(Program));
var app = builder.Build();

app.RegisterPipelineComponents(typeof(Program));



app.Run();
