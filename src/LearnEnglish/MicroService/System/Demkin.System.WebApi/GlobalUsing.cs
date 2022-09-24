// shared
global using Demkin.Core.Extensions;
global using Serilog;
global using MediatR;
global using Demkin.Core;
global using Demkin.Core.Jwt;
global using Demkin.Domain.Abstraction;
global using Demkin.Core.Filters;
global using Demkin.Utils.ContractResolver;
global using Demkin.Utils;

// domain
global using Demkin.System.Domain;
global using Demkin.System.Domain.Events;
global using Demkin.System.Domain.Interfaces;
global using Demkin.System.Domain.AggregateModels;

// infrastructure
global using Demkin.System.Infrastructure;
global using Demkin.System.Infrastructure.Repositories;

// application

global using Demkin.System.WebApi.Application.Commands;
global using Demkin.System.WebApi.Application.Queries;
global using Demkin.System.WebApi.ViewModels;