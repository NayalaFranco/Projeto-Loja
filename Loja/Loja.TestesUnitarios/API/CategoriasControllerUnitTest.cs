namespace Loja.TestesUnitarios.API
{
    public class CategoriasControllerUnitTest
    {

        private IMapper mapper;
        private CategoriaRepository categoriaRepository;
        private ICategoriaService categoriaService;
        private CategoriasController controller;
        private PagingParameters genericParameters;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        static CategoriasControllerUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbLojaCategoriasTest")
                .Options;
        }

        public CategoriasControllerUnitTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = configMapper.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);

            DBUnitTestsInitializer db = new DBUnitTestsInitializer();
            db.Seed(context);

            categoriaRepository = new CategoriaRepository(context);

            categoriaService = new CategoriaService(categoriaRepository);

            controller = new CategoriasController(mapper, categoriaService);

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
            Assert.IsType<List<CategoriaDTO>>(okObjResult.Value);
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
            var cat = okObjResult.Value.Should().BeAssignableTo<List<CategoriaDTO>>().Subject;

            Assert.Equal(2, cat.Count);

            Assert.Equal("Material Escolar", cat[0].Nome);
            Assert.Equal("Materiais escolar diversos", cat[0].Descricao);
            Assert.Equal("materialEscolarCat.jpg", cat[0].ImagemUrl);

            Assert.Equal("Roupas", cat[1].Nome);
            Assert.Equal("Roupas Masculinas e Femininas", cat[1].Descricao);
            Assert.Equal("roupasCat.jpg", cat[1].ImagemUrl);
        }

        /******************* TESTE GET BY ID *********************/
        [Fact]
        public async Task GetById_Return_OkObjectResult()
        {
            // Arrange
            int catId = 2;

            // Act
            var data = await controller.Get(catId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<CategoriaDTO>(okObjResult.Value);
        }

        [Fact]
        public async Task GetById_Return_NotFound()
        {
            // Arrange
            int catId = 777;

            // Act
            var data = await controller.Get(catId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        /*********************** TESTE POST **********************/
        [Fact]
        public async Task Post_Add_ValidData_Return_CreatedResult()
        {
            // Arrange
            var cat = new CategoriaDTO()
            {
                Nome = "Teste Unitario Post",
                Descricao = "Teste unitario",
                ImagemUrl = "teste.jpg"
            };

            // Act
            var data = await controller.Post(cat);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(data);
            Assert.IsType<CategoriaDTO>(createdResult.Value);
            var result = createdResult.Value.Should().BeAssignableTo<CategoriaDTO>().Subject;
            Assert.NotEqual(0, result.Id);
        }

        /*********************** TESTE PUT ***********************/
        [Fact]
        public async Task Put_Update_ValidData_Return_OkObjectResult()
        {
            // Arrange
            var catId = 3;

            var catUpdate = new CategoriaDTO()
            {
                Id = catId,
                Nome = "Cat Atualizada",
                Descricao = "Teste unitario 1",
                ImagemUrl = "imagemTeste.jpg"
            };

            // Act
            var updatedData = await controller.Put(catId, catUpdate);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(updatedData);
            var updatedCat = okObjResult.Value.Should().BeAssignableTo<CategoriaDTO>().Subject;
            Assert.Equal(catUpdate, updatedCat);
        }


        /********************* TESTE DELETE **********************/
        [Fact]
        public async Task Delete_Return_OkResult()
        {
            // Arrange
            var catId = 4;

            // Act
            var data = await controller.Delete(catId);

            // Assert
            Assert.IsType<OkResult>(data.Result);
        }
    }
}