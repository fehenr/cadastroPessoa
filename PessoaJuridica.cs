using System.Collections.Generic;
using System.IO;

namespace CadastroPessoa
{
    public class PessoaJuridica : Pessoa
    {
        public string cnpj { get; set; }

        public string RazaoSocial { get; set; }

        public string Caminho { get; private set; } = "Database/PessoaJuridica.csv";
        
        public override double PagarImposto(float rendimento)
        {
            if (rendimento <= 5000)
            {
                return rendimento * .06;

            }
            else if (rendimento > 5000 && rendimento <= 10000)
            {
                return rendimento * .08;

            }
            else
            {
                return (rendimento / 100) * 10;
            }


        }
        public bool ValidarCNPJ(string cnpj)
        {

            if (cnpj.Length == 14 && cnpj.Substring(cnpj.Length - 6, 4) == "0001")
            {
                return true;
            }
            return false;

        }
        public string PrepararLinhasCsv(PessoaJuridica pj)
        {
            return $"{pj.cnpj};{pj.nome};{pj.RazaoSocial}";
        }

        public void Inserir(PessoaJuridica pj)
        {

            string[] linhas = {PrepararLinhasCsv(pj)};

            File.AppendAllLines(Caminho, linhas);
        }

        public List<PessoaJuridica> Ler()
        {
            List<PessoaJuridica> listaPj = new List<PessoaJuridica>();

            string[] linhas = File.ReadAllLines(Caminho);
            foreach (var cadaLinha in linhas)
            {
                string[] atributos = cadaLinha.Split(";");

                PessoaJuridica cadaPj = new PessoaJuridica();

                cadaPj.cnpj = atributos[0];
                cadaPj.nome = atributos[1];
                cadaPj.RazaoSocial = atributos[2];

                listaPj.Add(cadaPj);
            }

            return listaPj;

        }
    }
}