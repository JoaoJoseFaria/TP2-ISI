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
    /// Interface serviço REST
    /// </summary>
    [ServiceContract]
    public interface IHealthSearchRestService
    {
        #region XML

        #region Prestador

        /// <summary>
        /// Devolve todos os prestadores
        /// </summary>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Xml")]
        List<Prestador> GetAllPrestadorXml();

        /// <summary>
        /// Devolve os prestadores por id
        /// </summary>
        /// <param name="id">Id do prestador</param>
        /// <returns>Prestador</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/{id}/Xml")]
        Prestador GetPrestadorByIdXml(string id);

        /// <summary>
        /// Devolve os prestadores por serviço
        /// </summary>
        /// <param name="id">Id do serviço</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/servico/{id}/Xml")]
        List<Prestador> GetPestadorByServicoXml(string id);

        /// <summary>
        /// Devolve os prestadores pela localização
        /// </summary>
        /// <param name="pais">Pais a procurar</param>
        /// <param name="regiao">Região a procurar</param>
        /// <param name="distrito">Distrito a procurar</param>
        /// <param name="concelho">Concelho a procurar</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/PrestadoresLocalizacao/Xml?pais={pais}&regiao={regiao}&distrito={distrito}&concelho={concelho}")]
        List<Prestador> GetPestadorByLocalizacaoXml(string pais = null, string regiao = null,
                            string distrito = null, string concelho = null);

        /// <summary>
        /// Actualiza dados do prestador
        /// </summary>
        /// <param name="prestador">Prestador actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Xml")]
        bool UpdatePrestadorXml(Prestador prestador);

        /// <summary>
        /// Marca prestador como eliminado
        /// </summary>
        /// <param name="id">Id do restador a marcar</param>
        /// <returns>True caso actualiza, false caso contrário<</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/{id}/Xml")]
        bool DeletePrestadorXml(string id);

        /// <summary>
        /// Elimina registo do prestador da base de dados
        /// </summary>
        /// <param name="id">Id do prestador a eliminar</param>
        /// <returns>True caso elimina, false caso contrário<</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Hard/{id}/Xml")]
        bool HardDeletePrestadorXml(string id);

        /// <summary>
        /// Adiciona novo prestador
        /// </summary>
        /// <param name="prestador">Prestador a adicionar</param>
        /// <returns>Id novo prestador</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Xml")]
        int AddPrestadorXml(Prestador prestador);

        #endregion

        #region Localizacao

        /// <summary>
        /// Devolve todas as localizações
        /// </summary>
        /// <returns>Lista de localizações</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Xml")]
        List<Localizacao> GetAllLocalizacaoXml();

        /// <summary>
        /// Devolve localização pelo id
        /// </summary>
        /// <param name="id">Id da localização</param>
        /// <returns>Localização</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/{id}/Xml")]
        Localizacao GetLocalizacaoByIdXml(string id);

        /// <summary>
        /// Actualiza uma localização
        /// </summary>
        /// <param name="localizacao">Localização actualizada</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Xml")]
        bool UpdateLocalizacaoXml(Localizacao localizacao);

        /// <summary>
        /// Marca localização como eliminada
        /// </summary>
        /// <param name="id">Id da localização a marcar</param>
        /// <returns>True caso marcada, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/{id}/Xml")]
        bool DeleteLocalizacaoXml(string id);

        /// <summary>
        /// Elimina localização da base de dados
        /// </summary>
        /// <param name="id">Id da localização a eliminar</param>
        /// <returns>True caso eliminada, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Hard/{id}/Xml")]
        bool HardDeleteLocalizacaoXml(string id);

        /// <summary>
        /// Adiciona uma localização
        /// </summary>
        /// <param name="localizacao">Localização a adicionar</param>
        /// <returns>Id nova localização</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Xml")]
        int AddLocalizacaoXml(Localizacao localizacao);

        #endregion

        #region Servico

        /// <summary>
        /// Devolve todos os serviços
        /// </summary>
        /// <returns>Lista de serviços</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Xml")]
        List<Servico> GetAllServicoXml();

        /// <summary>
        /// Devolve serviço por id
        /// </summary>
        /// <param name="id">Id do serviço a pesquisar</param>
        /// <returns>Serviço devolvido</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/{id}/Xml")]
        Servico GetServicoByIdXml(string id);

        /// <summary>
        /// Actualiza um serviço
        /// </summary>
        /// <param name="servico">Serviço actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Xml")]
        bool UpdateServicoXml(Servico servico);

        /// <summary>
        /// Marca serviço como eliminado
        /// </summary>
        /// <param name="id">Id do serviço a marcar</param>
        /// <returns>True caso marque, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/{id}/Xml")]
        bool DeleteServicoXml(string id);

        /// <summary>
        /// Remove o serviço da base de dados
        /// </summary>
        /// <param name="id">Id do serviço a remover</param>
        /// <returns>True caso remova, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Hard/{id}/Xml")]
        bool HardDeleteServicoXml(string id);

        /// <summary>
        /// Adiciona novo serviço na base de dados
        /// </summary>
        /// <param name="servico">Serviço a adicionar</param>
        /// <returns>Id do novo serviço</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Xml")]
        int AddServicoXml(Servico servico);

        #endregion

        #region PrestadorServico

        /// <summary>
        /// Adiciona um serviço a um prestador
        /// </summary>
        /// <param name="prestadorServico"></param>
        /// <returns>True caso adicione, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/PrestadorServico/Xml")]
        bool AddPrestadorServicoXml(PrestadorServico prestadorServico);

        #endregion

        #region Manutenção

        /// <summary>
        /// Limpa todos os registos da base de dados que estão marcados como eliminados
        /// </summary>
        /// <param name="data">Data a partir da qual está marcada como eliminada</param>
        /// <returns>-1 em caso de sucesso</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/LimparTabelas/{data}/Xml")]
        int LimparTabelasXml(string data);

        #endregion

        #endregion

        #region JSON

        #region Prestador

        /// <summary>
        /// Devolve todos os prestadores
        /// </summary>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Json")]
        List<Prestador> GetAllPrestadorJson();

        /// <summary>
        /// Devolve os prestadores por id
        /// </summary>
        /// <param name="id">Id do prestador</param>
        /// <returns>Prestador</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/{id}/Json")]
        Prestador GetPrestadorByIdJson(string id);

        /// <summary>
        /// Devolve os prestadores por serviço
        /// </summary>
        /// <param name="id">Id do serviço</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/servico/{id}/Json")]
        List<Prestador> GetPestadorByServicoJson(string id);

        /// <summary>
        /// Devolve os prestadores pela localização
        /// </summary>
        /// <param name="pais">Pais a procurar</param>
        /// <param name="regiao">Região a procurar</param>
        /// <param name="distrito">Distrito a procurar</param>
        /// <param name="concelho">Concelho a procurar</param>
        /// <returns>Lista de prestadores</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/PrestadoresLocalizacao/Json?pais={pais}&regiao={regiao}&distrito={distrito}&concelho={concelho}")]
        List<Prestador> GetPestadorByLocalizacaoJson(string pais = null, string regiao = null,
                            string distrito = null, string concelho = null);

        /// <summary>
        /// Actualiza dados do prestador
        /// </summary>
        /// <param name="prestador">Prestador actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Json")]
        bool UpdatePrestadorJson(Prestador prestador);

        /// <summary>
        /// Marca prestador como eliminado
        /// </summary>
        /// <param name="id">Id do restador a marcar</param>
        /// <returns>True caso actualiza, false caso contrário<</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/{id}/Json")]
        bool DeletePrestadorJson(string id);

        /// <summary>
        /// Elimina registo do prestador da base de dados
        /// </summary>
        /// <param name="id">Id do prestador a eliminar</param>
        /// <returns>True caso elimina, false caso contrário<</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Hard/{id}/Json")]
        bool HardDeletePrestadorJson(string id);

        /// <summary>
        /// Adiciona novo prestador
        /// </summary>
        /// <param name="prestador">Prestador a adicionar</param>
        /// <returns>Id novo prestador</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Prestadores/Json")]
        int AddPrestadorJson(Prestador prestador);

        #endregion

        #region Localizacao

        /// <summary>
        /// Devolve todas as localizações
        /// </summary>
        /// <returns>Lista de localizações</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Json")]
        List<Localizacao> GetAllLocalizacaoJson();

        /// <summary>
        /// Devolve localização pelo id
        /// </summary>
        /// <param name="id">Id da localização</param>
        /// <returns>Localização</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/{id}/Json")]
        Localizacao GetLocalizacaoByIdJson(string id);

        /// <summary>
        /// Actualiza uma localização
        /// </summary>
        /// <param name="localizacao">Localização actualizada</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Json")]
        bool UpdateLocalizacaoJson(Localizacao localizacao);

        /// <summary>
        /// Marca localização como eliminada
        /// </summary>
        /// <param name="id">Id da localização a marcar</param>
        /// <returns>True caso marcada, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/{id}/Json")]
        bool DeleteLocalizacaoJson(string id);

        /// <summary>
        /// Elimina localização da base de dados
        /// </summary>
        /// <param name="id">Id da localização a eliminar</param>
        /// <returns>True caso eliminada, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Hard/{id}/Json")]
        bool HardDeleteLocalizacaoJson(string id);

        /// <summary>
        /// Adiciona uma localização
        /// </summary>
        /// <param name="localizacao">Localização a adicionar</param>
        /// <returns>Id nova localização</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Localizacao/Json")]
        int AddLocalizacaoJson(Localizacao localizacao);

        #endregion

        #region Servico

        /// <summary>
        /// Devolve todos os serviços
        /// </summary>
        /// <returns>Lista de serviços</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Json")]
        List<Servico> GetAllServicoJson();

        /// <summary>
        /// Devolve serviço por id
        /// </summary>
        /// <param name="id">Id do serviço a pesquisar</param>
        /// <returns>Serviço devolvido</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/{id}/Json")]
        Servico GetServicoByIdJson(string id);

        /// <summary>
        /// Actualiza um serviço
        /// </summary>
        /// <param name="servico">Serviço actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Json")]
        bool UpdateServicoJson(Servico servico);

        /// <summary>
        /// Marca serviço como eliminado
        /// </summary>
        /// <param name="id">Id do serviço a marcar</param>
        /// <returns>True caso marque, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/{id}/Json")]
        bool DeleteServicoJson(string id);

        /// <summary>
        /// Remove o serviço da base de dados
        /// </summary>
        /// <param name="id">Id do serviço a remover</param>
        /// <returns>True caso remova, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Hard/{id}/Json")]
        bool HardDeleteServicoJson(string id);

        /// <summary>
        /// Adiciona novo serviço na base de dados
        /// </summary>
        /// <param name="servico">Serviço a adicionar</param>
        /// <returns>Id do novo serviço</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/Servico/Json")]
        int AddServicoJson(Servico servico);

        #endregion

        #region PrestadorServico

        /// <summary>
        /// Adiciona um serviço a um prestador
        /// </summary>
        /// <param name="prestadorServico"></param>
        /// <returns>True caso adicione, false caso contrário</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/PrestadorServico/Json")]
        bool AddPrestadorServicoJson(PrestadorServico prestadorServico);

        #endregion

        #region Manutenção

        /// <summary>
        /// Limpa todos os registos da base de dados que estão marcados como eliminados
        /// </summary>
        /// <param name="data">Data a partir da qual está marcada como eliminada</param>
        /// <returns>-1 em caso de sucesso</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/LimparTabelas/{data}/Json")]
        int LimparTabelasJson(string data);

        #endregion

        #endregion
    }
}