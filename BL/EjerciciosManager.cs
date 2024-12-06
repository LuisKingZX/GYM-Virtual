using DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public class EjerciciosManager
    {
        public async Task<HttpResponseListadoEjercicios> obtenerEjercicios()
        {
            HttpCustomClient httpClient = new HttpCustomClient();

            string asignacionResponse = await httpClient.get("api/consultarEjercicios");
            HttpResponseListadoEjercicios rutinas = JsonSerializer.Deserialize<HttpResponseListadoEjercicios>(asignacionResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            try
            {
                return rutinas;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
