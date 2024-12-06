﻿
using DTO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public class MedidasManager
    {
        public async Task<ResponseHTTPMedida> registrarMedida(DTO.Dtos.MedidaCorporal medida)
        {
            HttpCustomClient httpClient = new HttpCustomClient();
            JsonContent jsonCon = JsonContent.Create(medida);
            string categoriasResponse = await httpClient.post("api/RegistrarMedida", jsonCon);
            ResponseHTTPMedida usuario = JsonSerializer.Deserialize<ResponseHTTPMedida>(categoriasResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            try
            {
                return usuario;
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
