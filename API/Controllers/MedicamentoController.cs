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
/* [Authorize] */

public class MedicamentoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MedicamentoController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicamentoDto>>> Get()
    {
        var entidad = await unitofwork.Medicamentos.GetAllAsync();
        return mapper.Map<List<MedicamentoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MedicamentoDto>> Get(int id)
    {
        var entidad = await unitofwork.Medicamentos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<MedicamentoDto>(entidad);
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicamentoDto>>> GetPaginacion([FromQuery] Params medicamentoParams)
    {
        var entidad = await unitofwork.Medicamentos.GetAllAsync(medicamentoParams.PageIndex, medicamentoParams.PageSize, medicamentoParams.Search);
        var listEntidad = mapper.Map<List<MedicamentoDto>>(entidad.registros);
        return new Pager<MedicamentoDto>(listEntidad, entidad.totalRegistros, medicamentoParams.PageIndex, medicamentoParams.PageSize, medicamentoParams.Search);
    }

    [HttpGet("consulta-5")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MedicamentoPrecio50000()
    {
        var entidad = await unitofwork.Medicamentos.MedicamentoPrecio50000();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpGet("consulta-5")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<object>>> MedicamentoPrecio50000Paginated([FromQuery] Params mascotaParams)
    {
        var entidad = await unitofwork.Medicamentos.MedicamentoPrecio50000Paginated(mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
        var listEntidad = mapper.Map<List<object>>(entidad.registros); 
        return new Pager<object>(listEntidad, entidad.totalRegistros, mascotaParams.PageIndex, mascotaParams.PageSize, mascotaParams.Search);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicamento>> Post(MedicamentoDto entidadDto)
    {
        var entidad = this.mapper.Map<Medicamento>(entidadDto);
        this.unitofwork.Medicamentos.Add(entidad);
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
    public async Task<ActionResult<MedicamentoDto>> Put(int id, [FromBody]MedicamentoDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Medicamento>(entidadDto);
        unitofwork.Medicamentos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Medicamentos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Medicamentos.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}