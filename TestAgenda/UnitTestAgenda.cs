using Agenda.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TestAgenda
{
    public class UnitTestAgenda
    {
        private readonly HttpClient _httpClient;
        private readonly string TokenFilePath = "token.json";
        private class TokenData
        {
            public string Token { get; set; }
        }

        public UnitTestAgenda()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7266");
        }

        private async System.Threading.Tasks.Task SetJwtTokenAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }        
        
        [Fact]
        public async System.Threading.Tasks.Task Login()
        {
            // Arrange
            var User = new User { UserName = "admin", Password = "admin" };
            var jsonContent = JsonConvert.SerializeObject(User);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseBody);
                var token = jsonDocument.RootElement.GetProperty("token").GetString();
                
                await SetJwtTokenAsync(token);  
            }

            // Assert
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task InsertRegisterAgendaTaskAsync()
        {
            // Arrange
            await Login();
            var jsonString = JsonConvert.SerializeObject(new Agenda.Infrastructure.Models.Task
            {
                Title = "Teste Agenda Base",
                Date = DateTime.Now.AddDays(5),
                Description = "Testando a agenda para uma tarefa base.",
                Status = EnumStatusTask.Pending
            });
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync("/Task", content);
            var item = await response.Content.ReadFromJsonAsync<Agenda.Infrastructure.Models.Task>();

            // Assert
            Assert.Equal(201, (int)response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTaskWhenTaskExists()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaTaskAsync();
            Agenda.Infrastructure.Models.Task task = new Agenda.Infrastructure.Models.Task();

            var response = await _httpClient.GetAsync("/Task/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var tasks = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Task>>();

            if (tasks != null && tasks.Count > 0)
                task = tasks[^1];

            task.Description = "Testando a agenda para uma tarefa atualizada.";

            var jsonString = JsonConvert.SerializeObject(task);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            response = await _httpClient.PutAsync($"/Task/{task.Id}", content);
            var item = await response.Content.ReadFromJsonAsync<Agenda.Infrastructure.Models.Task>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(200, (int)response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdReturnsTaskWhenTaskExists()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaTaskAsync();
            int taskId = 0;
            var response = await _httpClient.GetAsync("/Task/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var tasks = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Task>>();

            if (tasks != null && tasks.Count > 0)
                taskId = tasks[^1].Id;

            // Act
            response = await _httpClient.GetAsync($"/Task/{taskId}");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<Agenda.Infrastructure.Models.Task>(responseString);
            Assert.NotNull(task);
            Assert.Equal(taskId, task.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdReturnsNotFoundWhenTaskDoesNotExist()
        {
            // Arrange
            await Login();
            var taskId = -1;

            // Act
            var response = await _httpClient.GetAsync($"/Task/{taskId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllReturnsAllTasks()
        {
            // Arrange
            await Login();

            // Act
            await InsertRegisterAgendaTaskAsync();
            var response = await _httpClient.GetAsync("/Task/GetAll");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<Agenda.Infrastructure.Models.Task>>(responseString);
            Assert.NotNull(tasks);
            Assert.True(tasks.Count > 0); // Assumes there are tasks in the database
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateReturnsBadRequestWhenDateIsInvalid()
        {
            // Arrange
            await Login();
            var newTask = new Agenda.Infrastructure.Models.Task { Title = "Invalid Task", Date = DateTime.MinValue };
            var jsonContent = JsonConvert.SerializeObject(newTask);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Task", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteReturnsNoContentWhenTaskIsDeleted()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaTaskAsync();
            int taskId = 0;
            var response = await _httpClient.GetAsync("/Task/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var tasks = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Task>>();

            if (tasks != null && tasks.Count > 0)
                taskId = tasks[^1].Id;

            // Act
            response = await _httpClient.DeleteAsync($"/Task/{taskId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteReturnsNotFoundWhenTaskDoesNotExist()
        {
            // Arrange
            await Login();
            var taskId = -1;

            // Act
            var response = await _httpClient.DeleteAsync($"/Task/{taskId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task InsertRegisterAgendaContactAsync()
        {
            // Arrange
            await Login();
            var jsonString = JsonConvert.SerializeObject(new Agenda.Infrastructure.Models.Contact
            {
                Name = "Pedro Albuquerque",
                Phone = "+5565998761234",
                Email = "pedro@myemail.com"
            });
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _httpClient.PostAsync("/Contact", content);
            var item = await response.Content.ReadFromJsonAsync<Agenda.Infrastructure.Models.Contact>();

            // Assert
            Assert.Equal(201, (int)response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateContactWhenContactExists()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaContactAsync();
            Agenda.Infrastructure.Models.Contact contact = new Agenda.Infrastructure.Models.Contact();

            var response = await _httpClient.GetAsync("/Contact/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var contacts = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Contact>>();

            if (contacts != null && contacts.Count > 0)
                contact = contacts[^1];

            contact.Phone = "+5565998760000";

            var jsonString = JsonConvert.SerializeObject(contact);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            response = await _httpClient.PutAsync($"/Contact/{contact.Id}", content);
            var item = await response.Content.ReadFromJsonAsync<Agenda.Infrastructure.Models.Contact>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(200, (int)response.StatusCode);
        }


        [Fact]
        public async System.Threading.Tasks.Task CreateReturnsBadRequestWhenPhoneIsInvalid()
        {
            // Arrange
            await Login();
            var newContact = new Agenda.Infrastructure.Models.Contact { Name = "Phone Invalid", Phone = "O8176" };
            var jsonContent = JsonConvert.SerializeObject(newContact);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Contact", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateReturnsBadRequestWhenEmailIsInvalid()
        {
            // Arrange
            await Login();
            var newContact = new Agenda.Infrastructure.Models.Contact { Name = "Email Inválido", Email = "meuemail-myemail" };
            var jsonContent = JsonConvert.SerializeObject(newContact);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Contact", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdReturnsContactWhenContactExists()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaContactAsync();
            int contactId = 0;
            var response = await _httpClient.GetAsync("/Contact/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var contacts = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Contact>>();

            if (contacts != null && contacts.Count > 0)
                contactId = contacts[^1].Id;

            // Act
            response = await _httpClient.GetAsync($"/Contact/{contactId}");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            var contact = JsonConvert.DeserializeObject<Agenda.Infrastructure.Models.Contact>(responseString);
            Assert.NotNull(contact);
            Assert.Equal(contactId, contact.Id);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetByIdReturnsNotFoundWhenContactDoesNotExist()
        {
            // Arrange
            await Login();
            var contactId = -1;

            // Act
            var response = await _httpClient.GetAsync($"/Contact/{contactId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllReturnsAllContacts()
        {
            // Arrange
            await Login();

            // Act
            await InsertRegisterAgendaContactAsync();
            var response = await _httpClient.GetAsync("/Contact/GetAll");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var contacts = JsonConvert.DeserializeObject<List<Agenda.Infrastructure.Models.Contact>>(responseString);
            Assert.NotNull(contacts);
            Assert.True(contacts.Count > 0); 
        }
        
        [Fact]
        public async System.Threading.Tasks.Task DeleteReturnsNoContentWhenContactIsDeleted()
        {
            // Arrange
            await Login();
            await InsertRegisterAgendaContactAsync();
            int contactId = 0;
            var response = await _httpClient.GetAsync("/Contact/GetAll");

            Assert.True(response.IsSuccessStatusCode);

            var contacts = await response.Content.ReadFromJsonAsync<List<Agenda.Infrastructure.Models.Contact>>();

            if (contacts != null && contacts.Count > 0)
                contactId = contacts[^1].Id;

            // Act
            response = await _httpClient.DeleteAsync($"/Contact/{contactId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteReturnsNotFoundWhenContactDoesNotExist()
        {
            // Arrange
            await Login();
            var contactId = -1;

            // Act
            var response = await _httpClient.DeleteAsync($"/Contact/{contactId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}