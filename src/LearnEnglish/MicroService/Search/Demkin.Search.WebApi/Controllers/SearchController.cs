﻿using Demkin.Core;
using Demkin.Search.Domain;
using Demkin.Search.WebApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demkin.Search.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResult<SearchEpisodeResponse>> SearchEpisodes([FromQuery] SearchEpisodesQuery query)
        {
            var result = await _mediator.Send(query, HttpContext.RequestAborted);

            return ApiResult<SearchEpisodeResponse>.Build(result);
        }
    }
}