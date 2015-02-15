using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HealthSearch
{
    [ServiceContract]
    public interface IHealthSearchSoapService
    {

        #region Prestador

        [OperationContract]
        List<Prestador> GetAllPrestador();

        [OperationContract]
        Prestador GetPrestadorById(string id);

        [OperationContract]
        bool UpdatePrestador(Prestador prestador);

        [OperationContract]
        bool DeletePrestador(string id);

        [OperationContract]
        bool HardDeletePrestador(string id);

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
    }
}
