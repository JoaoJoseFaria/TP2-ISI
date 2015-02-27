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
    /// Classe que implementa o serviço SOAP
    /// </summary>
    public class HealthSearchSoapService : IHealthSearchSoapService
    {

        #region Localizacao

        /// <summary>
        /// Devolve todas as localizações
        /// </summary>
        /// <returns>Lista de localizações</returns>
        public List<Localizacao> GetAllLocalizacao()
        {
            try
            {
                using (var db = new HealthSearchEntitiesLocalizacao())
                {
                    return db.Localizacao.Where(p => p.eliminado == null).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Devolve localização pelo id
        /// </summary>
        /// <param name="id">Id da localização</param>
        /// <returns>Localização</returns>
        public Localizacao GetLocalizacaoById(string id)
        {
            try
            {
                int localizacaoId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesLocalizacao())
                {
                    return db.Localizacao.SingleOrDefault(p => p.id == localizacaoId & p.eliminado == null);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza uma localização
        /// </summary>
        /// <param name="localizacao">Localização actualizada</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        public bool UpdateLocalizacao(Localizacao localizacao)
        {
            try
            {
                using (var db = new HealthSearchEntitiesLocalizacao())
                {

                    Localizacao aux = db.Localizacao.SingleOrDefault(p => p.id == localizacao.id & p.eliminado == null);

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

        /// <summary>
        /// Marca localização como eliminada
        /// </summary>
        /// <param name="id">Id da localização a marcar</param>
        /// <returns>True caso marcada, false caso contrário</returns>
        public bool DeleteLocalizacao(string id)
        {
            try
            {
                using (var dbLocalizacao = new HealthSearchEntitiesLocalizacao())
                {
                    int localizacaoId = Convert.ToInt32(id);
                    Localizacao aux = dbLocalizacao.Localizacao.SingleOrDefault(p => p.id == localizacaoId & p.eliminado == null);

                    aux.eliminado = DateTime.Now;

                    dbLocalizacao.SaveChanges();

                    //Desactivar os prestadores com a localização desactivada
                    using (var dbPrestadores = new HealthSearchEntitiesPrestador())
                    {
                        var prestList = from prestador in dbPrestadores.Prestador.ToList()
                                        where prestador.idLocalizacao == localizacaoId & prestador.eliminado == null
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
                                                        where prestadorServico.idPrestador == prestador.id & prestadorServico.eliminado == null
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

        /// <summary>
        /// Elimina localização da base de dados
        /// </summary>
        /// <param name="id">Id da localização a eliminar</param>
        /// <returns>True caso eliminada, false caso contrário</returns>
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

        /// <summary>
        /// Adiciona uma localização
        /// </summary>
        /// <param name="localizacao">Localização a adicionar</param>
        /// <returns>Id nova localização</returns>
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

        /// <summary>
        /// Devolve todos os prestadores
        /// </summary>
        /// <returns>Lista de prestadores</returns>
        public List<Prestador> GetAllPrestador()
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {
                    return db.Prestador.Where(p => p.eliminado == null).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Devolve os prestadores por id
        /// </summary>
        /// <param name="id">Id do prestador</param>
        /// <returns>Prestador</returns>
        public Prestador GetPrestadorById(string id)
        {
            try
            {
                int prestadorId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesPrestador())
                {
                    return db.Prestador.SingleOrDefault(p => p.id == prestadorId & p.eliminado == null);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Devolve os prestadores por serviço
        /// </summary>
        /// <param name="id">Id do serviço</param>
        /// <returns>Lista de prestadores</returns>
        public List<Prestador> GetPestadorByServico(string id)
        {
            List<Prestador> aux = new List<Prestador>();
            int servicoId = Convert.ToInt32(id);
            try
            {
                using (var db = new HealthSearchEntitiesPrestadorServico())
                {
                    var prestList = from prestServ in db.PrestadorServico
                                    where prestServ.idServico == servicoId & prestServ.eliminado == null
                                    select prestServ.Prestador;

                    foreach (Prestador prest in prestList)
                    {
                        aux.Add(new Prestador
                        {
                            id=prest.id,
                            idLocalizacao=prest.idLocalizacao,
                            nome=prest.nome,
                            morada=prest.morada,
                            telefone=prest.telefone,
                            email=prest.email,
                            eliminado=prest.eliminado
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return aux;
        }

        /// <summary>
        /// Devolve os prestadores pela localização
        /// </summary>
        /// <param name="pais">Pais a procurar</param>
        /// <param name="regiao">Região a procurar</param>
        /// <param name="distrito">Distrito a procurar</param>
        /// <param name="concelho">Concelho a procurar</param>
        /// <returns>Lista de prestadores</returns>
        public List<Prestador> GetPestadorByLocalizacaoo(string pais = null, string regiao = null,
                            string distrito = null, string concelho = null)
        {
            List<Prestador> aux = new List<Prestador>();
            try
            {
                using (var db = new HealthSearchEntitiesVwPrestadorLocalizacao())
                {
                    var prestList = from prest in db.VwPrestadorLocalizacao
                                    where prest.eliminado == null
                                    select prest;

                    if (!string.IsNullOrWhiteSpace(pais))
                        prestList = prestList.Where(p => p.pais == pais);
                    if (!string.IsNullOrWhiteSpace(regiao))
                        prestList = prestList.Where(p => p.regiao == regiao);
                    if (!string.IsNullOrWhiteSpace(distrito))
                        prestList = prestList.Where(p => p.distrito == distrito);
                    if (!string.IsNullOrWhiteSpace(concelho))
                        prestList = prestList.Where(p => p.concelho == concelho);

                    foreach (VwPrestadorLocalizacao prest in prestList)
                    {
                        aux.Add(new Prestador
                        {
                            id = prest.id,
                            idLocalizacao = prest.idLocalizacao,
                            nome = prest.nome,
                            morada = prest.morada,
                            telefone = prest.telefone,
                            email = prest.email,
                            eliminado = prest.eliminado
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
            return aux;
        }

        /// <summary>
        /// Actualiza dados do prestador
        /// </summary>
        /// <param name="prestador">Prestador actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        public bool UpdatePrestador(Prestador prestador)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {

                    Prestador aux = db.Prestador.SingleOrDefault(p => p.id == prestador.id & p.eliminado == null);

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

        /// <summary>
        /// Marca prestador como eliminado
        /// </summary>
        /// <param name="id">Id do restador a marcar</param>
        /// <returns>True caso actualiza, false caso contrário<</returns>
        public bool DeletePrestador (string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesPrestador())
                {
                    int prestadorId = Convert.ToInt32(id);
                    Prestador aux = db.Prestador.SingleOrDefault(p => p.id == prestadorId & p.eliminado == null);

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

        /// <summary>
        /// Elimina registo do prestador da base de dados
        /// </summary>
        /// <param name="id">Id do prestador a eliminar</param>
        /// <returns>True caso elimina, false caso contrário<</returns>
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

        /// <summary>
        /// Adiciona novo prestador
        /// </summary>
        /// <param name="prestador">Prestador a adicionar</param>
        /// <returns>Id novo prestador</returns>
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

        /// <summary>
        /// Devolve todos os serviços
        /// </summary>
        /// <returns>Lista de serviços</returns>
        public List<Servico> GetAllServico()
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {
                    return db.Servico.Where(p => p.eliminado == null).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Devolve serviço por id
        /// </summary>
        /// <param name="id">Id do serviço a pesquisar</param>
        /// <returns>Serviço devolvido</returns>
        public Servico GetServicoById(string id)
        {
            try
            {
                int servicoId = Convert.ToInt32(id);

                using (var db = new HealthSearchEntitiesServico())
                {
                    return db.Servico.SingleOrDefault(p => p.id == servicoId & p.eliminado == null);
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza um serviço
        /// </summary>
        /// <param name="servico">Serviço actualizado</param>
        /// <returns>True caso actualiza, false caso contrário</returns>
        public bool UpdateServico (Servico servico)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {

                    Servico aux = db.Servico.SingleOrDefault(p => p.id == servico.id & p.eliminado == null);

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

        /// <summary>
        /// Marca serviço como eliminado
        /// </summary>
        /// <param name="id">Id do serviço a marcar</param>
        /// <returns>True caso marque, false caso contrário</returns>
        public bool DeleteServico(string id)
        {
            try
            {
                using (var db = new HealthSearchEntitiesServico())
                {
                    int servicoId = Convert.ToInt32(id);
                    Servico aux = db.Servico.SingleOrDefault(p => p.id == servicoId & p.eliminado == null);

                    aux.eliminado = DateTime.Now;

                    db.SaveChanges();

                    //Desactivar os servicos por prestador desactivados
                    using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                    {
                        var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                            where prestadorServico.idServico == servicoId & prestadorServico.eliminado == null
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

        /// <summary>
        /// Remove o serviço da base de dados
        /// </summary>
        /// <param name="id">Id do serviço a remover</param>
        /// <returns>True caso remova, false caso contrário</returns>
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

        /// <summary>
        /// Adiciona novo serviço na base de dados
        /// </summary>
        /// <param name="servico">Serviço a adicionar</param>
        /// <returns>Id do novo serviço</returns>
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

        /// <summary>
        /// Marca um registo como eliminado
        /// </summary>
        /// <param name="prestadorServico">serviço a marcar</param>
        /// <returns>True caso marque, false caso contrário</returns>
        private bool DeletePrestadorServico (string id)
        {
            try
            {
                //Desactivar os servicos por prestador desactivados
                using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                {
                    int prestadorServicoId = Convert.ToInt32(id);
                    var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                        where prestadorServico.idPrestador == prestadorServicoId & prestadorServico.eliminado == null
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

        /// <summary>
        /// Marca um registo como eliminado
        /// </summary>
        /// <param name="prestadorServico">serviço a marcar</param>
        /// <returns>True caso marque, false caso contrário</returns>
        private bool DeleteServicoPrestador(string id)
        {
            try
            {
                //Desactivar os servicos por prestador desactivados
                using (var dbPrestadoresServicos = new HealthSearchEntitiesPrestadorServico())
                {
                    int prestadorServicoId = Convert.ToInt32(id);
                    var prestServList = from prestadorServico in dbPrestadoresServicos.PrestadorServico.ToList()
                                        where prestadorServico.idServico == prestadorServicoId & prestadorServico.eliminado == null
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

        /// <summary>
        /// Adiciona prestador Serviço
        /// </summary>
        /// <param name="prestadorServico">Prestador serviço</param>
        /// <returns>True caso adicione</returns>
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

        #region Manutenção

        /// <summary>
        /// Limpa todos os registos da base de dados que estão marcados como eliminados
        /// </summary>
        /// <param name="data">Data a partir da qual está marcada como eliminada</param>
        /// <returns>-1 em caso de sucesso</returns>
        public int LimparTabelas(string data)
        {
            try
            {
                using (var db = new HealthSearchEntitiesSpLimparTabelas())
                {
                    return db.SpLimparTabelas(data);
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
