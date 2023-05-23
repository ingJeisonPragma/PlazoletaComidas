using Food.Domain.Business.DTO;
using Food.Domain.Interface.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Food.Transversal.Proxy
{
    public class HttpPetitionServices : IHttpPetitionServices
    {
        public async Task<StandardResponse> PetitionStandard(StandardRequest standard)
        {
            try
            {
                string Resultado = string.Empty;
                bool IsValid = true;

                //Valida que la URL que se va a realizar petición llegue con datos
                if (string.IsNullOrEmpty(standard.URL) || string.IsNullOrWhiteSpace(standard.URL))
                {
                    IsValid = false;
                    Resultado += "- La URL está vacía. \n";
                }

                //Valida que el metodo a consultar llegue con datos
                if (string.IsNullOrEmpty(standard.MethodName) || string.IsNullOrWhiteSpace(standard.MethodName))
                {
                    IsValid = false;
                    Resultado += "- El nombre del método está vacío.\n";
                }

                //Validar que los parametros de encabezado y valores tengan la misma cantidad de datos
                if (standard.HeaderParameters != null && standard.ValuesParameters != null)
                {
                    if (standard.HeaderParameters.Count != standard.ValuesParameters.Count)
                    {
                        IsValid = false;
                        Resultado += "- El número de parámetros de encabezado no corresponden al mismo número de parámetros de valores. \n";
                    }

                    if (standard.HeaderParameters.Count > 0 && standard.ValuesParameters.Count > 0)
                    {
                        standard.MethodName += "?";
                        for (int i = 0; i < standard.HeaderParameters.Count; i++)
                            standard.MethodName += standard.HeaderParameters[i] + "=" + standard.ValuesParameters[i] + "&";
                        standard.MethodName = standard.MethodName[0..^1];
                    }
                }

                //Se verifica que todos los parametros sean validos
                if (!IsValid)
                    return new StandardResponse
                    {
                        IsSuccess = false,
                        Message = Resultado,
                        Result = null
                    };

                //Si la petición es de integral se mandan los parametros por defecto

                JObject json = new JObject();
                HttpClient client = new();
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(standard.URL); //Se envia la URL del API que se va consultar
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); //Se agrega la variable de cabecera
                if (standard.IsAuthorize)
                {
                    if (string.IsNullOrEmpty(standard.TypeAuthorize))
                        return new StandardResponse
                        {
                            IsSuccess = false,
                            Message = "- Se requiere el Tipo de Autorización (TypeAuthorize)",
                            Result = null
                        };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(standard.TypeAuthorize, standard.Token);
                }

                if (standard.IsHeader)
                {
                    //Validar que los parametros de encabezado y valores tengan la misma cantidad de datos
                    if (standard.RequestHeader != null && standard.RequestValues != null)
                    {
                        if (standard.RequestHeader.Count != standard.RequestValues.Count)
                        {
                            IsValid = false;
                            Resultado += "- El número de parámetros para Header no corresponden al mismo número de parámetros de valores del Header. \n";
                        }

                        if (standard.RequestHeader.Count > 0 && standard.RequestValues.Count > 0)
                        {
                            for (int i = 0; i < standard.RequestHeader.Count; i++)
                                client.DefaultRequestHeaders.Add(standard.RequestHeader[i], standard.RequestValues[i]);
                        }
                    }
                }

                //Validación y ejecución del tipo de petición
                HttpResponseMessage response = new HttpResponseMessage();
                switch (standard.RequestType)
                {
                    case 1: //Metodo Get
                        response = await client.GetAsync(standard.MethodName);
                        break;
                    case 2: //Metodo Post
                        if (standard.ValueBody != null)
                        {
                            if (string.IsNullOrEmpty(standard.TypeBody))
                            {
                                return new StandardResponse
                                {
                                    IsSuccess = false,
                                    Message = "El Tipo de application es obligatorio (TypeBody) si la solicitud tiene un Body en la solicitud",
                                };
                            }
                            switch (standard.TypeBody.ToLower())
                            {
                                case "json":
                                    response = await client.PostAsync(standard.MethodName, new StringContent(JsonConvert.SerializeObject(standard.ValueBody), Encoding.UTF8, "application/json"));
                                    break;
                                case "x-www-form-urlencoded":
                                    response = await client.PostAsync(standard.MethodName, new FormUrlEncodedContent(JsonConvert.DeserializeObject<Dictionary<string, string>>(standard.ValueBody.ToString())));
                                    break;
                            }
                        }
                        else
                            response = await client.PostAsync(standard.MethodName, null);
                        break;
                    case 3: //Metodo PUT
                        response = standard.ValueBody != null ?
                            await client.PutAsync(standard.MethodName, new StringContent(JsonConvert.SerializeObject(standard.ValueBody), Encoding.UTF8, "application/json")) :
                            await client.PutAsync(standard.MethodName, null);
                        break;
                    case 4: //Metodo Delete
                        if (standard.ValueBody != null)
                        {
                            var request = new HttpRequestMessage
                            {
                                Method = HttpMethod.Delete,
                                RequestUri = new Uri(standard.URL + standard.MethodName),
                                Content = new StringContent(JsonConvert.SerializeObject(standard.ValueBody), Encoding.UTF8, "application/json"),
                            };
                            response = await client.SendAsync(request);
                        }
                        else
                            response = await client.DeleteAsync(standard.MethodName);
                        break;
                    default:
                        return new StandardResponse
                        {
                            IsSuccess = false,
                            Message = "El tipo de petición (RequestType) debe ser enviada con el código 1 (GET), 2 (POST), 3 (PUT) ó 4 (DELETE)",
                            Result = null
                        };
                }

                //Se transforma el resultado de la petición
                string result = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    if (standard.IsOwn)
                    {
                        json = JObject.Parse(result);
                        return JsonConvert.DeserializeObject<StandardResponse>(json.ToString());
                    }
                    else
                        return new StandardResponse
                        {
                            IsSuccess = true,
                            Message = "Consulta exitosa",
                            Result = result.ToString()
                        };
                }
                else
                    return new StandardResponse
                    {
                        IsSuccess = false,
                        Message = "No fue posible realizar la consulta.",
                        Result = result
                    };
            }
            catch (Exception ex)
            {
                return new StandardResponse
                {
                    IsSuccess = false,
                    Message = "Se presento un error al consultar los registros: \n" + ex.Message,
                    Result = new object()
                };
            }
        }
    }
}
