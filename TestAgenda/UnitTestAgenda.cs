using Agenda.Infrastructure.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestAgenda
{ 
    public class UnitTestAgenda
    {
        private readonly HttpClient _httpClient;
        public UnitTestAgenda()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7266");
        }

        [Fact]
        public async System.Threading.Tasks.Task InsertRegisterAgendaTaskAsync()
        {
            // Arrange
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
        public async System.Threading.Tasks.Task GetByIdReturnsTaskWhenTaskExists()
        {
            // Arrange
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
            var taskId = -1;

            // Act
            var response = await _httpClient.GetAsync($"/Task/{taskId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllReturnsAllTasks()
        {
            // Act
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
        public async System.Threading.Tasks.Task CreateReturnsBadRequestWhenPhoneIsInvalid()
        {
            // Arrange
            var newContact = new Agenda.Infrastructure.Models.Contact { Name = "Phone Invalid", Phone = "O8176" };
            var jsonContent = JsonConvert.SerializeObject(newContact);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Contact", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateReturnsBadRequestWhenEmaileIsInvalid()
        {
            // Arrange
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
            var contactId = -1;

            // Act
            var response = await _httpClient.GetAsync($"/Contact/{contactId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetAllReturnsAllContacts()
        {
            // Act
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
            var contactId = -1;

            // Act
            var response = await _httpClient.DeleteAsync($"/Contact/{contactId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}