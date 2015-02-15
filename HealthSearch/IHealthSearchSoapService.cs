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

        #endregion

        #region Localizacao

        [OperationContract]
        List<Localizacao> GetAllLocalizacao();

        [OperationContract]
        Localizacao GetLocalizacaoById(string id);

        [OperationContract]
        bool updateLocalizacao(Localizacao localizacao);

        #endregion

        #region Servico

        [OperationContract]
        List<Servico> GetAllServico();

        [OperationContract]
        Servico GetServicoById(string id);

        [OperationContract]
        bool updateServico(Servico servico);

        #endregion
    }
}
