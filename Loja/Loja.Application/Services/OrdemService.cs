using Loja.Application.Interfaces;
using Loja.Domain.Entities;
using Loja.Domain.Enums;
using Loja.Domain.Interfaces;
using Loja.Domain.PaginationEntities;
using System.Linq.Expressions;

namespace Loja.Application.Services
{
    public class OrdemService : IOrdemService
    {
        private readonly IOrdemRepository _ordemRepository;
        public OrdemService(IOrdemRepository ordemRepository)
        {
            _ordemRepository = ordemRepository ??
                throw new ArgumentNullException(nameof(ordemRepository));
        }

        /// <summary>
        /// Obtém uma ordem pelo Id.
        /// </summary>
        /// <param name="id">Id da Ordem.</param>
        /// <returns>Retorna uma Ordem.</returns>
        public async Task<Ordem> GetById(int? id)
        {
            var ordemEntity = await _ordemRepository.GetByIdAsync(x => x.Id == id);
            return ordemEntity;
        }

        /// <summary>
        /// Obtém uma ordem pelo Id.
        /// </summary>
        /// <param name="id">Id da Ordem.</param>
        /// <returns>Retorna uma Ordem com os Produtos inclusos.</returns>
        public async Task<Ordem> GetOrdemComProdutosById(int? id)
        {
            var ordemEntity = await _ordemRepository.GetOrdemByIdIncluiProdutoAsync(x => x.Id == id);
            return ordemEntity;
        }

        /// <summary>
        /// Obtém uma lista paginada com as Ordens.
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <returns>Retorna um objeto PagingList com a lista de Ordens e os dados de paginação</returns>
        public async Task<PagingList<Ordem>> GetOrdens(PagingParameters parameters)
        {
            var pagingList = await _ordemRepository.GetAsync(parameters);

            return pagingList;
        }

        /// <summary>
        /// Obtém uma lista paginada com as Ordens.
        /// <br>Pode ser filtrada pelo Id de vendedor e/ou cliente.</br>
        /// </summary>
        /// <param name="parameters">Objeto com os parâmetros de paginação</param>
        /// <param name="predicate">Delegate com o critério de busca</param>
        /// <returns>Retorna um objeto PagingList com a lista de Ordens e os dados de paginação</returns>
        public async Task<PagingList<Ordem>> GetOrdens(PagingParameters parameters, Expression<Func<Ordem, bool>> predicate)
        {
            var pagingList = await _ordemRepository.GetAsync(parameters);

            return pagingList;
        }

        /// <summary>
        /// Adiciona uma ordem à tabela do banco de dados.
        /// </summary>
        /// <param name="ordemNova">Objeto com os dados da ordem a ser adicionada.</param>
        /// <returns>Retorna a ordem adicionada.</returns>
        public async Task<Ordem> Add(Ordem ordemNova)
        {
            var ordem = await _ordemRepository.CreateAsync(ordemNova);
            return ordem;
        }

        /// <summary>
        /// Atualiza o status de uma Ordem.
        /// </summary>
        /// <param name="id">Id da Ordem.</param>
        /// <param name="statusVenda">Novo status.</param>
        /// <returns>Retorna a ordem atualizada.</returns>
        public async Task<Ordem> UpdateStatus(int id, EnumStatusVenda statusVenda)
        {
            Ordem ordem = _ordemRepository.GetByIdAsync(x => x.Id == id).Result;

            ordem.UpdateStatus(statusVenda);

            var ordemAtualizada = await _ordemRepository.UpdateAsync(ordem);
            return ordemAtualizada;
        }

        /// <summary>
        /// Remove uma ordem do banco de dados.
        /// </summary>
        /// <param name="id">Id da ordem.</param>
        public async Task Remove(int? id)
        {
            var ordemEntity = _ordemRepository.GetByIdAsync(x => x.Id == id).Result;
            if (ordemEntity == null)
                return;

            await _ordemRepository.RemoveAsync(ordemEntity);
        }
    }
}
