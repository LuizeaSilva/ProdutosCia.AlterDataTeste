using Microsoft.AspNetCore.Mvc;
using ProdutosCia.API.Controllers.Base;
using ProdutosCia.Application.Dtos;
using ProdutosCia.Application.Dtos.Produtcts.Request;
using ProdutosCia.Application.Dtos.Produtcts.Response;
using ProdutosCia.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProdutosCia.API.Controllers;

public class ProductController(IProductService productService) : BaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    [SwaggerResponse(200, "Ok", typeof(List<ProductResponse>))]
    [SwaggerResponse(204, "No data")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAll(cancellationToken);
        
        if(!products.Any())
            return NoContent();

        return Ok(products);
    }
    
    [HttpGet("{id}")]
    [SwaggerResponse(200, "Ok", typeof(ProductResponse))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetById(id, cancellationToken);
        
        if(product == null)
            return NotFound();

        return Ok(product);
    }
    
    [HttpPost]
    [SwaggerResponse(201, "Created", typeof(ProductResponse))]
    public async Task<IActionResult> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var response = await _productService.Create(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }
    
    [HttpPost("Bulk-Insert")]
    [SwaggerResponse(200, "Ok", typeof(List<BulkCreateResponse>))]
    [SwaggerResponse(400, "Bad Request for all inserts", typeof(List<BulkCreateResponse>))]
    [SwaggerResponse(207, "Inserted but some wasn't because validation failed", typeof(List<BulkCreateResponse>))]
    public async Task<IActionResult> BulkCreate(List<CreateProductRequest> request, CancellationToken cancellationToken)
    {
        var response = await _productService.BulkCreate(request, cancellationToken);

        if (response.All(x => x.StatusCode == 200))
            return Ok(response);
        else if(response.All(x => x.StatusCode == 400))
            return BadRequest(response);
        else
            return StatusCode(207, response);
    }
    
    [HttpDelete("{id}")]
    [SwaggerResponse(204, "Deleted")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _productService.Delete(id, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    [SwaggerResponse(204, "Updated")]
    public async Task<IActionResult> Put(Guid id, PutProductRequest request, CancellationToken cancellationToken)
    {
        await _productService.Put(id, request, cancellationToken);
        return NoContent();
    }
}