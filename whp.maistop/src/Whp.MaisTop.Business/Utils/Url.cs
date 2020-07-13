using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Business.Utils
{
    public class Url
    {
        public int Rede { get; set; }
        public string ProjecId { get; set; }
        public string UrlRedirecionamento { get; set; }

        public Url()
        {
            Rede = 0;
            ProjecId = String.Empty;
            UrlRedirecionamento = String.Empty;
        }


        public List<Url> RetornaDadosAcesso()
        {
            var projectIdMaisTop1 = "cc2339d0-a99d-4005-aacb-92ad8457aef3";
            var projectIdMaisTop2 = "a5420eaf-d064-47ed-983d-cb247e58351f";
            var projectIdMaisTop3 = "c42e688d-c4cb-4035-b711-a3d47f74ae15";

            var urlBrasilCtMaisTop1 = "https://maistop1.brasilincentivos.com.br/login?idSession="; //"https://hml.incentivos.brasilct.net/login?idSession=";//
            var urlBrasilCtMaisTop2 = "https://maistop2.brasilincentivos.com.br/login?idSession=";//"https://hml.incentivos.brasilct.net/login?idSession=";//
            var urlBrasilCtMaisTop3 = "https://maistop3.brasilincentivos.com.br/login?idSession=";//"https://hml.incentivos.brasilct.net/login?idSession=";//

            return new List<Url>
           {
                new Url {Rede = 2, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 8 , ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 7, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 10, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 14, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 12, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 19, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 6, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 1, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 9, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 3, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 11, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 18, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 5, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 4, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 15, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 16, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 13, ProjecId = projectIdMaisTop2, UrlRedirecionamento = urlBrasilCtMaisTop2}
               ,new Url {Rede = 17, ProjecId = projectIdMaisTop3, UrlRedirecionamento = urlBrasilCtMaisTop3}
               ,new Url {Rede = 20, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 21, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 22, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}
               ,new Url {Rede = 23, ProjecId = projectIdMaisTop1, UrlRedirecionamento = urlBrasilCtMaisTop1}

           };

        }

    }
}
