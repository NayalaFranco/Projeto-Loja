namespace Loja.TestesUnitarios.API
{
    public class VendedoresControllerUnitTest
    {

        private IMapper mapper;
        private VendedorRepository vendedorRepository;
        private IVendedorService vendedorService;
        private VendedoresController controller;
        private PagingParameters genericParameters;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        static VendedoresControllerUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbLojaVendedoresTeste")
                .Options;
        }

        public VendedoresControllerUnitTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = configMapper.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);

            DBUnitTestsInitializer db = new DBUnitTestsInitializer();
            db.Seed(context);

            vendedorRepository = new VendedorRepository(context);

            vendedorService = new VendedorService(vendedorRepository);

            controller = new VendedoresController(mapper, vendedorService);

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
            Assert.IsType<List<VendedorDTO>>(okObjResult.Value);
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
            var vendedors = okObjResult.Value.Should().BeAssignableTo<List<VendedorDTO>>().Subject;

            Assert.Equal(2, vendedors.Count);

            Assert.Equal(1, vendedors[0].Id);
            Assert.Equal("Genivaldo Jovaldo", vendedors[0].Nome);
            Assert.Equal("565.703.380-99", vendedors[0].Cpf);
            Assert.Equal(DateTime.Parse("1990-08-20"), vendedors[0].Nascimento);
            Assert.Equal("R.Endereço qualquer. N.00", vendedors[0].Endereco);
            Assert.Equal("email@email.com", vendedors[0].Email);
            Assert.Equal("(00) 0-0000-0000", vendedors[0].Telefone);
            Assert.Equal(DateTime.Parse("2022-02-28"), vendedors[0].DataCadastro);

            Assert.Equal(2, vendedors[1].Id);
            Assert.Equal("Josenildo Gevildo", vendedors[1].Nome);
            Assert.Equal("380.751.090-74", vendedors[1].Cpf);
            Assert.Equal(DateTime.Parse("1978-01-11"), vendedors[1].Nascimento);
            Assert.Equal("R.Endereço qualquer. N.01", vendedors[1].Endereco);
            Assert.Equal("email@email.com", vendedors[1].Email);
            Assert.Equal("(01) 2-3456-7891", vendedors[1].Telefone);
            Assert.Equal(DateTime.Parse("2023-03-23"), vendedors[1].DataCadastro);
        }

        /******************* TESTE GET BY ID *********************/
        [Fact]
        public async Task GetById_Return_OkObjectResult()
        {
            // Arrange
            int vendedorId = 2;

            // Act
            var data = await controller.Get(vendedorId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<VendedorDTO>(okObjResult.Value);
        }

        [Fact]
        public async Task GetById_Return_NotFound()
        {
            // Arrange
            int vendedorId = 777;

            // Act
            var data = await controller.Get(vendedorId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        /*********************** TESTE POST **********************/
        [Fact]
        public async Task Post_Add_ValidData_Return_CreatedResult()
        {
            // Arrange
            var vendedor = new VendedorDTO()
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
            var data = await controller.Post(vendedor);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(data);
            Assert.IsType<VendedorDTO>(createdResult.Value);
            var result = createdResult.Value.Should().BeAssignableTo<VendedorDTO>().Subject;
            Assert.NotEqual(0, result.Id);
        }

        /*********************** TESTE PUT ***********************/
        [Fact]
        public async Task Put_Update_ValidData_Return_OkObjectResult()
        {
            // Arrange
            var vendedorId = 3;

            // Act
            var getVendedor = await controller.Get(vendedorId);
            var vendedorResult = getVendedor.Result.Should().BeAssignableTo<OkObjectResult>().Subject;
            var vendedor = vendedorResult.Value.Should().BeAssignableTo<VendedorDTO>().Subject;

            var vendedorUpdate = new VendedorDTO()
            {
                Id = vendedorId,
                Nome = "Vendedor Atualizado",
                Cpf = vendedor.Cpf,
                Nascimento = vendedor.Nascimento,
                Endereco = "Rua do Teste N.04",
                Email = "emailAtualizado@NovoEmail.com",
                Telefone = vendedor.Telefone,
                DataCadastro = vendedor.DataCadastro
            };

            var updatedData = await controller.Put(vendedorId, vendedorUpdate);

            var getUpdatedRequest = await controller.Get(vendedorId);
            var getUpdatedResult = getUpdatedRequest.Result.Should().BeAssignableTo<OkObjectResult>().Subject;
            var getUpdated = getUpdatedResult.Value.Should().BeAssignableTo<VendedorDTO>().Subject;

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(updatedData);
            var updatedVendedor = okObjResult.Value.Should().BeAssignableTo<VendedorDTO>().Subject;
            Assert.Equal(vendedorUpdate, updatedVendedor);

            Assert.Equal(getUpdated.Id, updatedVendedor.Id);
            Assert.Equal(getUpdated.Nome, updatedVendedor.Nome);
            Assert.Equal(getUpdated.Cpf, updatedVendedor.Cpf);
            Assert.Equal(getUpdated.Nascimento, updatedVendedor.Nascimento);
            Assert.Equal(getUpdated.Endereco, updatedVendedor.Endereco);
            Assert.Equal(getUpdated.Email, updatedVendedor.Email);
            Assert.Equal(getUpdated.Telefone, updatedVendedor.Telefone);
            Assert.Equal(getUpdated.DataCadastro, updatedVendedor.DataCadastro);
        }


        /********************* TESTE DELETE **********************/
        [Fact]
        public async Task Delete_Return_OkResult()
        {
            // Arrange
            var vendedorId = 4;

            // Act
            var data = await controller.Delete(vendedorId);

            // Assert
            Assert.IsType<OkResult>(data.Result);
        }
    }
}