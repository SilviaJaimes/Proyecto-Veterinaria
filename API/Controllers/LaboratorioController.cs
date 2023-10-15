using API.Dtos;
using API.Helpers.Errors;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class LaboratorioController : BaseApiController
{
    private readonly IUnitOfWork unitofwork;
    private readonly  IMapper mapper;

    public LaboratorioController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<LaboratorioDto>>> Get()
    {
        var entidad = await unitofwork.Laboratorios.GetAllAsync();
        return mapper.Map<List<LaboratorioDto>>(entidad);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LaboratorioDto>> Get(int id)
    {
        var entidad = await unitofwork.Laboratorios.GetByIdAsync(id);
        if (entidad == null){
            return NotFound();
        }
        return this.mapper.Map<LaboratorioDto>(entidad);
    }

    [HttpGet("paginacion")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<LaboratorioDto>>> GetPaginacion([FromQuery] Params laboratorioParams)
    {
        var entidad = await unitofwork.Laboratorios.GetAllAsync(laboratorioParams.PageIndex, laboratorioParams.PageSize, laboratorioParams.Search);
        var listEntidad = mapper.Map<List<LaboratorioDto>>(entidad.registros);
        return new Pager<LaboratorioDto>(listEntidad, entidad.totalRegistros, laboratorioParams.PageIndex, laboratorioParams.PageSize, laboratorioParams.Search);
    }

    [HttpGet("consulta-2/{Laboratorio}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<object>> MedicamentoLaboratorio(string Laboratorio)
    {
        var entidad = await unitofwork.Laboratorios.MedicamentoLaboratorio(Laboratorio);
        var dto = mapper.Map<IEnumerable<object>>(entidad);
        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Laboratorio>> Post(LaboratorioDto entidadDto)
    {
        var entidad = this.mapper.Map<Laboratorio>(entidadDto);
        this.unitofwork.Laboratorios.Add(entidad);
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
    public async Task<ActionResult<LaboratorioDto>> Put(int id, [FromBody]LaboratorioDto entidadDto){
        if(entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<Laboratorio>(entidadDto);
        unitofwork.Laboratorios.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var entidad = await unitofwork.Laboratorios.GetByIdAsync(id);
        if(entidad == null)
        {
            return NotFound();
        }
        unitofwork.Laboratorios.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }
}