using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HealthSearch
{
    public class HealthSearchSoapService : IHealthSearchSoapService
    {

        #region Localizacao

        public List<Localizacao> GetAllLocalizacao()
        {
            try
            {
                using (var db = new HealthSearchEntitiesLocalizacao())
                {
                    return db.Localizacao.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Localizacao GetLocalizacaoById(string id)
        {
            try
            {
                int localizacaoId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesLocalizacao())
                {
                    return db.Localizacao.SingleOrDefault(p => p.id == localizacaoId);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool updateLocalizacao(Localizacao localizacao)
        {
            try
            {
                using (var db = new HealthSearchEntitiesLocalizacao())
                {

                    Localizacao aux = db.Localizacao.SingleOrDefault(p => p.id == localizacao.id);

                    aux.pais = localizacao.pais;
                    aux.regiao = localizacao.regiao;
                    aux.distrito = localizacao.distrito;
                    aux.concelho = localizacao.concelho;
                    aux.eliminado = localizacao.eliminado;

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region Prestador

        public List<Prestador> GetAllPrestador()
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {
                    return db.Prestador.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Prestador GetPrestadorById(string id)
        {
            try
            {
                int prestadorId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesPrestador())
                {
                    return db.Prestador.SingleOrDefault(p => p.id == prestadorId);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool UpdatePrestador(Prestador prestador)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {

                    Prestador aux = db.Prestador.SingleOrDefault(p => p.id == prestador.id);

                    aux.eliminado = prestador.eliminado;
                    aux.email = prestador.email;
                    aux.idLocalizacao = prestador.idLocalizacao;
                    aux.morada = prestador.morada;
                    aux.nome = prestador.nome;
                    aux.telefone = prestador.telefone;
                   
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region Servico

        public List<Servico> GetAllServico()
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {
                    return db.Servico.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Servico GetServicoById(string id)
        {
            try
            {
                int servicoId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesServico())
                {
                    return db.Servico.SingleOrDefault(p => p.id == servicoId);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public bool updateServico (Servico servico)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {

                    Servico aux = db.Servico.SingleOrDefault(p => p.id == servico.id);

                    aux.codigo = servico.codigo;
                    aux.descricao = servico.descricao;
                    aux.eliminado = servico.eliminado;
                 
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion
    }
}
