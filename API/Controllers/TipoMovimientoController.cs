using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class TipoMovimientoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public TipoMovimientoController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoMovimientoDto>>> Get()
    {
        var entidad = await unitofwork.TipoMovimientos.GetAllAsync();
        return mapper.Map<List<TipoMovimientoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoMovimientoDto>> Get(int id)
    {
        var entidad = await unitofwork.TipoMovimientos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<TipoMovimientoDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoMovimientoDto>>> GetPaginacion([FromQuery] Params TipoMovimientoParams)
    {
        var entidad = await unitofwork.TipoMovimientos.GetAllAsync(TipoMovimientoParams.PageIndex, TipoMovimientoParams.PageSize, TipoMovimientoParams.Search);
        var listEntidad = mapper.Map<List<TipoMovimientoDto>>(entidad.registros);
        return new Pager<TipoMovimientoDto>(listEntidad, entidad.totalRegistros, TipoMovimientoParams.PageIndex, TipoMovimientoParams.PageSize, TipoMovimientoParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoMovimiento>> Post(TipoMovimientoDto entidadDto)
    {
        var entidad = this.mapper.Map<TipoMovimiento>(entidadDto);
        this.unitofwork.TipoMovimientos.Add(entidad);
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
    public async Task<ActionResult<TipoMovimientoDto>> Put(int id, [FromBody]TipoMovimientoDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<TipoMovimiento>(entidadDto);
        unitofwork.TipoMovimientos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.TipoMovimientos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.TipoMovimientos.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}