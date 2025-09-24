using CacaMantos.Admin.API.Domain.Exceptions;
using CacaMantos.Admin.API.Presentation.DTO;

namespace CacaMantos.Admin.API.Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException dex)
            {
                await ErroValidacao(context, dex);
            }
            catch (Exception ex)
            {
                await ErroNaoTratado(context, ex);
            }
        }

        private async Task ErroValidacao(HttpContext context, DomainException dex)
        {
            _logger.LogError(dex, "Erro de validação ao processar requisição {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorResponseDTO(StatusCodes.Status400BadRequest, dex.Message));
        }
        

        private async Task ErroNaoTratado(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao processar requisição {Path}", context.Request.Path);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new ErrorResponseDTO(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}