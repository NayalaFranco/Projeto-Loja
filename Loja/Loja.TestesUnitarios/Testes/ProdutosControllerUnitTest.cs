namespace Loja.TestesUnitarios.Testes
{
    public class ProdutosControllerUnitTest
    {

        private IMapper mapper;
        private ProdutoRepository produtoRepository;
        private CategoriaRepository categoriaRepository;
        private IProdutoService produtoService;
        private ICategoriaService categoriaService;
        private ProdutosController controller;
        private PagingParameters genericParameters;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        static ProdutosControllerUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbLojaProdutosTest")
                .Options;
        }

        public ProdutosControllerUnitTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = configMapper.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);

            DBUnitTestsInitializer db = new DBUnitTestsInitializer();
            db.Seed(context);

            produtoRepository = new ProdutoRepository(context);
            categoriaRepository = new CategoriaRepository(context);

            produtoService = new ProdutoService(produtoRepository);
            categoriaService = new CategoriaService(categoriaRepository);

            controller = new ProdutosController(mapper, produtoService, categoriaService);

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
            Assert.IsType<List<ProdutoDTO>>(okObjResult.Value);
        }


        [Fact]
        public async Task Get_MatchResult()
        {
            // Arrange
            PagingParameters doisItemPorPagina = new PagingParameters()
            {
                PageNumber = 1,
                PageSize = 3,
                OrderedBy = "id"
            };

            // Act
            var data = await controller.Get(doisItemPorPagina);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            var prod = okObjResult.Value.Should().BeAssignableTo<List<ProdutoDTO>>().Subject;

            Assert.Equal(3, prod.Count);

            Assert.Equal("Caderno espiral", prod[0].Nome);
            Assert.Equal("Caderno espiral 100 folhas", prod[0].Descricao);
            Assert.Equal(7.45M, prod[0].Preco);
            Assert.Equal("caderno1.jpg", prod[0].ImagemUrl);
            Assert.Equal(50, prod[0].Estoque);
            Assert.Equal(DateTime.Parse("2023-05-02"), prod[0].DataCadastro);
            Assert.Equal(1, prod[0].CategoriaId);

            Assert.Equal("Estojo escolar", prod[1].Nome);
            Assert.Equal("Estojo escolar cinza", prod[1].Descricao);
            Assert.Equal(5.65M, prod[1].Preco);
            Assert.Equal("estojo1.jpg", prod[1].ImagemUrl);
            Assert.Equal(70, prod[1].Estoque);
            Assert.Equal(DateTime.Parse("2023-04-02"), prod[1].DataCadastro);
            Assert.Equal(1, prod[1].CategoriaId);
        }

        /******************* TESTE GET BY ID *********************/
        [Fact]
        public async Task GetById_Return_OkObjectResult()
        {
            // Arrange
            int prodId = 2;

            // Act
            var data = await controller.Get(prodId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<ProdutoDTO>(okObjResult.Value);
        }

        [Fact]
        public async Task GetById_Return_NotFound()
        {
            // Arrange
            int prodId = 777;

            // Act
            var data = await controller.Get(prodId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        /*********************** TESTE POST **********************/
        [Fact]
        public async Task Post_Add_ValidData_Return_CreatedResult()
        {
            // Arrange
            var prod = new ProdutoDTO()
            {
                Nome = "Teste Unitario Post",
                Descricao = "Teste unitario",
                Preco = 1.11M,
                ImagemUrl = "teste.jpg",
                Estoque = 2,
                DataCadastro = DateTime.Parse("2023-06-29"),
                CategoriaId = 2
            };

            // Act
            var data = await controller.Post(prod);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(data);
            Assert.IsType<ProdutoDTO>(createdResult.Value);
            var result = createdResult.Value.Should().BeAssignableTo<ProdutoDTO>().Subject;
            Assert.NotEqual(0, result.Id);
        }

        /*********************** TESTE PUT ***********************/
        [Fact]
        public async Task Put_Update_ValidData_Return_OkObjectResult()
        {
            // Arrange
            var prodId = 4;

            var prodUpdate = new ProdutoDTO()
            {
                Id = prodId,
                Nome = "Calculadora atualizada",
                Descricao = "Teste unitario Produtos",
                Preco = 15.99M,
                ImagemUrl = "imagemTeste.jpg",
                Estoque = 15,
                DataCadastro = DateTime.Parse("2023-04-29"),
                CategoriaId = 1
            };

            // Act
            var updatedData = await controller.Put(prodId, prodUpdate);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(updatedData);
            var updatedCat = okObjResult.Value.Should().BeAssignableTo<ProdutoDTO>().Subject;
            Assert.Equal(prodUpdate, updatedCat);
        }


        /********************* TESTE DELETE **********************/
        [Fact]
        public async Task Delete_Return_OkResult()
        {
            // Arrange
            var prodId = 5;

            // Act
            var data = await controller.Delete(prodId);

            // Assert
            Assert.IsType<OkResult>(data.Result);
        }
    }
}