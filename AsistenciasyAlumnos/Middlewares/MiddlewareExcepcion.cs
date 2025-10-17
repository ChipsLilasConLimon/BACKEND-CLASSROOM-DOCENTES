using System.ComponentModel.DataAnnotations;
using System.Data;
using TextAPI_D311.Excepciones;
using TextAPI_D311.Modelos;

namespace TextAPI_D311.Middlewares
{
    public class MiddlewareExcepcion
    {
        private readonly RequestDelegate _next;

        public MiddlewareExcepcion(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // TODO DE VALIDACION previa
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                int statusCode = ex switch
                {
                    NoDataExcepcion => StatusCodes.Status404NotFound,
                    ValidacionExcepcion => StatusCodes.Status400BadRequest,
                    Exception => StatusCodes.Status500InternalServerError,
                    _ => StatusCodes.Status500InternalServerError
                };
                var respuesta = new Respuesta(
                    mensaje: ex.Message
                    );
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(respuesta);
            }
            // TODO validacion posterior
        }

    }
}
