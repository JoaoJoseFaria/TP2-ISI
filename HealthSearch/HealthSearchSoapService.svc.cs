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

        public bool UpdateLocalizacao(Localizacao localizacao)
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

        public bool DeleteLocalizacao(string id)
        {
            try
            {
                using (var dbLocalizacao = new HealthSearchEntitiesLocalizacao())
                {
                    int localizacaoId = Convert.ToInt32(id);
                    Localizacao aux = dbLocalizacao.Localizacao.SingleOrDefault(p => p.id == localizacaoId);

                    aux.eliminado = DateTime.Now;

                    dbLocalizacao.SaveChanges();

                    //Desactivar os prestadores com a localização desactivada
                    using (var dbPrestadores = new HealthSearchEntitiesPrestador())
                    {
                        var prestList = from prestador in dbPrestadores.Prestador.ToList()
                                        where prestador.idLocalizacao == localizacaoId
                                        select prestador;

                        foreach (var prestador in prestList)
                        {
                            try
                            {
                                DeletePrestador(prestador.id.ToString());

                                //Desactivar os servicos dos prestadores desactivados
                                using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                                {
                                    var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                                        where prestadorServico.idPrestador == prestador.id
                                                        select prestadorServico;

                                    foreach (var prestServ in prestServList)
                                    {
                                        try
                                        {
                                            DeletePrestadorServico(prestServ.idPrestador.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            throw new FaultException(ex.Message);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new FaultException(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public bool HardDeleteLocalizacao(string id)
        {
            try
            {
                using (var dbLocalizacao = new HealthSearchEntitiesLocalizacao())
                {
                    int localizacaoId = Convert.ToInt32(id);
                    Localizacao aux = dbLocalizacao.Localizacao.SingleOrDefault(p => p.id == localizacaoId);

                    dbLocalizacao.Localizacao.Remove(aux);

                    dbLocalizacao.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public int AddLocalizacao(Localizacao localizacao)
        {
            try
            {
                using (var db = new HealthSearchEntitiesLocalizacao())
                {

                    db.Localizacao.Add(localizacao);
                    db.SaveChanges();

                    return localizacao.id;
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

        public bool DeletePrestador (string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {
                    int prestadorId = Convert.ToInt32(id);
                    Prestador aux = db.Prestador.SingleOrDefault(p => p.id == prestadorId);

                    aux.eliminado = DateTime.Now;

                    db.SaveChanges();

                    //Desactivar os servicos dos prestadores desactivados
                    using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                    {
                        var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                            where prestadorServico.idPrestador == prestadorId
                                            select prestadorServico;

                        foreach (var prestServ in prestServList)
                        {
                            try
                            {
                                DeletePrestadorServico(prestServ.idPrestador.ToString());
                            }
                            catch (Exception ex)
                            {
                                throw new FaultException(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public bool HardDeletePrestador(string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {
                    int prestadorId = Convert.ToInt32(id);
                    Prestador aux = db.Prestador.SingleOrDefault(p => p.id == prestadorId);

                    db.Prestador.Remove(aux);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public int AddPrestador(Prestador prestador)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {

                    db.Prestador.Add(prestador);
                    db.SaveChanges();

                    return prestador.id;
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

        public bool UpdateServico (Servico servico)
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

        public bool DeleteServico(string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {
                    int servicoId = Convert.ToInt32(id);
                    Servico aux = db.Servico.SingleOrDefault(p => p.id == servicoId);

                    aux.eliminado = DateTime.Now;

                    db.SaveChanges();

                    //Desactivar os servicos por prestador desactivados
                    using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                    {
                        var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                            where prestadorServico.idServico == servicoId
                                            select prestadorServico;

                        foreach (var prestServ in prestServList)
                        {
                            try
                            {
                                DeleteServicoPrestador(prestServ.idServico.ToString());
                            }
                            catch (Exception ex)
                            {
                                throw new FaultException(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public bool HardDeleteServico(string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {
                    int servicoId = Convert.ToInt32(id);
                    Servico aux = db.Servico.SingleOrDefault(p => p.id == servicoId);

                    db.Servico.Remove(aux);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public int AddServico(Servico servico)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {

                    db.Servico.Add(servico);
                    db.SaveChanges();

                    return servico.id;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #region PrestadorServico

        private bool DeletePrestadorServico (string id)
        {
            try
            {
                //Desactivar os servicos por prestador desactivados
                using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                {
                    int prestadorServicoId = Convert.ToInt32(id);
                    var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                        where prestadorServico.idPrestador == prestadorServicoId
                                        select prestadorServico;

                    foreach (var prestServ in prestServList)
                    {
                        prestServ.eliminado = DateTime.Now;

                        dbPrestadoresServicos.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        private bool DeleteServicoPrestador(string id)
        {
            try
            {
                //Desactivar os servicos por prestador desactivados
                using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                {
                    int prestadorServicoId = Convert.ToInt32(id);
                    var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                        where prestadorServico.idServico == prestadorServicoId
                                        select prestadorServico;

                    foreach (var prestServ in prestServList)
                    {
                        prestServ.eliminado = DateTime.Now;

                        dbPrestadoresServicos.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return true;
        }

        public bool AddPrestadorServico(PrestadorServico prestadorServico)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestadorServico())
                {

                    db.PrestadorServico.Add(prestadorServico);
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
