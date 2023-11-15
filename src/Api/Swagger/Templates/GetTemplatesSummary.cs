﻿using Api.Endpoints.Templates;
using Api.Models.Requests;
using Api.Models.Responses;
using FastEndpoints;

namespace Api.Swagger.Templates;

internal class GetTemplatesSummary : Summary<GetTemplatesEndpoint>
{
    private static ApiResponse<TemplateResponse[]> Successful =>
        ApiResponse<TemplateResponse[]>.Success(
            new TemplateResponse[]
            {
                new()
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = "template_1",
                    TextTemplate = "template_1 body",
                    DataChunksDescription = null
                }
            });

    private static ApiResponse<TemplateResponse> Failed =>
        ApiResponse<TemplateResponse>.Fail(
            new ProblemDetails());

    public GetTemplatesSummary()
    {
        Summary = "Get templates";
        Description = "Using one of the accepted query headers get one or several templates";
        ExampleRequest = new GetTemplatesRequest
        {
            All = true,
            Ids = Array.Empty<string>(),
            Names = Array.Empty<string>(),
        };

        Response(StatusCodes.Status200OK,
            "List of templates",
            example: Successful
        );

        Response(StatusCodes.Status400BadRequest,
            "Template request validation failed. Check the ErrorCodes section in the response body for more info",
            example: Failed);
    }
}