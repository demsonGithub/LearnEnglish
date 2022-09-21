// shared
global using MediatR;
global using Demkin.Core;
global using Demkin.Core.Exceptions;

// domain
global using Demkin.Domain.Abstraction;
global using Demkin.FileOperation.Domain.AggregateModels;
global using Demkin.FileOperation.Domain.Events;
global using Demkin.FileOperation.Domain;
global using Demkin.FileOperation.Domain.Interfaces;

// infrastructure
global using Demkin.FileOperation.Infrastructure;
global using Demkin.FileOperation.Infrastructure.Repositories;

// application
global using Demkin.FileOperation.WebApi.Extensions;
global using Demkin.FileOperation.WebApi.Application.Commands;
global using Demkin.FileOperation.WebApi.Application.Queries;
global using Demkin.FileOperation.WebApi.ViewModels;