using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProveedorControlle : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public ProveedorControlle( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProveedorDto>>> Get()
    {
        var entidad = await unitofwork.Proveedores.GetAllAsync();
        return mapper.Map<List<ProveedorDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProveedorDto>> Get(int id)
    {
        var entidad = await unitofwork.Proveedores.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<ProveedorDto>(entidad);
    }

    [HttpGet("paginacion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<ProveedorDto>>> GetPaginacion([FromQuery] Params proveedorParams)
    {
        var entidad = await unitofwork.Proveedores.GetAllAsync(proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
        var listEntidad = mapper.Map<List<ProveedorDto>>(entidad.registros);
        return new Pager<ProveedorDto>(listEntidad, entidad.totalRegistros, proveedorParams.PageIndex, proveedorParams.PageSize, proveedorParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Proveedor>> Post(ProveedorDto entidadDto)
    {
        var entidad = this.mapper.Map<Proveedor>(entidadDto);
        this.unitofwork.Proveedores.Add(entidad);
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
    public async Task<ActionResult<ProveedorDto>> Put(int id, [FromBody]ProveedorDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Proveedor>(entidadDto);
        unitofwork.Proveedores.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Proveedores.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Proveedores.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}