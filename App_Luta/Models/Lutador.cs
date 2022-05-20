
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App_Luta.Models
{
    public class Lutador
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; }
        [JsonPropertyName("idade")]
        public int Idade { get; set; }
        [JsonPropertyName("artesMarciais")]
        public string[] ArtesMarciais { get; set; }
        [JsonPropertyName("lutas")]
        public int Lutas { get; set; }
        [JsonPropertyName("derrotas")]
        public int Derrotas { get; set; }
        [JsonPropertyName("vitorias")]
        public int Vitorias { get; set; }
        public bool IsPerdedor { get; protected set; }
        private int PercentualDeVitorias()
        {
            return Vitorias * Lutas / 100;
        }
        private void IncrementarVitoria()
        {
            this.Vitorias += 1;
        }
        private void IncrementarLutas()
        {
            this.Lutas += 1;
        }
        public void IniciarLuta(Lutador desafiante, List<Lutador> lutadores)
        {
            Lutador vencedor = Lutar(desafiante);
            if (vencedor.Id == desafiante.Id)
            {
                vencedor.IncrementarVitoria();
                vencedor.IncrementarLutas();
                this.IsPerdedor = true;
                lutadores.Remove(this);
                this.IncrementarLutas();
            }
            else
            {
                desafiante.IsPerdedor = true;
                desafiante.IncrementarLutas();
                lutadores.Remove(desafiante);
                this.IncrementarLutas();
                this.IncrementarVitoria();
            }

        }
        private Lutador Lutar(Lutador desafiante)
        {
            if (this.PercentualDeVitorias() == desafiante.PercentualDeVitorias())
            {
                return this.ArtesMarciais.Length > desafiante.ArtesMarciais.Length ? this : desafiante;
            }
            return this.PercentualDeVitorias() > desafiante.PercentualDeVitorias() ? this : desafiante;
        }
    }

}
