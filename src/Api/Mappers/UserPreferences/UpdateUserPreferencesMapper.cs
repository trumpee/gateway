using Api.Extensions;
using Api.Models.Requests.UserPreferences;
using Api.Models.Responses;
using Core.Models.UserPreferences;
using ErrorOr;
using FastEndpoints;

namespace Api.Mappers.UserPreferences;

public class UpdateUserPreferencesMapper
    : Mapper<UpdateUserPreferencesRequest, ApiResponse, ErrorOr<UserPreferencesDto>>
{
    public override ErrorOr<UserPreferencesDto> ToEntity(UpdateUserPreferencesRequest r)
    {
        return new UserPreferencesDto
        {
            UserId = r.UserId,
            Channels = r.Channels.ToDictionary(
                kvp => kvp.Key,
                kvp => new ChannelDescriptorBaseDto
                {
                    Enabled = kvp.Value.Enabled,
                    Description = kvp.Value.Description,
                    Metadata = kvp.Value.Metadata
                })
        };
    }
    
    public override ApiResponse FromEntity(ErrorOr<UserPreferencesDto> e)
    {
        if (e.IsError)
        {
            var problemDetails = new ProblemDetails
            {
                Errors = e.ToProblemDetailsErrors()
            };
            
            return ApiResponse.Fail(problemDetails);
        }
        
        return ApiResponse.Success();
    }
}