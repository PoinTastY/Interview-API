using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClienteAPI
{
    public class Program
    {
        
       
        public static async Task Main(string[] args)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri("URL");

                //add product
                var producto = new Producto
                {
                    Id = 0,
                    Nombre = "Producto 1",
                    Precio = 10.99m
                };

                var content = new StringContent(JsonSerializer.Serialize(producto), Encoding.UTF8, "application/json");

                Console.WriteLine("Prueba de get sin Autorizacion:");

                var responseNoAuth = await client.PostAsync("api/productos", content);

                if (responseNoAuth.IsSuccessStatusCode)
                {
                    Console.WriteLine("Producto agregado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"Error al agregar el producto. {responseNoAuth.ReasonPhrase}");
                }

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "KEY");

                Console.WriteLine("Prueba de get con Autorizacion:");

                var responseGood = await client.PostAsync("api/productos", content);

                if (responseGood.IsSuccessStatusCode)
                {
                    Console.WriteLine("Producto agregado exitosamente.");
                }
                else
                {
                    Console.WriteLine($"Error al agregar el producto. {responseGood.ReasonPhrase}");
                }

                //get products
                var responseGet = await client.GetAsync("api/productos");
                if (responseGet.IsSuccessStatusCode)
                {
                    var json = await responseGet.Content.ReadAsStringAsync();
                    var productos = JsonSerializer.Deserialize<List<Producto>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    foreach (var product in productos!)
                    {
                        Console.WriteLine($"Id: {product.Id} - Nombre: {product.Nombre} - Precio: {product.Precio}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error al obtener los productos. {responseGet.ReasonPhrase}");
                }
            }
            catch         (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}