using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HealthSearch
{
    /// <summary>
    /// Interface serviço SOAP
    /// </summary>
    [ServiceContract]
    public interface IHealthSearchSoapService
    {

        #region Prestador

        /// <summary>
        /// Devolve todos os prestadores
        /// </summary>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        List<Prestador> GetAllPrestador();

        /// <summary>
        /// Devolve os prestadores por id
        /// </summary>
        /// <param name="id">Id do prestador</param>
        /// <returns>Prestador</returns>
        [OperationContract]
        Prestador GetPrestadorById(string id);

        /// <summary>
        /// Devolve os prestadores por serviço
        /// </summary>
        /// <param name="id">Id do serviço</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        List<Prestador> GetPestadorByServico(string id);

        /// <summary>
        /// Devolve os prestadores pela localização
        /// </summary>
        /// <param name="pais">Pais a procurar</param>
        /// <param name="regiao">Região a procurar</param>
        /// <param name="distrito">Distrito a procurar</param>
        /// <param name="concelho">Concelho a procurar</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        List<Prestador> GetPestadorByLocalizacaoo(string pais = null, string regiao = null,
                            string distrito = null, string concelho = null);

        /// <summary>
        /// Actualiza dados do prestador
        /// </summary>
        /// <param name="prestador">Prestador actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        bool UpdatePrestador(Prestador prestador);

        /// <summary>
        /// Marca prestador como eliminado
        /// </summary>
        /// <param name="id">Id do restador a marcar</param>
        /// <returns>True caso actualiza, false caso contrário<</returns>
        [OperationContract]
        bool DeletePrestador(string id);

        /// <summary>
        /// Elimina registo do prestador da base de dados
        /// </summary>
        /// <param name="id">Id do prestador a eliminar</param>
        /// <returns>True caso elimina, false caso contrário<</returns>
        [OperationContract]
        bool HardDeletePrestador(string id);

        /// <summary>
        /// Adiciona novo prestador
        /// </summary>
        /// <param name="prestador">Prestador a adicionar</param>
        /// <returns>Id novo prestador</returns>
        [OperationContract]
        int AddPrestador(Prestador prestador);

        #endregion

        #region Localizacao

        [OperationContract]
        List<Localizacao> GetAllLocalizacao();

        [OperationContract]
        Localizacao GetLocalizacaoById(string id);

        [OperationContract]
        bool UpdateLocalizacao(Localizacao localizacao);

        [OperationContract]
        bool DeleteLocalizacao(string id);

        [OperationContract]
        bool HardDeleteLocalizacao(string id);

        [OperationContract]
        int AddLocalizacao(Localizacao localizacao);

        #endregion

        #region Servico

        [OperationContract]
        List<Servico> GetAllServico();

        [OperationContract]
        Servico GetServicoById(string id);

        [OperationContract]
        bool UpdateServico(Servico servico);

        [OperationContract]
        bool DeleteServico(string id);

        [OperationContract]
        bool HardDeleteServico(string id);

        [OperationContract]
        int AddServico(Servico servico);

        #endregion

        #region PrestadorServico

        [OperationContract]
        bool AddPrestadorServico(PrestadorServico prestadorServico);

        #endregion

        #region Manutenção

        [OperationContract]
        int LimparTabelas(string data); 
        
        #endregion
    }
}
