using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class MovimientoMedicamentoController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public MovimientoMedicamentoController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovimientoMedicamentoDto>>> Get()
    {
        var entidad = await unitofwork.MovimientoMedicamentos.GetAllAsync();
        return mapper.Map<List<MovimientoMedicamentoDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimientoMedicamentoDto>> Get(int id)
    {
        var entidad = await unitofwork.MovimientoMedicamentos.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<MovimientoMedicamentoDto>(entidad);
    }

    [HttpGet("paginacion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<MovimientoMedicamentoDto>>> GetPaginacion([FromQuery] Params movimientoMedicamentoParams)
    {
        var entidad = await unitofwork.MovimientoMedicamentos.GetAllAsync(movimientoMedicamentoParams.PageIndex, movimientoMedicamentoParams.PageSize, movimientoMedicamentoParams.Search);
        var listEntidad = mapper.Map<List<MovimientoMedicamentoDto>>(entidad.registros);
        return new Pager<MovimientoMedicamentoDto>(listEntidad, entidad.totalRegistros, movimientoMedicamentoParams.PageIndex, movimientoMedicamentoParams.PageSize, movimientoMedicamentoParams.Search);
    }

    [HttpGet("consulta-8")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MovimientosYValores()
    {
        var entidad = await unitofwork.MovimientoMedicamentos.MovimientosYValores();
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovimientoMedicamento>> Post(MovimientoMedicamentoDto entidadDto)
    {
        var entidad = this.mapper.Map<MovimientoMedicamento>(entidadDto);
        this.unitofwork.MovimientoMedicamentos.Add(entidad);
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
    public async Task<ActionResult<MovimientoMedicamentoDto>> Put(int id, [FromBody]MovimientoMedicamentoDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<MovimientoMedicamento>(entidadDto);
        unitofwork.MovimientoMedicamentos.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.MovimientoMedicamentos.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.MovimientoMedicamentos.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}