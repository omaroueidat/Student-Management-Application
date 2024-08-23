using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models.DTO;
using StudentMVC.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace StudentMVC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public StudentController(ILogger<StudentController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
                catch (Exception e)
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
                catch (Exception e)
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
                    // Prepare the requests
                    HttpRequestMessage requestRegions = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Region"),
                        Method = HttpMethod.Get
                    };

                    HttpRequestMessage requestAddressCodeValues = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/CodeValues/1"),
                        Method = HttpMethod.Get
                    };

                    HttpRequestMessage requestContactCodeValues = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/CodeValues/2"),
                        Method = HttpMethod.Get
                    };

                    // Send the request and revieve response
                    HttpResponseMessage regionsResponse = await httpClient.SendAsync(requestRegions);
                    HttpResponseMessage addressTypesResponse = await httpClient.SendAsync(requestAddressCodeValues);
                    HttpResponseMessage contactTypesResponse = await httpClient.SendAsync(requestContactCodeValues);

                    // Check the status code of the response
                    if (regionsResponse.StatusCode != System.Net.HttpStatusCode.OK || addressTypesResponse.StatusCode != System.Net.HttpStatusCode.OK || contactTypesResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("A Server Error Occured. Please try again later!");
                    }

                    // Read the response body
                    Stream regionStream = regionsResponse.Content.ReadAsStream();
                    Stream addressTypeStream = addressTypesResponse.Content.ReadAsStream();
                    Stream contactTypeStream = contactTypesResponse.Content.ReadAsStream();

                    StreamReader region = new StreamReader(regionStream);
                    StreamReader address = new StreamReader(addressTypeStream);
                    StreamReader contact = new StreamReader(contactTypeStream);

                    string regionsResponseBody = region.ReadToEnd();
                    string addressTypesResponseBody = address.ReadToEnd();
                    string contactTypesResponseBody = contact.ReadToEnd();

                    // Options for name case sensitvty and null values during deserialization
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Matches JSON properties case insensitively
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        AllowTrailingCommas = true,
                        ReadCommentHandling = JsonCommentHandling.Skip
                    };

                    // Deserialize the json response in Body
                    List<Region>? regions = JsonSerializer.Deserialize<List<Region>>(regionsResponseBody, options);
                    List<CodeValue>? addressTypes = JsonSerializer.Deserialize<List<CodeValue>>(addressTypesResponseBody, options);
                    List<CodeValue>? contactTypes = JsonSerializer.Deserialize<List<CodeValue>>(contactTypesResponseBody, options);

                    ViewBag.Regions = regions;
                    ViewBag.AddressTypes = addressTypes;
                    ViewBag.ContactTypes = contactTypes;


                    return View();
                }
            }
            catch (Exception e)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(StudentRequest student, string AddressData, string ContactData)
        {
            if (string.IsNullOrEmpty(AddressData))
            {
                ViewBag.ErrorMessage = "You Should Enter at least one Address";
                return RedirectToActionPermanent("Create", "Student");
            }

            if (string.IsNullOrEmpty(ContactData))
            {
                ViewBag.ErrorMessage = "You Should Enter at least one Contact";
                return RedirectToActionPermanent("Create", "Student");
            }

            List<AddressRequest>? addresses = JsonSerializer.Deserialize<List<AddressRequest>>(AddressData);

            List<ContactRequest>? contacts = JsonSerializer.Deserialize<List<ContactRequest>>(ContactData);

            if (addresses is null || contacts is null)
            {
                // return error view
                ViewBag.Message = "Unexpected Error Occured! Please try again later";
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            student.Addresses = addresses;
            student.Contacts = contacts;

            var studentJson = JsonSerializer.Serialize(student);

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                try
                {
                    // Prepare the Request
                    HttpRequestMessage studentInsertRequest = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Student"),
                        Method = HttpMethod.Post,
                        Content = new StringContent(studentJson, Encoding.UTF8, "application/json")
                    };

                    // Send the Request an recieve a response
                    HttpResponseMessage response = await httpClient.SendAsync(studentInsertRequest);

                    // check the status code of the response
                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    {
                        throw new Exception("Unexpected Error Occured! Please try again later");
                    }

                    return RedirectToActionPermanent("Index", "Student");
                }
                catch (Exception e)
                {
                    // return error view
                    ViewBag.Message = e.Message;
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

            }

        }

        [HttpGet]
        [Route("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                try
                {
                    // Prepare the Request
                    HttpRequestMessage studentGetRequest = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Student/{id}"),
                        Method = HttpMethod.Get
                    };

                    // Send the Request an recieve a response
                    HttpResponseMessage response = await httpClient.SendAsync(studentGetRequest);

                    // check the status code of the response
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("Unexpected Error Occured! Please try again later");
                    }

                    // Read the response body
                    Stream studentStream = response.Content.ReadAsStream();

                    StreamReader studentReader = new StreamReader(studentStream);

                    string responseBody = studentReader.ReadToEnd();

                    // Options for name case sensitvty and null values during deserialization
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true, // Matches JSON properties case insensitively
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        AllowTrailingCommas = true,
                        ReadCommentHandling = JsonCommentHandling.Skip
                    };

                    // Deserialize the json response in Body
                    StudentResponse? student = JsonSerializer.Deserialize<StudentResponse>(responseBody, options);

                    // Get regions, AddressTypes and ContactTypes for the select menu
                    // Prepare the requests
                    HttpRequestMessage requestRegions = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Region"),
                        Method = HttpMethod.Get
                    };

                    HttpRequestMessage requestAddressCodeValues = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/CodeValues/1"),
                        Method = HttpMethod.Get
                    };

                    HttpRequestMessage requestContactCodeValues = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/CodeValues/2"),
                        Method = HttpMethod.Get
                    };

                    // Send the request and revieve response
                    HttpResponseMessage regionsResponse = await httpClient.SendAsync(requestRegions);
                    HttpResponseMessage addressTypesResponse = await httpClient.SendAsync(requestAddressCodeValues);
                    HttpResponseMessage contactTypesResponse = await httpClient.SendAsync(requestContactCodeValues);

                    // Check the status code of the response
                    if (regionsResponse.StatusCode != System.Net.HttpStatusCode.OK || addressTypesResponse.StatusCode != System.Net.HttpStatusCode.OK || contactTypesResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new Exception("A Server Error Occured. Please try again later!");
                    }

                    // Read the response body
                    Stream regionStream = regionsResponse.Content.ReadAsStream();
                    Stream addressTypeStream = addressTypesResponse.Content.ReadAsStream();
                    Stream contactTypeStream = contactTypesResponse.Content.ReadAsStream();

                    StreamReader region = new StreamReader(regionStream);
                    StreamReader address = new StreamReader(addressTypeStream);
                    StreamReader contact = new StreamReader(contactTypeStream);

                    string regionsResponseBody = region.ReadToEnd();
                    string addressTypesResponseBody = address.ReadToEnd();
                    string contactTypesResponseBody = contact.ReadToEnd();


                    // Deserialize the json response in Body
                    List<Region>? regions = JsonSerializer.Deserialize<List<Region>>(regionsResponseBody, options);
                    List<CodeValue>? addressTypes = JsonSerializer.Deserialize<List<CodeValue>>(addressTypesResponseBody, options);
                    List<CodeValue>? contactTypes = JsonSerializer.Deserialize<List<CodeValue>>(contactTypesResponseBody, options);

                    ViewBag.Regions = regions;
                    ViewBag.AddressTypes = addressTypes;
                    ViewBag.ContactTypes = contactTypes;

                    return View(student);
                }
                catch (Exception e)
                {
                    // return error view
                    ViewBag.Message = e.Message;
                    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }
        }

        [HttpPost]
        [Route("[action]/{id:guid}")]
        public async Task<IActionResult> Edit(StudentUpdateRequest student, Guid id, string AddressData, string ContactData)
        {

            if (string.IsNullOrEmpty(AddressData))
            {
                ViewBag.ErrorMessage = "You Should Enter at least one Address";
                return RedirectToActionPermanent("Edit", "Student");
            }

            if (string.IsNullOrEmpty(ContactData))
            {
                ViewBag.ErrorMessage = "You Should Enter at least one Contact";
                return RedirectToActionPermanent("Edit", "Student");
            }

            List<Address>? addresses = JsonSerializer.Deserialize<List<Address>>(AddressData);

            List<Contact>? contacts = JsonSerializer.Deserialize<List<Contact>>(ContactData);

            if (addresses is null || contacts is null)
            {
                // return error view
                ViewBag.Message = "Unexpected Error Occured! Please try again later";
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            student.Addresses = addresses;
            student.Contacts = contacts;

            var studentJson = JsonSerializer.Serialize(student);

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                try
                {
                    // Prepare the Request
                    HttpRequestMessage studentInsertRequest = new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"{_configuration["ApiUri"]}/api/Student/{id}"),
                        Method = HttpMethod.Put,
                        Content = new StringContent(studentJson, Encoding.UTF8, "application/json")
                    };

                    // Send the Request an recieve a response
                    HttpResponseMessage response = await httpClient.SendAsync(studentInsertRequest);

                    // check the status code of the response
                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                    {
                        throw new Exception("Unexpected Error Occured! Please try again later");
                    }

                    return RedirectToActionPermanent("Index", "Student");
                }
                catch (Exception e)
                {
                    // return error view
                    ViewBag.Message = e.Message;
                    // log the error
                    _logger.LogError(e.Message, e);
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

            }
        }
    }
}
