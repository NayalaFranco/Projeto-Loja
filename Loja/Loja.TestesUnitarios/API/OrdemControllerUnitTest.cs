using Loja.Domain.Enums;
using Loja.Domain.Validation;

namespace Loja.TestesUnitarios.API
{
    public class OrdensControllerUnitTest
    {

        private IMapper mapper;
        private OrdemRepository ordemRepository;
        private IOrdemService ordemService;
        private ProdutoRepository produtoRepository;
        private IProdutoService produtoService;
        private ClienteRepository clienteRepository;
        private IClienteService clienteService;
        private VendedorRepository vendedorRepository;
        private IVendedorService vendedorService;
        private OrdensController controller;
        private PagingParameters genericParameters;

        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; }

        static OrdensControllerUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("dbLojaOrdensTeste")
                .Options;
        }

        public OrdensControllerUnitTest()
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToDTOMappingProfile());
            });
            mapper = configMapper.CreateMapper();

            var context = new ApplicationDbContext(dbContextOptions);

            DBUnitTestsInitializer db = new DBUnitTestsInitializer();
            db.Seed(context);

            ordemRepository = new OrdemRepository(context);
            produtoRepository = new ProdutoRepository(context);
            clienteRepository = new ClienteRepository(context);
            vendedorRepository = new VendedorRepository(context);

            ordemService = new OrdemService(ordemRepository);
            produtoService = new ProdutoService(produtoRepository);
            clienteService = new ClienteService(clienteRepository);
            vendedorService = new VendedorService(vendedorRepository);

            controller = new OrdensController(mapper, ordemService, produtoService, clienteService, vendedorService);

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
            Assert.IsType<List<OrdemDTO>>(okObjResult.Value);
        }


        [Fact]
        public async Task Get_MatchResult()
        {
            // Arrange
            PagingParameters tresItensPorPagina = new PagingParameters()
            {
                PageNumber = 1,
                PageSize = 3,
                OrderedBy = "id"
            };

            // Act
            var data = await controller.Get(tresItensPorPagina);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            var ordens = okObjResult.Value.Should().BeAssignableTo<List<OrdemDTO>>().Subject;

            Assert.Equal(3, ordens.Count);

            Assert.Equal(1, ordens[0].Id);
            Assert.Equal(1, ordens[0].VendedorId);
            Assert.Equal(1, ordens[0].ClienteId);
            Assert.Equal(5526.63M, ordens[0].Total);
            Assert.Equal(EnumStatusVenda.AguardandoPagamento, ordens[0].StatusVenda);
            Assert.Equal(DateTime.Parse("2023-06-03"), ordens[0].DataCriacao);

            Assert.Equal(2, ordens[1].Id);
            Assert.Equal(2, ordens[1].VendedorId);
            Assert.Equal(1, ordens[1].ClienteId);
            Assert.Equal(164.06M, ordens[1].Total);
            Assert.Equal(EnumStatusVenda.AguardandoPagamento, ordens[1].StatusVenda);
            Assert.Equal(DateTime.Parse("2023-05-09"), ordens[1].DataCriacao);

        }

        /******************* TESTE GET BY ID *********************/
        [Fact]
        public async Task GetById_Return_OkObjectResult()
        {
            // Arrange
            int ordemId = 1;

            // Act
            var data = await controller.Get(ordemId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            Assert.IsType<OrdemDTO>(okObjResult.Value);
        }

        [Fact]
        public async Task GetById_Return_NotFound()
        {
            // Arrange
            int ordemId = 777;

            // Act
            var data = await controller.Get(ordemId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact]
        public async Task GetById_Return_Include_OrdemProduto()
        {
            // Arrange
            int ordemId = 2;

            // Act
            var data = await controller.Get(ordemId);

            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(data.Result);
            var ordem = Assert.IsType<OrdemDTO>(okObjResult.Value);


            Assert.Equal(2, ordem.Id);
            Assert.Equal(2, ordem.VendedorId);
            Assert.Equal(1, ordem.ClienteId);
            Assert.Equal(164.06M, ordem.Total);
            Assert.Equal(DateTime.Parse("2023-05-09"), ordem.DataCriacao);

            var produtos = Assert.IsType<List<OrdemProdutoDTO>>(ordem.Produtos);

            Assert.Equal(2, produtos[0].OrdemId);
            Assert.Equal(8, produtos[0].ProdutoId);
            Assert.Equal("Pct. c/ 5 meias cores diversas Tam.43", produtos[0].NomeProduto);
            Assert.Equal(21.90M, produtos[0].PrecoUnitario);
            Assert.Equal(2, produtos[0].Quantidade);
            Assert.Equal(2.19M, produtos[0].Desconto);

            Assert.Equal(2, produtos[1].OrdemId);
            Assert.Equal(6, produtos[1].ProdutoId);
            Assert.Equal("Jaqueta Unissex Tam.G", produtos[1].NomeProduto);
            Assert.Equal(128.90M, produtos[1].PrecoUnitario);
            Assert.Equal(1, produtos[1].Quantidade);
            Assert.Equal(6.45M, produtos[1].Desconto);
        }

        /*********************** TESTE POST **********************/
        [Fact]
        public async Task Post_Add_ValidData_Return_CreatedResult()
        {
            // Arrange

            var listProdutos = new List<OrdemProdutoDTO>
            {
                new OrdemProdutoDTO
                {
                    ProdutoId = 7,
                    Quantidade = 2,
                    Desconto = 13.98M

                },
                new OrdemProdutoDTO
                {
                    ProdutoId = 6,
                    Quantidade = 1,
                    Desconto = 12.89M
                }
            };

            var ordem = new OrdemDTO()
            {
                VendedorId = 2,
                ClienteId = 1,
                Produtos = listProdutos
            };

            // Act
            var data = await controller.Post(ordem);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(data);
            var result = createdResult.Value.Should().BeAssignableTo<OrdemDTO>().Subject;
            Assert.NotEqual(0, result.Id);

            var ordemGet = await controller.Get(result.Id);
            var ordemObjResult = Assert.IsType<OkObjectResult>(ordemGet.Result);
            var ordemData = Assert.IsType<OrdemDTO>(ordemObjResult.Value);
            Assert.Equal(2, ordemData.VendedorId);
            Assert.Equal(1, ordemData.ClienteId);
            Assert.Equal(241.83M, ordemData.Total);
            Assert.Equal(EnumStatusVenda.AguardandoPagamento, ordemData.StatusVenda);

            var produtos = Assert.IsType<List<OrdemProdutoDTO>>(ordemData.Produtos);

            Assert.Equal(7, produtos[0].ProdutoId);
            Assert.Equal("Calça Jeans Fem.", produtos[0].NomeProduto);
            Assert.Equal(69.90M, produtos[0].PrecoUnitario);

            Assert.Equal(6, produtos[1].ProdutoId);
            Assert.Equal("Jaqueta Unissex", produtos[1].NomeProduto);
            Assert.Equal(128.90M, produtos[1].PrecoUnitario);

        }

        /*********************** TESTE PUT ***********************/
        [Fact]
        public async Task Put_Update_StatusVenda_Return_OkObjectResult()
        {
            // Arrange
            int ordemId = 3;
            var status = EnumStatusVenda.PagamentoAprovado;

            // Act

            var updatedData = await controller.Put(ordemId, status);


            // Assert
            var okObjResult = Assert.IsType<OkObjectResult>(updatedData);
            var updatedOrdem = okObjResult.Value.Should().BeAssignableTo<OrdemDTO>().Subject;
            Assert.Equal(ordemId, updatedOrdem.Id);
            Assert.Equal(16.35M, updatedOrdem.Total);

            Assert.Equal(status, updatedOrdem.StatusVenda);
        }

        [Fact]
        public async Task Put_Update_StatusVenda_Return_DomainExceptionValidation()
        {
            // Arrange
            var ordemId = 2;
            var status = EnumStatusVenda.Entregue;

            // Assert
            var ex = await Assert.ThrowsAsync<DomainExceptionValidation>(async () => await controller.Put(ordemId, status));

            Assert.Equal("Transição de Status Inválida", ex.Message);
        }

        /********************* TESTE DELETE **********************/
        [Fact]
        public async Task Delete_Return_OkResult()
        {
            // Arrange
            var ordemId = 4;

            // Act
            var data = await controller.Delete(ordemId);

            // Assert
            Assert.IsType<OkResult>(data.Result);
        }
    }
}