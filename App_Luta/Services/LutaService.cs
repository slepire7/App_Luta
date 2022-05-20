using App_Luta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_Luta.Services
{
    public class LutaService
    {
        private readonly WebApiService _webApiService;
        private static IEnumerable<Lutador> s_lutadores;
        public LutaService(WebApiService webApiService)
        {
            _webApiService = webApiService;
        }
        public async Task<IEnumerable<Lutador>> GetAll()
        {
            _webApiService.SetUrlBase("https://apidev-mbb.t-systems.com.br:8443/edgemicro_tsdev/torneioluta/api/");
            s_lutadores = await _webApiService.Request<IEnumerable<Lutador>>(null, "competidores", RestSharp.Method.Get);
            return s_lutadores;
        }
        public Lutador ObterCampea(int[] IdLutadores)
        {
            List<Lutador> integrantes_torneio = s_lutadores.Where(o => IdLutadores.Contains(o.Id)).OrderBy(o => o.Idade).ToList();
            while (integrantes_torneio.Count > 1)
            {
                for (int i = 0; i < integrantes_torneio.Count; i++)
                {
                    int IdxProximoLutador = i + 1;
                    if (IdxProximoLutador < integrantes_torneio.Count)
                    {
                        integrantes_torneio[i].IniciarLuta(integrantes_torneio[IdxProximoLutador], integrantes_torneio);
                    }
                }
            }
            return integrantes_torneio.FirstOrDefault(o => o.IsPerdedor == false);
        }
    }
}
