// shared
global using Serilog;
global using Demkin.Utils;
global using Demkin.Core.Extensions;
global using MediatR;
global using Demkin.Core;

// domain
global using Demkin.Listen.Domain;
global using Demkin.Listen.Domain.AggregateModels;

// infrastructure
global using Demkin.Listen.Infrastructure;
global using Demkin.Listen.Domain.Interfaces;

// application
global using Demkin.Listen.WebApi.Admin.Application.Commands;
global using Demkin.Listen.WebApi.Admin.Application.Queries;
global using Demkin.Listen.WebApi.Admin.ViewModels;