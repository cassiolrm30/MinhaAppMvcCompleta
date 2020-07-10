using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(Contexto contexto) : base(contexto) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await contexto.Enderecos.AsNoTracking().FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
        }
    }
}