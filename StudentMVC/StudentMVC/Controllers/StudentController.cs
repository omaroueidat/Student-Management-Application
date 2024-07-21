using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models.DTO;
using StudentMVC.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;

namespace StudentMVC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public StudentController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Request the students from the API
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Student"),
                    Method = HttpMethod.Get
                };

                try
                {

                    // Send the request to api and recieve the response
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                    // Read the response using stream
                    Stream stream = httpResponseMessage.Content.ReadAsStream();

                    StreamReader sr = new StreamReader(stream);

                    string response = sr.ReadToEnd();

				    // Options for name case sensitvty and null values during deserialization
				    var options = new JsonSerializerOptions
				    {
					    PropertyNameCaseInsensitive = true, // Matches JSON properties case insensitively
					    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
					    AllowTrailingCommas = true,
					    ReadCommentHandling = JsonCommentHandling.Skip
				    };

				    // Transform the json into StudentResponse collection object
				    List<StudentResponse>? students = JsonSerializer.Deserialize<List<StudentResponse>>(response, options);

                    if (students is null)
                    {
                        return View("Error");
                    }

                    ViewBag.Students = students;
                } 
                catch(Exception e)
                {
                    ViewBag.Message = e.Message;
					return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }


                return View();
            }

        }

        [HttpGet]
        [Route("[action]/{StudentId:guid}")]
        public async Task<IActionResult> Delete(Guid? StudentId)
        {
            if (StudentId is null)
            {
                return View("Error");
            }

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Prepare the Delete Request
                HttpRequestMessage httpDeleteRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Student/{StudentId}"),
                    Method = HttpMethod.Delete
                };

                // Send the request to the Student API
                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpDeleteRequest);

                // Check if the Response contains any error
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ViewBag.StudentId = StudentId;
                    return View("404");
                }

                if (httpResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
					return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
				}


                // else return the view student to redirect to the student index page to refresh the page with the new data
                return RedirectToAction("Index", "Student");
            }
            
        }

        [HttpGet]
        [Route("ViewDetails/{StudentId:guid}")]
        public async Task<IActionResult> ViewDetails(Guid StudentId)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // Prepare the request
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_configuration["ApiUri"]}/api/student/{StudentId}"),
                    Method = HttpMethod.Get
                };

                try 
                {
                    // Send the request to the api
                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                    if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ViewBag.StudentId = StudentId;
                        return View("404");
                    }

                    // Read the response using stream
					Stream stream = httpResponseMessage.Content.ReadAsStream();

					StreamReader sr = new StreamReader(stream);

					string response = sr.ReadToEnd();

					// Options for name case sensitvty and null values during deserialization
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true, // Matches JSON properties case insensitively
						DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
						AllowTrailingCommas = true,
						ReadCommentHandling = JsonCommentHandling.Skip
					};

                    StudentResponse? student = JsonSerializer.Deserialize<StudentResponse>(response, options);

                    if (student is null)
                    {
                        throw new Exception();
                    }


                    return View(student);
				}
                catch(Exception e)
                {
                    return RedirectPermanent("Student/Index");
				}


            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Create()
        {
            try
            {
                using (HttpClient httpClient = _httpClientFactory.CreateClient())
                {
                    // Prepare the request
                    HttpRequestMessage request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Region"),
                        Method = HttpMethod.Get
                    };

                    // Send the request and revieve response
                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    // Check the status code of the response
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("A Server Error Occured. Please try again later!");
                    }

                    // Read the response body
                    Stream stream = response.Content.ReadAsStream();

                    StreamReader sr = new StreamReader(stream);

                    string responseBody = sr.ReadToEnd();

                    // Options for name case sensitvty and null values during deserialization
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Matches JSON properties case insensitively
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        AllowTrailingCommas = true,
                        ReadCommentHandling = JsonCommentHandling.Skip
                    };

                    // Deserialize the json response in Body
                    List<Region>? regions = JsonSerializer.Deserialize<List<Region>>(responseBody, options);

                    ViewBag.Regions = regions;

                    return View();
                }
            } 
            catch(Exception e)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }


    }
}
