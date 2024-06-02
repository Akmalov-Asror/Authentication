using Teaching.Common.Entities.Dto_s;

namespace Teaching.V1.Auth.Services.Interfaces;

public interface IUzdevumi
{
    ValueTask<List<UzdevumiDto>> GetByFilter();
    ValueTask<CreateDto> Create(CreateDto dto);
    ValueTask<UpdateUzdevumiDto> UpdateUzdevumi(Guid id,UpdateUzdevumiDto dto);
} 