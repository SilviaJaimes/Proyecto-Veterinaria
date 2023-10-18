using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize]

public class MascotaController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MascotaController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
    {
        var entidad = await unitofwork.Mascotas.GetAllAsync();
        return mapper.Map<List<MascotaDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MascotaDto>> Get(int id)
    {
        var entidad = await unitofwork.Mascotas.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<MascotaDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MascotaDto>>> GetPaginacion([FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.GetAllAsync(mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<MascotaDto>>(entidad.registros);
        return new Pager<MascotaDto>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-3/{Especie}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MascotaEspecie(string Especie)
    {
        var entidad = await unitofwork.Mascotas.MascotaEspecie(Especie);
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-3/{Especie}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MascotaEspeciePaginated(string Especie, [FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.MascotaEspeciePaginated(Especie, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros);
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-6")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MascotasVacunadas2023()
    {
        var entidad = await unitofwork.Mascotas.MascotasVacunadas2023();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-6")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MascotasVacunadas2023Paginated([FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.MascotasVacunadas2023Paginated(mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-7")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MascotasPorEspecie()
    {
        var entidad = await unitofwork.Mascotas.MascotasPorEspecie();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-7")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MascotasPorEspeciePaginated([FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.MascotasPorEspeciePaginated(mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-9/{Veterinario}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MascotasPorVeterinario(string Veterinario)
    {
        var entidad = await unitofwork.Mascotas.MascotasPorVeterinario(Veterinario);
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-9/{Veterinario}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MascotasPorVeterinarioPaginated(string Veterinario, [FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.MascotasPorVeterinarioPaginated(Veterinario, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-11/{Raza}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MascotasYPropietariosPorRaza(string Raza)
    {
        var entidad = await unitofwork.Mascotas.MascotasYPropietariosPorRaza(Raza);
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-11/{Raza}")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MascotasYPropietariosPorRazaPaginated(string Raza, [FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.MascotasYPropietariosPorRazaPaginated(Raza, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpGet("consulta-12")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> CantidadMascotasPorRaza()
    {
        var entidad = await unitofwork.Mascotas.CantidadMascotasPorRaza();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-12")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> CantidadMascotasPorRazaPaginated([FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Mascotas.CantidadMascotasPorRazaPaginated(mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Mascota>> Post(MascotaDto entidadDto)
    {
        var entidad = this.mapper.Map<Mascota>(entidadDto);
        this.unitofwork.Mascotas.Add(entidad);
        await unitofwork.SaveAsync();
        if(entidad == null)
        {
            return BadRequest();
        }
        entidadDto.Id = entidad.Id;
        return CreatedAtAction(nameof(Post), new {id = entidadDto.Id}, entidadDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MascotaDto>> Put(int id, [FromBody]MascotaDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Mascota>(entidadDto);
        unitofwork.Mascotas.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Mascotas.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Mascotas.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}