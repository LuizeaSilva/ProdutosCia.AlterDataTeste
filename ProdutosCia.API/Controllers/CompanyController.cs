using Microsoft.AspNetCore.Mvc;
using ProdutosCia.API.Controllers.Base;
using ProdutosCia.Application.Dtos.Companies.Request;
using ProdutosCia.Application.Dtos.Companies.Response;
using ProdutosCia.Application.Dtos.CompanyProducts.Request;
using ProdutosCia.Application.Dtos.CompanyProducts.Response;
using ProdutosCia.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ProdutosCia.API.Controllers;

public class CompanyController(ICompanyService companyService, ICompanyProductService companyProductService) : BaseController
{
    private readonly ICompanyService _companyService = companyService;
    private readonly ICompanyProductService _companyProductService = companyProductService;

    [HttpGet]
    [SwaggerResponse(200, "Ok", typeof(List<CompanyResponse>))]
    [SwaggerResponse(204, "No data")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _companyService.GetAll(cancellationToken);
        
        if(!companies.Any())
            return NoContent();

        return Ok(companies);
    }
    
    [HttpGet("{id}")]
    [SwaggerResponse(200, "Ok", typeof(CompanyResponse))]
    [SwaggerResponse(404, "Not Found")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _companyService.GetById(id, cancellationToken);
        
        if(company == null)
            return NotFound();

        return Ok(company);
    }
    
    [HttpPost]
    [SwaggerResponse(201, "Created", typeof(CompanyResponse))]
    public async Task<IActionResult> Create(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var response = await _companyService.Create(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }
    
    [HttpDelete("{id}")]
    [SwaggerResponse(204, "Deleted")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _companyService.Delete(id, cancellationToken);
        return NoContent();
    }
    
    [HttpPut("{id}")]
    [SwaggerResponse(204, "Updated")]
    public async Task<IActionResult> Put(Guid id, PutCompanyRequest request, CancellationToken cancellationToken)
    {
        await _companyService.Put(id, request, cancellationToken);
        return NoContent();
    }
   
    [HttpPost("{id}/Product/{productId}")]
    [SwaggerResponse(200, "Ok", typeof(CompanyProductResponse))]
    public async Task<IActionResult> CreateCompanyProduct(Guid id, Guid productId, CreateCompanyProductRequest request, CancellationToken cancellationToken)
    { 
        request.ProductId = productId;
        request.CompanyId = id;
            
        var response = await _companyProductService.Create(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPatch("{id}/Product/{productId}/quantity/increase")]
    [SwaggerResponse(204, "Updated")]
    public async Task<IActionResult> IncreaseProductQuantityForCompany(Guid id, Guid productId, IncreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken)
    {
        request.ProductId = productId;
        request.CompanyId = id;
        
        await _companyProductService.IncreaseQuantity(request, cancellationToken);
        return NoContent();
    }
    
    [HttpPatch("{id}/Product/{productId}/quantity/decrease")]
    [SwaggerResponse(204, "Updated")]
    public async Task<IActionResult> DecreaseProductQuantityForCompany(Guid id, Guid productId, DecreaseQuantityCompanyProductRequest request, CancellationToken cancellationToken)
    {
        request.ProductId = productId;
        request.CompanyId = id;
        
        await _companyProductService.DecreaseQuantity(request, cancellationToken);
        return NoContent();
    }
    
    [HttpGet("{id}/Stock/total-value")]
    [SwaggerResponse(200, "Ok", typeof(CompanyStockTotalValueResponse))]
    public async Task<IActionResult> GetStockTotalValues(Guid id, CancellationToken cancellationToken)
    {
        var stockTotalValue = await _companyService.GetStockTotalValue(id, cancellationToken);
        return Ok(stockTotalValue);
    }
    
    [HttpGet("{id}/Stock/total-product")]
    [SwaggerResponse(200, "Ok", typeof( CompanyStockTotalProductResponse))]
    public async Task<IActionResult> GetStockTotalProduct(Guid id, CancellationToken cancellationToken)
    {
        var stockTotalProduct = await _companyService.GetStockTotalProduct(id, cancellationToken);
        return Ok(stockTotalProduct);
    }
    
    [HttpGet("{id}/Stock/average-cost")]
    [SwaggerResponse(200, "Ok", typeof(CompanyStockAverageCostResponse))]
    public async Task<IActionResult> GetStockAverageCost(Guid id, CancellationToken cancellationToken)
    {
        var stockAverageCost = await _companyService.GetStockAverageCost(id, cancellationToken);
        return Ok(stockAverageCost);
    }
    
    [HttpGet("{id}/Product/{productId}/average-cost")]
    [SwaggerResponse(200, "Ok", typeof(CompanyProductAverageCostResponse))]
    public async Task<IActionResult> GetProductAverageCost(Guid id, Guid productId, CancellationToken cancellationToken)
    {
        var productAverageCost = await _companyProductService.GetProductAverageCost(id,  productId, cancellationToken);
        return Ok(productAverageCost);
    }
}