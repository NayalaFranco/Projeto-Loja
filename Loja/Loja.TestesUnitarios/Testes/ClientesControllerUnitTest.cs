namespace Loja.TestesUnitarios.Testes
{
    public class ClientesControllerUnitTest
    {

        private IMapper mapper;
        private ClienteRepository clienteRepository;
        private IClienteService clienteService;
        private ClientesController controller;
        private PagingParameters genericParameters;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        static ClientesControllerUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbLojaClientesTeste")
                .Options;
        }

        public ClientesControllerUnitTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = configMapper.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);

            DBUnitTestsInitializer db = new DBUnitTestsInitializer();
            db.Seed(context);

            clienteRepository = new ClienteRepository(context);

            clienteService = new ClienteService(mapper, clienteRepository);

            controller = new ClientesController(clienteService);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };

            genericParameters = new PagingParameters()
            {
                PageNumber = 1,
                PageSize = 10,
                OrderedBy = "id"
            };
        }

        /*********************** TESTE GET ***********************/
        [Fact]
        public async Task Get_Return_OkObjectResult()
        {
            // Act
            var data = await controller.Get(genericParameters);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<List<ClienteDTO>>(okObjResult.Value);
        }


        [Fact]
        public async Task Get_MatchResult()
        {
            // Arrange
            PagingParameters doisItemPorPagina = new PagingParameters()
            {
                PageNumber = 1,
                PageSize = 2,
                OrderedBy = "id"
            };

            // Act
            var data = await controller.Get(doisItemPorPagina);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            var clientes = okObjResult.Value.Should().BeAssignableTo<List<ClienteDTO>>().Subject;

            Assert.Equal(2, clientes.Count);

            Assert.Equal(1, clientes[0].Id);
            Assert.Equal("Amado Amoroso", clientes[0].Nome);
            Assert.Equal("026.122.460-37", clientes[0].Cpf);
            Assert.Equal(DateTime.Parse("1978-01-11"), clientes[0].Nascimento);
            Assert.Equal("R.Endereço qualquer. N.02", clientes[0].Endereco);
            Assert.Equal("email@email.com", clientes[0].Email);
            Assert.Equal("(01) 2-3456-7891", clientes[0].Telefone);
            Assert.Equal(DateTime.Parse("2023-08-23"), clientes[0].DataCadastro);

            Assert.Equal(2, clientes[1].Id);
            Assert.Equal("Chaplin da Silva", clientes[1].Nome);
            Assert.Equal("218.461.780-61", clientes[1].Cpf);
            Assert.Equal(DateTime.Parse("1999-06-12"), clientes[1].Nascimento);
            Assert.Equal("R.Endereço qualquer. N.04", clientes[1].Endereco);
            Assert.Equal("email@email.com", clientes[1].Email);
            Assert.Equal("(11) 1-1111-1111", clientes[1].Telefone);
            Assert.Equal(DateTime.Parse("2021-01-11"), clientes[1].DataCadastro);
        }

        /******************* TESTE GET BY ID *********************/
        [Fact]
        public async Task GetById_Return_OkObjectResult()
        {
            // Arrange
            int clienteId = 2;

            // Act
            var data = await controller.Get(clienteId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<ClienteDTO>(okObjResult.Value);
        }

        [Fact]
        public async Task GetById_Return_NotFound()
        {
            // Arrange
            int clienteId = 777;

            // Act
            var data = await controller.Get(clienteId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        /*********************** TESTE POST **********************/
        [Fact]
        public async Task Post_Add_ValidData_Return_CreatedResult()
        {
            // Arrange
            var cliente = new ClienteDTO()
            {
                Nome = "Teste Unitario Post",
                Cpf = "000.000.000-00",
                Nascimento = DateTime.Parse("01/01/2001"),
                Endereco = "Rua do Teste N.02",
                Email = "email@email.com",
                Telefone = "1234-5678",
                DataCadastro = DateTime.Parse("02/02/2022")
            };

            // Act
            var data = await controller.Post(cliente);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(data);
            Assert.IsType<ClienteDTO>(createdResult.Value);
            var result = createdResult.Value.Should().BeAssignableTo<ClienteDTO>().Subject;
            Assert.NotEqual(0, result.Id);
        }

        /*********************** TESTE PUT ***********************/
        [Fact]
        public async Task Put_Update_ValidData_Return_OkObjectResult()
        {
            // Arrange
            var clienteId = 3;

            // Act
            var getCliente = await controller.Get(clienteId);
            var clienteResult = getCliente.Result.Should().BeAssignableTo<OkObjectResult>().Subject;
            var cliente = clienteResult.Value.Should().BeAssignableTo<ClienteDTO>().Subject;

            var clienteUpdate = new ClienteDTO()
            {
                Id = clienteId,
                Nome = "Senhor Atualizado",
                Cpf = cliente.Cpf,
                Nascimento = cliente.Nascimento,
                Endereco = "Rua do Teste N.03",
                Email = "emailAtualizado@NovoEmail.com",
                Telefone = cliente.Telefone,
                DataCadastro = cliente.DataCadastro
            };

            var updatedData = await controller.Put(clienteId, clienteUpdate);

            var getUpdatedRequest = await controller.Get(clienteId);
            var getUpdatedResult = getUpdatedRequest.Result.Should().BeAssignableTo<OkObjectResult>().Subject;
            var getUpdated = getUpdatedResult.Value.Should().BeAssignableTo<ClienteDTO>().Subject;

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(updatedData);
            var updatedCliente = okObjResult.Value.Should().BeAssignableTo<ClienteDTO>().Subject;
            Assert.Equal(clienteUpdate, updatedCliente);

            Assert.Equal(getUpdated.Id, updatedCliente.Id);
            Assert.Equal(getUpdated.Nome, updatedCliente.Nome);
            Assert.Equal(getUpdated.Cpf, updatedCliente.Cpf);
            Assert.Equal(getUpdated.Nascimento, updatedCliente.Nascimento);
            Assert.Equal(getUpdated.Endereco, updatedCliente.Endereco);
            Assert.Equal(getUpdated.Email, updatedCliente.Email);
            Assert.Equal(getUpdated.Telefone, updatedCliente.Telefone);
            Assert.Equal(getUpdated.DataCadastro, updatedCliente.DataCadastro);
        }


        /********************* TESTE DELETE **********************/
        [Fact]
        public async Task Delete_Return_OkObjectResult_And_NotFound()
        {
            // Arrange
            var clienteId = 4;

            // Act
            var data = await controller.Delete(clienteId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<ClienteDTO>(okObjectResult.Value);
            var getDeleted = await controller.Get(clienteId);
            Assert.IsType<NotFoundResult>(getDeleted.Result);
        }
    }
}