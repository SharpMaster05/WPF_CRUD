
using AutoMapper;
using DAL.Abstractions;

namespace BLL.Abstractions;

public class GenericService<DTO, Entity> : IService<DTO> where DTO : class, new() where Entity : class, new()
{
    private readonly IRepository<Entity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IRepository<Entity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Add(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        await _repository.Add(entity);
    }

    public async Task Delete(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        var existingEntity = await _repository.GetById(GetKey(entity));

        if (existingEntity != null)
        {
            await _repository.Delete(existingEntity);
        }
    }

    public async Task Update(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        var existingEntity = await _repository.GetById(GetKey(entity));

        if (existingEntity != null)
        {
            _mapper.Map(service, existingEntity);
            await _repository.Update(existingEntity);
        }
    }

    public async Task<IEnumerable<DTO>> GetAll()
    {
        var entities = await _repository.GetAll();
        return entities.Select(e => _mapper.Map<DTO>(e));
    }

    public async Task<DTO> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return _mapper.Map<DTO>(entity);
    }

    private int GetKey(Entity entity)
    {
        var propertyInfo = entity.GetType().GetProperty("Id");
        if (propertyInfo != null)
        {
            return (int)propertyInfo.GetValue(entity);
        }
        throw new ArgumentException("Entity does not have an Id property");
    }
}