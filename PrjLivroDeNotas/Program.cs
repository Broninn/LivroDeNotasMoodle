using PrjLivroDeNotas.Metodos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PrjLivroDeNotas;
using static PrjLivroDeNotas.Metodos.core_grades_create_gradecategory;

namespace PrjLivroDeNotas
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> ListaSala = LerArquivoSalas();
            DateTime ini, fim, TotalIni, totalfim;
            TotalIni = DateTime.Now;
            TimeSpan diff;
            int qntLinhas = ListaSala.Count;
            Console.WriteLine($"Processamento iniciado em: {TotalIni} - salas a processar: {qntLinhas}");

            foreach (var item in ListaSala)
            {
                ini = DateTime.Now;
                Console.Write($"ID Sala: {item} ");
                SESINEMApenas2Trimestres(Convert.ToInt32(item));
                fim = DateTime.Now;
                diff = fim - ini;
                qntLinhas--;
                Console.WriteLine($"Tempo de Processamento: {ToReadableString(diff)} Restam: {qntLinhas} salas");
                GravaLog(item);
                GravaLogEstat($"{item} - Tempo de Processamento: {ToReadableString(diff)}");

            }
            totalfim = DateTime.Now;
            Console.WriteLine($"Processo concluído {totalfim} duração: {ToReadableString(totalfim - TotalIni)} ");
        }

        private static List<string> LerArquivoSalas()
        {
            string arquivo = Directory.GetCurrentDirectory() + @"\ListaSalas.txt";
            List<string> ret = new List<string>();
            if (File.Exists(arquivo))
            {
                using (StreamReader sr = new StreamReader(arquivo))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        ret.Add(linha);
                    }
                }
            }
            return ret;
        }

        public static void GravaLog(string pLog)
        {
            string arquivo = Directory.GetCurrentDirectory() + @"\Log.txt";

            if (!File.Exists(arquivo))
            {
                File.Create(arquivo);

            }
            using (StreamWriter sw = File.AppendText(arquivo))
            {
                sw.WriteLine($"Configuração completa da Sala:{pLog} ");
            }

        }

        public static void GravaLogEstat(string pLog)
        {
            string arquivo = Directory.GetCurrentDirectory() + @"\Logestat.txt";

            if (!File.Exists(arquivo))
            {
                using (File.Create(arquivo)) { }

            }
            using (StreamWriter sw = File.AppendText(arquivo))
            {
                sw.WriteLine($"Configuração completa da Sala:{pLog}");
            }

        }

        public static string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }



        //##########    GERAL    ##########

        private static void CriarCategoriaNaoAvaliativa(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa.",
                itemname = "Não Avaliativa.",
                idnumber = "NaoAvaliativa.",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion
        }

        private static void CriarCategoriaUsoInterno(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesUsoInterno = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Uso Interno.",
                itemname = "Uso Interno.",
                idnumber = "UsoInterno.",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
            };
            var etapaUsoInterno = new core_grades_create_gradecategory();
            r = etapaUsoInterno.SetCategorisInfo(pAtividadesUsoInterno).Result;

            Console.Write(".");
            #endregion
        }

        private static void CriarApenasCategoriasBimestres(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            #region Categorias Bimestrais


            var pEtapa1Bimestre = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "1 Bimestre",
                itemname = "Nota 1 Bimestre",
                idnumber = "BIM1",
                aggregation = 10,
                weightoverride = 1,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
                parentcategoryidnumber = "AE1A1"
            };
            var bimestre1 = new core_grades_create_gradecategory();
            r = bimestre1.SetCategorisInfo(pEtapa1Bimestre).Result;
            //Console.WriteLine(regular);

            var pEtapa2Bimestre = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "2 Bimestre",
                itemname = "Nota 2 Bimestre",
                idnumber = "BIM2",
                aggregation = 10,
                weightoverride = 1,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
                parentcategoryidnumber = "AE1A1"
            };
            var bimestre2 = new core_grades_create_gradecategory();
            r = bimestre2.SetCategorisInfo(pEtapa2Bimestre).Result;
            //Console.WriteLine(regular);

            #endregion

        }

        private static void CriarEtapa2(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            #region Etapa2


            var pEtapa1Bimestre = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Atividade Avaliativa 2",
                itemname = "Atividade Avaliativa 2",
                idnumber = "AE2A1",
                aggregation = 6,
                weightoverride = 1,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
                parentcategoryidnumber = "REGULAR"
            };
            var bimestre1 = new core_grades_create_gradecategory();
            r = bimestre1.SetCategorisInfo(pEtapa1Bimestre).Result;
            //Console.WriteLine(regular);
            //=[[AE1A1]]

            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final",
                itemname = "Média Recuperação Final",
                idnumber = "RF3R1",
                gradepass = 5,
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");

            #endregion

        }

        //##########    SENAI    ##########
        private static void SENAIAprendizagemPresencialConectadaSemipresencialSGE(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1, AE2, AE1A, RE1R, AE2A, RE2R;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                gradepass = 6,
                aggregateonlygraded = 0,
                aggregationcoef2 = 1.0M,
                grademax = 10
                //parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 1 (Distância)",
                itemname = "Média Etapa 1 (Distância)",
                idnumber = "AE1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 0.4M,
                aggregateonlygraded = 0,
                parentcategoryid = regular,
                grademax = 10,
                gradepass = 6
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades à Distância
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 (Distância)",
                itemname = "Nota Avaliações Etapa 1 (Distância)",
                idnumber = "AE1A",
                aggregation = 6,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1,
                gradepass = 6,
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            AE1A = r.categoryids.FirstOrDefault();
            Console.Write(".");

            // Etapa 1 - Atividades à Distância
            var pEtapa1AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Distância) - Integração SGE",
                itemname = "Nota Avaliações 1 (Distância) - Integração SGE",
                idnumber = "AE1A1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A,
                gradepass = 6,
            };
            var etapa1AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesItemMetodos.SetCategorisInfo(pEtapa1AtividadesItem).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 1 (Distância) - Integração SGE",
                itemname = "Média Recuperação Etapa 1 (Distância) - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 11,
                grademax = 10,
                gradepass = 6,
                parentcategoryid = AE1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            RE1R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var pEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 2 (Presencial)",
                itemname = "Média Etapa 2 (Presencial)",
                idnumber = "AE2",
                aggregation = 6,
                weightoverride = 1,
                gradepass = 6,
                grademax = 10,
                aggregationcoef2 = 0.6M,
                aggregateonlygraded = 0,
                parentcategoryid = regular
            };
            var etapa2Metodos = new core_grades_create_gradecategory();
            r = etapa2Metodos.SetCategorisInfo(pEtapa2).Result;
            AE2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 2 - Atividades Presenciais
            var pEtapa2Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 2 (Presencial)",
                itemname = "Média Avaliações Etapa 2 (Presencial)",
                idnumber = "AE2A",
                grademax = 10,
                aggregation = 6,
                gradepass = 6,
                aggregateonlygraded = 0,
                parentcategoryid = AE2
            };
            var etapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesMetodos.SetCategorisInfo(pEtapa2Atividades).Result;
            AE2A = r.categoryids.FirstOrDefault();
            Console.Write(".");


            var pEtapa2AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Presencial) - Integração SGE",
                itemname = "Nota Avaliações 1 (Presencial) - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 11,
                grademax = 10,
                gradepass = 6,
                aggregateonlygraded = 0,
                parentcategoryid = AE2A
            };
            var etapa2AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesItemMetodos.SetCategorisInfo(pEtapa2AtividadesItem).Result;
            Console.Write(".");

           //Etapa 1 - Recuperação à Distância
            var pEtapa2Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 2 (Presencial) - Integração SGE",
                itemname = "Média Recuperação Etapa 2 (Presencial) - Integração SGE",
                idnumber = "RE2R1",
                grademax = 10,
                gradepass = 6,
                aggregation = 11,
                parentcategoryid = AE2
            };
            var etapa2RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoMetodos.SetCategorisInfo(pEtapa2Recuperacao).Result;
            RE2R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final - Integração SGE",
                itemname = "Média Recuperação Final - Integração SGE",
                idnumber = "RF3R1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 1,
                gradepass = 5
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");
        }
        private static void SENAIAperfeicoamentoFormacaoCidada(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1, AE2, AE1A, RE1R, AE2A, RE2R;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 10,
                weightoverride = 1,
                aggregateonlygraded = 0,
                aggregationcoef2 = 1.0M,
                gradepass = 7,
                grademax = 10
                //parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write("ER - ");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 1 (Distância)",
                itemname = "Média Etapa 1 (Distância)",
                idnumber = "AE1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 0.4M,
                aggregateonlygraded = 0,
                parentcategoryid = regular,
                grademax = 10,
                gradepass = 7
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1 = r.categoryids.FirstOrDefault();
            Console.Write("E1 - ");

            //Etapa 1 - Atividades à Distância
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 (Distância)",
                itemname = "Nota Avaliações Etapa 1 (Distância)",
                idnumber = "AE1A1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1,
                gradepass = 7,
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            AE1A = r.categoryids.FirstOrDefault();
            Console.Write("AE1 - ");

            // Etapa 1 - trimestre 1
            var pEtapa1AtividadesTrimestre1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 1 Trimestre (Distância)",
                itemname = "Nota Avaliação 1 Trimestre (Distância)",
                idnumber = "E1TRIM1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A,
                gradepass = 7,
            };
            var etapa1AtividadesTrimestre1 = new core_grades_create_gradecategory();
            r = etapa1AtividadesTrimestre1.SetCategorisInfo(pEtapa1AtividadesTrimestre1).Result;
            Console.Write("T1 - ");

            // Etapa 1 - trimestre 2
            var pEtapa1AtividadesTrimestre2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 2 Trimestre (Distância)",
                itemname = "Nota Avaliação 2 Trimestre (Distância)",
                idnumber = "E1TRIM2",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A,
                gradepass = 7,
            };
            var etapa1AtividadesTrimestre2 = new core_grades_create_gradecategory();
            r = etapa1AtividadesTrimestre2.SetCategorisInfo(pEtapa1AtividadesTrimestre2).Result;
            Console.Write("T2 - ");

            // Etapa 1 - trimestre 3
            var pEtapa1AtividadesTrimestre3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 3 Trimestre (Distância)",
                itemname = "Nota Avaliação 3 Trimestre (Distância)",
                idnumber = "E1TRIM3",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A,
                gradepass = 7,
            };
            var etapa1AtividadesTrimestre3 = new core_grades_create_gradecategory();
            r = etapa1AtividadesTrimestre3.SetCategorisInfo(pEtapa1AtividadesTrimestre3).Result;
            Console.Write("T3 - ");

            //Etapa 1 - Recuperação à Distância
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 1 (Distância) ",
                itemname = "Média Recuperação Etapa 1 (Distância) ",
                idnumber = "RE1R1",
                aggregation = 11,
                grademax = 10,
                gradepass = 7,
                parentcategoryid = AE1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            RE1R = r.categoryids.FirstOrDefault();
            Console.Write("RE1 - ");

            #endregion

            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var pEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 2 (Presencial)",
                itemname = "Nota Etapa 2 (Presencial)",
                idnumber = "AE2",
                aggregation = 6,
                weightoverride = 1,
                gradepass = 7,
                grademax = 10,
                aggregationcoef2 = 0.6M,
                aggregateonlygraded = 0,
                parentcategoryid = regular
            };
            var etapa2Metodos = new core_grades_create_gradecategory();
            r = etapa2Metodos.SetCategorisInfo(pEtapa2).Result;
            AE2 = r.categoryids.FirstOrDefault();
            Console.Write("E2 - ");

            //Etapa 2 - Atividades Presenciais
            var pEtapa2Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 2 (Presencial)",
                itemname = "Nota Avaliações Etapa 2 (Presencial)",
                idnumber = "AE2A1",
                grademax = 10,
                aggregation = 11,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE2
            };
            var etapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesMetodos.SetCategorisInfo(pEtapa2Atividades).Result;
            AE2A = r.categoryids.FirstOrDefault();
            Console.Write("AE2 - ");


            var petapa2AtividadesTrimestre1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 1 Trimestre (Presencial)",
                itemname = "Nota Avaliação 1 Trimestre (Presencial)",
                idnumber = "E2TRIM1",
                aggregation = 11,
                grademax = 10,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE2A
            };
            var etapa2AtividadesTrimestre1 = new core_grades_create_gradecategory();
            r = etapa2AtividadesTrimestre1.SetCategorisInfo(petapa2AtividadesTrimestre1).Result;
            Console.Write("T1 - ");


            var petapa2AtividadesTrimestre2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 2 Trimestre (Presencial)",
                itemname = "Nota Avaliação 2 Trimestre (Presencial)",
                idnumber = "E2TRIM2",
                grademax = 10,
                gradepass = 7,
                aggregateonlygraded = 0,
                aggregation = 11,
                parentcategoryid = AE2A
            };

            var etapa2AtividadesTrimestre2 = new core_grades_create_gradecategory();
            r = etapa2AtividadesTrimestre2.SetCategorisInfo(petapa2AtividadesTrimestre2).Result;
            Console.Write("T2 - ");


            var petapa2AtividadesTrimestre3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 3 Trimestre (Presencial)",
                itemname = "Nota Avaliação 3 Trimestre (Presencial)",
                idnumber = "E2TRIM3",
                grademax = 10,
                aggregateonlygraded = 0,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2A
            };

            var etapa2AtividadesTrimestre3 = new core_grades_create_gradecategory();
            r = etapa2AtividadesTrimestre3.SetCategorisInfo(petapa2AtividadesTrimestre3).Result;
            Console.Write("T3 - ");

            //Etapa 1 - Recuperação à Distância
            var pEtapa2Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 2 (Presencial)",
                itemname = "Média Recuperação Etapa 2 (Presencial)",
                idnumber = "RE2R1",
                grademax = 10,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2
            };
            var etapa2RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoMetodos.SetCategorisInfo(pEtapa2Recuperacao).Result;
            RE2R = r.categoryids.FirstOrDefault();
            Console.Write("RE2 - ");

            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final",
                itemname = "Média Recuperação Final)",
                idnumber = "RF3R1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 1,
                gradepass = 5,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write("RF");

            #region Atividades Para Trimestre SESI

            //Atividades Para Trimestre SESI

            var pTrimestre1Sesi = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 1 (SESI)",
                itemname = "Trimestre 1 (SESI)",
                idnumber = "Trimestre 1 (SESI)",
                aggregateonlygraded = 1,
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                gradepass = 7,
                grademax = 10,
                aggregationcoef2 = 0,
                /*calculo para adicionar manual:
                    =([[E1TRIM1]]*0,4)+([[E2TRIM1]]*0,6)
                */
            };
            var etapaTrimestre1Sesi = new core_grades_create_gradecategory();
            r = etapaTrimestre1Sesi.SetCategorisInfo(pTrimestre1Sesi).Result;

            Console.Write(".");

            var pTrimestre2Sesi = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 2 (SESI)",
                itemname = "Trimestre 2 (SESI)",
                idnumber = "Trimestre 2 (SESI)",
                aggregateonlygraded = 1,
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                gradepass = 7,
                grademax = 10,
                aggregationcoef2 = 0,
                /*calculo para adicionar manual:
                    =([[E1TRIM2]]*0,4)+([[E2TRIM2]]*0,6)
                */
            };
            var etapaTrimestre2Sesi = new core_grades_create_gradecategory();
            r = etapaTrimestre2Sesi.SetCategorisInfo(pTrimestre2Sesi).Result;

            Console.Write(".");

            var pTrimestre3Sesi = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 3 (SESI)",
                itemname = "Trimestre 3 (SESI)",
                idnumber = "Trimestre 3 (SESI)",
                aggregateonlygraded = 1,
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                gradepass = 7,
                grademax = 10,
                aggregationcoef2 = 0,
                /*calculo para adicionar manual:
                    =([[E1TRIM3]]*0,4)+([[E2TRIM3]]*0,6)
                */
            };
            var etapaTrimestre3Sesi = new core_grades_create_gradecategory();
            r = etapaTrimestre3Sesi.SetCategorisInfo(pTrimestre3Sesi).Result;

            Console.Write(".");



            #endregion


        }
        private static void SENAIAperfeicoamentoEAD(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            //Console.WriteLine(r.categoryid);
            #endregion

            #region Etapa Regular
            //Atividades Avaliativas
            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Atividades Avaliativas",
                itemname = "Atividades Avaliativas",
                idnumber = "AE1A1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                gradepass = 6,
                decimals = 1,
                grademax = 10,
            };
            var etapaRegular = new core_grades_create_gradecategory();
            r = etapaRegular.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            //Console.WriteLine(regular);

            #endregion

        }
        private static void SENAIQualificaçãoSemipresencial(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1, AE2;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 10,
                weightoverride = 1,
                gradepass = 6,
                grademax = 10,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 1 (Distância)",
                itemname = "Média Etapa 1 (Distância)",
                idnumber = "AE1",
                gradepass = 6,
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 10,
                weightoverride = 1,
                aggregationcoef2 = 0.4M,
                parentcategoryid = regular
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades à Distância
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 (Distância) - Integração SGE",
                itemname = "Nota Avaliações Etapa 1 (Distância) - Integração SGE",
                idnumber = "AE1A1",
                gradepass = 6,
                aggregateonlygraded = 0,
                aggregation = 11,
                grademax = 10,
                parentcategoryid = AE1
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 1 (Distância) - Integração SGE",
                itemname = "Média Recuperação Etapa 1 (Distância) - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 11,
                gradepass = 6,
                grademax = 10,
                parentcategoryid = AE1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            Console.Write(".");
            #endregion

            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var pEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 2 (Presencial)",
                itemname = "Nota Etapa 2 (Presencial)",
                idnumber = "AE2",
                gradepass = 6,
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 0.6M,
                aggregateonlygraded = 0,
                grademax = 10,
                parentcategoryid = regular
            };
            var etapa2Metodos = new core_grades_create_gradecategory();
            r = etapa2Metodos.SetCategorisInfo(pEtapa2).Result;
            AE2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 2 - Atividades Presenciais
            var pEtapa2Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 2 (Presencial) - Integração SGE",
                itemname = "Média Avaliações Etapa 2 (Presencial) - Integração SGE",
                idnumber = "AE2A1",
                gradepass = 6,
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 10,
                parentcategoryid = AE2
            };
            var etapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesMetodos.SetCategorisInfo(pEtapa2Atividades).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa2Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 2 (Presencial) - Integração SGE",
                itemname = "Média Recuperação Etapa 2 (Presencial) - Integração SGE",
                idnumber = "RE2R1",
                gradepass = 6,
                grademax = 10,
                aggregation = 11,
                parentcategoryid = AE2
            };
            var etapa2RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoMetodos.SetCategorisInfo(pEtapa2Recuperacao).Result;
            Console.Write(".");
            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final - Integração SGE",
                itemname = "Média Recuperação Final - Integração SGE",
                idnumber = "RF3R1",
                gradepass = 5,
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");
        }
        private static void SENAIQualificação100EAD(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1A1;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 6,
                weightoverride = 1,
                grademax = 10,
                gradepass = 6,
                aggregationcoef2 = 1.0M,
                decimals = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1",
                itemname = "Nota Avaliações Etapa 1",
                idnumber = "AE1A1",
                aggregation = 6,
                grademax = 10,
                gradepass = 6,
                decimals = 1,
                weightoverride = 1,
                aggregateonlygraded = 0,
                parentcategoryid = regular
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1A1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Atividade 0,4
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 1",
                itemname = "Nota Avaliação 1",
                idnumber = "AE1A",
                aggregation = 11,
                gradepass = 6,
                grademax = 10,
                decimals = 1,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A1
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            Console.Write(".");

            //Atividade 0,6
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final",
                itemname = "Nota Recuperação Final",
                idnumber = "RF3R1",
                aggregation = 6,
                gradepass = 5,
                grademax = 10,
                decimals = 1,
                aggregateonlygraded = 1,
                parentcategoryid = AE1A1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            Console.Write(".");
            #endregion
        }
        private static void SENAITecnicoSemipresencialSGE(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1, AE2, AE1A, RE1R, AE2A, RE2R;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 10,
                weightoverride = 1,
                aggregateonlygraded = 0,
                aggregationcoef2 = 1.0M,
                gradepass = 7,
                grademax = 10
                //parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 1 (Distância)",
                itemname = "Média Etapa 1 (Distância)",
                idnumber = "AE1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 0.4M,
                aggregateonlygraded = 0,
                parentcategoryid = regular,
                grademax = 10,
                gradepass = 7
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades à Distância
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 (Distância)",
                itemname = "Nota Avaliações Etapa 1 (Distância)",
                idnumber = "AE1A",
                aggregation = 6,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1,
                gradepass = 7,
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            AE1A = r.categoryids.FirstOrDefault();
            Console.Write(".");

            // Etapa 1 - Atividades à Distância
            var pEtapa1AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Distância) - Integração SGE",
                itemname = "Nota Avaliações 1 (Distância) - Integração SGE",
                idnumber = "AE1A1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A,
                gradepass = 7,
            };
            var etapa1AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesItemMetodos.SetCategorisInfo(pEtapa1AtividadesItem).Result;
            Console.Write(".");

            var pEtapa1ParalelasItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Paralela 1 (Distância) - Integração SGE",
                itemname = "Nota Recuperação Paralela 1 (Distância) - Integração SGE",
                idnumber = "AE1R1",
                aggregation = 11,
                gradepass = 7,
                grademax = 10,
                parentcategoryid = AE1A
            };

            var pEtapa1ParalelasMetodos = new core_grades_create_gradecategory();
            r = pEtapa1ParalelasMetodos.SetCategorisInfo(pEtapa1ParalelasItem).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 1 (Distância) - Integração SGE",
                itemname = "Média Recuperação Etapa 1 (Distância) - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 11,
                grademax = 10,
                gradepass = 7,
                parentcategoryid = AE1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            RE1R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var pEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 2 (Presencial)",
                itemname = "Média Etapa 2 (Presencial)",
                idnumber = "AE2",
                aggregation = 6,
                weightoverride = 1,
                gradepass = 7,
                grademax = 10,
                aggregationcoef2 = 0.6M,
                aggregateonlygraded = 0,
                parentcategoryid = regular
            };
            var etapa2Metodos = new core_grades_create_gradecategory();
            r = etapa2Metodos.SetCategorisInfo(pEtapa2).Result;
            AE2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 2 - Atividades Presenciais
            var pEtapa2Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 2 (Presencial)",
                itemname = "Média Avaliações Etapa 2 (Presencial)",
                idnumber = "AE2A",
                grademax = 10,
                aggregation = 6,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE2
            };
            var etapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesMetodos.SetCategorisInfo(pEtapa2Atividades).Result;
            AE2A = r.categoryids.FirstOrDefault();
            Console.Write(".");


            var pEtapa2AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Presencial) - Integração SGE",
                itemname = "Nota Avaliações 1 (Presencial) - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 11,
                grademax = 10,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE2A
            };
            var etapa2AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesItemMetodos.SetCategorisInfo(pEtapa2AtividadesItem).Result;
            Console.Write(".");


            var pEtapa2ParalelasItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Paralela 1 (Presencial) - Integração SGE",
                itemname = "Nota Recuperação Paralela 1 (Presencial) - Integração SGE",
                idnumber = "AE2R1",
                grademax = 10,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2A
            };

            var pEtapa2ParalelasMetodos = new core_grades_create_gradecategory();
            r = pEtapa2ParalelasMetodos.SetCategorisInfo(pEtapa2ParalelasItem).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa2Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 2 (Presencial) - Integração SGE",
                itemname = "Média Recuperação Etapa 2 (Presencial) - Integração SGE",
                idnumber = "RE2R1",
                grademax = 10,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2
            };
            var etapa2RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoMetodos.SetCategorisInfo(pEtapa2Recuperacao).Result;
            RE2R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final - Integração SGE",
                itemname = "Média Recuperação Final - Integração SGE",
                idnumber = "RF3R1",
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 1,
                gradepass = 5,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");
        }
        private static void SENAITecnicoPresencial(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1, AE2, AE1A, RE1R, AE2A, RE2R;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 11,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                gradepass = 7,
                aggregateonlygraded = 0,
                aggregationcoef2 = 1.0M,
                grademax = 10
                //parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 1 (Distância)",
                itemname = "Média Etapa 1 (Distância)",
                idnumber = "AE1",
                aggregation = 6,
                weightoverride = 1,
                aggregateonlygraded = 0,
                gradepass = 7,
                parentcategoryid = regular,
                grademax = 10
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades à Distância
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 (Distância)",
                itemname = "Nota Avaliações Etapa 1 (Distância)",
                idnumber = "AE1A",
                aggregation = 6,
                grademax = 10,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE1
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            AE1A = r.categoryids.FirstOrDefault();
            Console.Write(".");

            // Etapa 1 - Atividades à Distância
            var pEtapa1AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Distância)",
                itemname = "Nota Avaliações 1 (Distância)",
                idnumber = "AE1A1",
                gradepass = 7,
                aggregation = 11,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A
            };
            var etapa1AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesItemMetodos.SetCategorisInfo(pEtapa1AtividadesItem).Result;
            Console.Write(".");

            var pEtapa1ParalelasItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Paralela 1 (Distância)",
                itemname = "Nota Recuperação Paralela 1 (Distância)",
                idnumber = "AE1R1",
                aggregation = 11,
                gradepass = 7,
                grademax = 10,
                parentcategoryid = AE1A
            };

            var pEtapa1ParalelasMetodos = new core_grades_create_gradecategory();
            r = pEtapa1ParalelasMetodos.SetCategorisInfo(pEtapa1ParalelasItem).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 1 (Distância) ",
                itemname = "Média Recuperação Etapa 1 (Distância) ",
                idnumber = "RE1R1",
                aggregation = 11,
                grademax = 10,
                gradepass = 7,
                parentcategoryid = AE1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            RE1R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var pEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa 2 (Presencial)",
                itemname = "Nota Etapa 2 (Presencial)",
                idnumber = "AE2",
                aggregation = 6,
                gradepass = 7,
                weightoverride = 1,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = regular
            };
            var etapa2Metodos = new core_grades_create_gradecategory();
            r = etapa2Metodos.SetCategorisInfo(pEtapa2).Result;
            AE2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 2 - Atividades Presenciais
            var pEtapa2Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 2 (Presencial)",
                itemname = "Média Avaliações Etapa 2 (Presencial)",
                idnumber = "AE2A",
                grademax = 10,
                aggregation = 6,
                gradepass = 7,
                aggregateonlygraded = 0,
                parentcategoryid = AE2
            };
            var etapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesMetodos.SetCategorisInfo(pEtapa2Atividades).Result;
            AE2A = r.categoryids.FirstOrDefault();
            Console.Write(".");


            var pEtapa2AtividadesItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações 1 (Presencial)",
                itemname = "Nota Avaliações 1 (Presencial)",
                idnumber = "AE2A1",
                aggregation = 11,
                gradepass = 7,
                grademax = 10,
                aggregateonlygraded = 0,
                parentcategoryid = AE2A
            };
            var etapa2AtividadesItemMetodos = new core_grades_create_gradecategory();
            r = etapa2AtividadesItemMetodos.SetCategorisInfo(pEtapa2AtividadesItem).Result;
            Console.Write(".");


            var pEtapa2ParalelasItem = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Paralela 1 (Presencial)",
                itemname = "Nota Recuperação Paralela 1 (Presencial)",
                idnumber = "AE2R1",
                grademax = 10,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2A
            };

            var pEtapa2ParalelasMetodos = new core_grades_create_gradecategory();
            r = pEtapa2ParalelasMetodos.SetCategorisInfo(pEtapa2ParalelasItem).Result;
            Console.Write(".");

            //Etapa 1 - Recuperação à Distância
            var pEtapa2Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Etapa 2 (Presencial)",
                itemname = "Média Recuperação Etapa 2 (Presencial)",
                idnumber = "RE2R1",
                grademax = 10,
                gradepass = 7,
                aggregation = 11,
                parentcategoryid = AE2
            };
            var etapa2RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoMetodos.SetCategorisInfo(pEtapa2Recuperacao).Result;
            RE2R = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final",
                itemname = "Média Recuperação Final)",
                idnumber = "RF3R1",
                aggregation = 11,
                gradepass = 5,
                grademax = 10,
                aggregateonlygraded = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");
        }
        private static void SENAIGraduacaoPresencial(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, pEtapaAtividadeAvaliativas;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregateonlygraded = 1,
                aggregationcoef2 = 0,
                parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 1,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Etapa 1 -  SGE",
                itemname = "Nota Avaliações Etapa 1 - SGE",
                idnumber = "AE1A1",
                aggregation = 10,
                weightoverride = 1,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = regular
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            pEtapaAtividadeAvaliativas = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion


            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final - SGE",
                itemname = "Média Recuperação Final",
                idnumber = "RF3R1",
                aggregation = 6,
                weightoverride = 1,
                aggregateonlygraded = 1,
                grademax = 10,
                grademin = 0,
                gradepass = 5,
                decimals = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");
        }

        private static void SENAINovoEnsinoMedioTecnicoEAD(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre1, UT1;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 1

            //Trimestre 1 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre",
                itemname = "Trimestre",
                idnumber = "Trimestre",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 1º Trimestre
            var LSREMpEtapaUnidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades Trimestre",
                itemname = "Unidades Trimestre",
                idnumber = "UT1",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A",
                itemname = "Unidade 1 A",
                idnumber = "AE1A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B",
                itemname = "Unidade 1 B",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade1Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C",
                itemname = "Unidade 1 C",
                idnumber = "AE1A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2",
                itemname = "Unidade 2",
                idnumber = "AE1A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3",
                itemname = "Unidade 3",
                idnumber = "AE1A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade5).Result;
            Console.Write(".");

            //Recuperação 1º Trimestre
            var LSREMpEtapaRecuperacao1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Trimestre",
                itemname = "Recuperação Trimestre",
                idnumber = "RE1R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Recuperacao1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao1Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion


        }



        //##########    IEL    ##########

        private static void IELExtensaoEad(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            //Console.WriteLine(r.categoryid);
            #endregion

            #region Etapa Regular
            //Atividades Avaliativas
            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Atividades Avaliativas",
                itemname = "Atividades Avaliativas",
                idnumber = "AE2A1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
            };
            var etapaRegular = new core_grades_create_gradecategory();
            r = etapaRegular.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            //Console.WriteLine(regular);

            #endregion

        }

        private static void IELGraduacaoEad(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            //Console.WriteLine(r.categoryid);
            #endregion

            #region Etapa Regular
            //Atividades Avaliativas
            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Atividades Avaliativas - Integração SGE",
                itemname = "Atividades Avaliativas - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
            };
            var etapaRegular = new core_grades_create_gradecategory();
            r = etapaRegular.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            //Console.WriteLine(regular);

            #endregion

        }

        private static void IELPosGraduacaoEad(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            //Console.WriteLine(r.categoryid);
            #endregion

            #region Etapa Regular
            //Atividades Avaliativas
            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Atividades Avaliativas - Integração SGE",
                itemname = "Atividades Avaliativas - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 6,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
            };
            var etapaRegular = new core_grades_create_gradecategory();
            r = etapaRegular.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            //Console.WriteLine(regular);

            #endregion

        }

        private static void IELGraduacaoTecnologicaPresencial(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, pEtapaAtividadeAvaliativas;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                //parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            //Console.WriteLine(r.categoryid);
            #endregion           
            
            #region Etapa Regular
            //Atividades Avaliativas

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                grademax = 10,
                weightoverride = 1,
                gradepass = 7,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0

                //parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");


            var pEtapaAtividadeAvaliativa = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações",
                itemname = "Notas Avaliações",
                idnumber = "AE1A1",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                gradepass = 7,
                decimals = 1,
                grademax = 10,
                parentcategoryid = regular
            };
            var etapaRegular = new core_grades_create_gradecategory();
            r = etapaRegular.SetCategorisInfo(pEtapaAtividadeAvaliativa).Result;
            pEtapaAtividadeAvaliativas = r.categoryids.FirstOrDefault();
            //Console.WriteLine(regular);            

            #endregion

            var pEtapa3RecuperacaoFinal = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação Final",
                itemname = "Média Recuperação Final",
                idnumber = "RF3R1",
                gradepass = 5,
                aggregation = 6,
                grademax = 10,
                aggregateonlygraded = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa2RecuperacaoFinalMetodos = new core_grades_create_gradecategory();
            r = etapa2RecuperacaoFinalMetodos.SetCategorisInfo(pEtapa3RecuperacaoFinal).Result;
            Console.Write(".");

        }

        private static void IELSTIHL(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1A1;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var pAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                parentcategoryidnumber = "NT"
            };
            var etapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = etapaNaoAvaliativa.SetCategorisInfo(pAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa 1 - Avaliações à Distância

            var pEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 6,
                weightoverride = 1,
                grademax = 10,
                gradepass = 7,
                aggregationcoef2 = 1.0M,
                decimals = 1,
                parentcategoryidnumber = "NT"
            };
            var etapa = new core_grades_create_gradecategory();
            r = etapa.SetCategorisInfo(pEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var pEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Notas Avaliativas",
                itemname = "Média Notas Avaliativas",
                idnumber = "AE1A1",
                aggregation = 13,
                grademax = 10,
                gradepass = 7,
                decimals = 1,
                weightoverride = 1,
                parentcategoryid = regular
            };
            var etapa1Metodos = new core_grades_create_gradecategory();
            r = etapa1Metodos.SetCategorisInfo(pEtapa1).Result;
            AE1A1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Atividade 0,4
            var pEtapa1Atividades = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 1",
                itemname = "Nota Avaliação 1",
                idnumber = "AE1",
                aggregation = 6,
                grademax = 6,
                decimals = 1,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A1
            };
            var etapa1AtividadesMetodos = new core_grades_create_gradecategory();
            r = etapa1AtividadesMetodos.SetCategorisInfo(pEtapa1Atividades).Result;
            Console.Write(".");

            //Atividade 0,6
            var pEtapa1Recuperacao = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 2",
                itemname = "Nota Avaliação 2",
                idnumber = "AE2",
                aggregation = 6,
                grademax = 4,
                decimals = 1,
                aggregateonlygraded = 0,
                parentcategoryid = AE1A1
            };
            var etapa1RecuperacaoMetodos = new core_grades_create_gradecategory();
            r = etapa1RecuperacaoMetodos.SetCategorisInfo(pEtapa1Recuperacao).Result;
            Console.Write(".");
            #endregion
        }


        //##########    SESI    ##########

        private static void SESIEJAEnsinoMedioEouFundamental(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, AE1A, AE2A;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSEEMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 6,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
            };
            var LSEEMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSEEMetapaNaoAvaliativa.SetCategorisInfo(LSEEMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSEEMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 10,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                //parentcategoryidnumber = "NT"
            };
            var LSEEMetapaRegular = new core_grades_create_gradecategory();
            r = LSEEMetapaRegular.SetCategorisInfo(LSEEMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion


            #region Presencial
            //Etapa 2 - Avaliações Presenciais
            var LSEEMpEtapa2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação",
                itemname = "Avaliação",
                idnumber = "AE1A",
                aggregation = 13,
                weightoverride = 1,
                aggregationcoef2 = 0.6M,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = regular
            };
            var LSEEMetapa2AtividadesMetodos = new core_grades_create_gradecategory();
            r = LSEEMetapa2AtividadesMetodos.SetCategorisInfo(LSEEMpEtapa2).Result;
            AE2A = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades Presencial 1
            var LSEEMpEtapa2Atividades1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 03",
                itemname = "Avaliação 03",
                idnumber = "AE1A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = AE2A,
            };
            var LSEEMpEtapa2Atividades1Metodos = new core_grades_create_gradecategory();
            r = LSEEMpEtapa2Atividades1Metodos.SetCategorisInfo(LSEEMpEtapa2Atividades1).Result;
            Console.Write(".");

            //Etapa 1 - Atividades Presencial 2
            var LSEEMpEtapa2Atividades2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação 04",
                itemname = "Avaliação 04",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = AE2A,
            };
            var LSEEMetapa2Atividades2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa2Atividades2Metodos.SetCategorisInfo(LSEEMpEtapa2Atividades2).Result;
            Console.Write(".");
            #endregion


            #region On Line

            //Etapa 1 - Avaliações à Distância
            var LSEEMpEtapa1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliações Online",
                itemname = "Avaliações Online",
                idnumber = "AE2A",
                aggregation = 13,
                weightoverride = 1,
                aggregationcoef2 = 0.4M,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = regular
            };
            var LSEEMetapa1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Metodos.SetCategorisInfo(LSEEMpEtapa1).Result;
            AE1A = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Etapa 1 - Atividades à Distância 1
            var LSEEMpEtapa1Atividades1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação Online 01",
                itemname = "Avaliação Online 01",
                idnumber = "AE2A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = AE1A,
            };
            var LSEEMetapa1Atividades1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Atividades1Metodos.SetCategorisInfo(LSEEMpEtapa1Atividades1).Result;
            Console.Write(".");

            //Etapa 1 - Atividades à Distância 2
            var LSEEMpEtapa1Atividades2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Avaliação Online 02",
                itemname = "Avaliação Online 02",
                idnumber = "AE2A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 10,
                grademin = 0,
                gradepass = 7,
                decimals = 1,
                parentcategoryid = AE1A,
            };
            var LSEEMetapa1Atividades2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Atividades2Metodos.SetCategorisInfo(LSEEMpEtapa1Atividades2).Result;
            Console.Write(".");

            #endregion

        }

        private static void SESIRegularEnsinoMedio(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre1, UT1, Trimestre2, UT2, Trimestre3, UT3;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 1

            //Trimestre 1 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "1º Trimestre",
                itemname = "1º Trimestre",
                idnumber = "Trimestre1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 1º Trimestre
            var LSREMpEtapaUnidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades 1º Trimestre",
                itemname = "Unidades 1º Trimestre",
                idnumber = "UT1",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE1A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade1Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE1A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE1A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE1A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade5).Result;
            Console.Write(".");

            //Recuperação 1º Trimestre
            var LSREMpEtapaRecuperacao1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação 1º Trimestre - Integração SGE",
                itemname = "Recuperação 1º Trimestre - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Recuperacao1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao1Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Trimestre 2

            //Trimestre 2 
            var LSREMpTrim2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "2º Trimestre",
                itemname = "2º Trimestre",
                idnumber = "Trimestre2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim2Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim2Metodos.SetCategorisInfo(LSREMpTrim2).Result;
            Trimestre2= r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 2º Trimestre
            var LSREMpEtapaUnidade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades 2º Trimestre",
                itemname = "Unidades 2º Trimestre",
                idnumber = "UT2",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre2,
            };
            var LSREMetapa2Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa2Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade2).Result;
            UT2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade2Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade2Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE2A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade2Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE2A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade2Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE2A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade2Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE2A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade5).Result;
            Console.Write(".");

            //Recuperação 2º Trimestre
            var LSREMpEtapaRecuperacao2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação 2º Trimestre - Integração SGE",
                itemname = "Recuperação 2º Trimestre - Integração SGE",
                idnumber = "RE2R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre2,
            };
            var LSREMetapa1Recuperacao2Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao2Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao2).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Trimestre 3

            //Trimestre  
            var LSREMpTrim3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "3º Trimestre",
                itemname = "3º Trimestre",
                idnumber = "Trimestre3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim3Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim3Metodos.SetCategorisInfo(LSREMpTrim3).Result;
            Trimestre3 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 3º Trimestre
            var LSREMpEtapaUnidade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades 3º Trimestre",
                itemname = "Unidades 3º Trimestre",
                idnumber = "UT3",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre3,
            };
            var LSREMetapa3Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa3Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade3).Result;
            UT3 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade3Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE3A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa1Unidade3Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade3Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE3A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa1Unidade3Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade3Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE3A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa1Unidade3Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade3Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE3A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa1Unidade3Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade3Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE3A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa1Unidade3Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade5).Result;
            Console.Write(".");

            //Recuperação 1º Trimestre
            var LSREMpEtapaRecuperacao3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação 3º Trimestre - Integração SGE",
                itemname = "Recuperação 3º Trimestre - Integração SGE",
                idnumber = "RE3R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre3,
            };
            var LSREMetapa1Recuperacao3Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao3Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao3).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion



        }

        private static void SESINEMApenas1Trimestre(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre, UT1;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 

            //Trimestre 1 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre",
                itemname = "Trimestre",
                idnumber = "Trimestre",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 1º Trimestre
            var LSREMpEtapaUnidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades Trimestre",
                itemname = "Unidades Trimestre",
                idnumber = "UT1",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre,
            };
            var LSREMetapa1Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE1A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade1Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE1A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE1A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE1A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade5).Result;
            Console.Write(".");

            //Recuperação 1º Trimestre
            var LSREMpEtapaRecuperacao1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação - Integração SGE",
                itemname = "Recuperação - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre,
            };
            var LSREMetapa1Recuperacao1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao1Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion



        }

        private static void SESINEMApenas2Trimestres(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre1, UT1, Trimestre2, UT2;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 1

            //Trimestre 1 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "1º Trimestre",
                itemname = "1º Trimestre",
                idnumber = "Trimestre1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 1º Trimestre
            var LSREMpEtapaUnidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades 1º Trimestre",
                itemname = "Unidades 1º Trimestre",
                idnumber = "UT1",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE1A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade1Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE1A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE1A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade1Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE1A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade5).Result;
            Console.Write(".");

            //Recuperação 1º Trimestre
            var LSREMpEtapaRecuperacao1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação 1º Trimestre - Integração SGE",
                itemname = "Recuperação 1º Trimestre - Integração SGE",
                idnumber = "RE1R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Recuperacao1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao1Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion

            #region Trimestre 2

            //Trimestre 2 
            var LSREMpTrim2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "2º Trimestre",
                itemname = "2º Trimestre",
                idnumber = "Trimestre2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim2Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim2Metodos.SetCategorisInfo(LSREMpTrim2).Result;
            Trimestre2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades 2º Trimestre
            var LSREMpEtapaUnidade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades 2º Trimestre",
                itemname = "Unidades 2º Trimestre",
                idnumber = "UT2",
                aggregation = 11,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre2,
            };
            var LSREMetapa2Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa2Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade2).Result;
            UT2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade2Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A - Integração SGE",
                itemname = "Unidade 1 A - Integração SGE",
                idnumber = "AE2A1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade2Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B - Integração SGE",
                itemname = "Unidade 1 B - Integração SGE",
                idnumber = "AE2A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade2Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 C - Integração SGE",
                itemname = "Unidade 1 C - Integração SGE",
                idnumber = "AE2A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade3).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade2Atividade4 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 2 - Integração SGE",
                itemname = "Unidade 2 - Integração SGE",
                idnumber = "AE2A4",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao4Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao4Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade4).Result;
            Console.Write(".");

            //Unidade 2
            var LSREMpEtapaUnidade2Atividade5 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 3 - Integração SGE",
                itemname = "Unidade 3 - Integração SGE",
                idnumber = "AE2A5",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa1Unidade2Avaliacao5Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao5Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade5).Result;
            Console.Write(".");

            //Recuperação 2º Trimestre
            var LSREMpEtapaRecuperacao2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Recuperação 2º Trimestre - Integração SGE",
                itemname = "Recuperação 2º Trimestre - Integração SGE",
                idnumber = "RE2R1",
                aggregation = 6,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 70,
                decimals = 0,
                parentcategoryid = Trimestre2,
            };
            var LSREMetapa1Recuperacao2Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Recuperacao2Metodos.SetCategorisInfo(LSREMpEtapaRecuperacao2).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            #endregion



        }

        private static void SESINEMItinerarioInformativo(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre01, Trimestre02, Trimestre03;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 100,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 1

            //Trimestre 1 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 01",
                itemname = "Trimestre 01",
                idnumber = "Trimestre01",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre01 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T1 - Avaliação On-Line 01",
                itemname = "Nota T1 - Avaliação On-Line 01",
                idnumber = "AE1A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre01,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade1Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T1 - Avaliação On-Line 02",
                itemname = "Nota T1 - Avaliação On-Line 02",
                idnumber = "AE1A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre01,
            };
            var LSEEMetapa1Unidade1Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade3).Result;
            Console.Write(".");

            #endregion

            #region Trimestre 2

            //Trimestre 2 
            var LSREMpTrim2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 02",
                itemname = "Trimestre 02",
                idnumber = "Trimestre02",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim2Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim2Metodos.SetCategorisInfo(LSREMpTrim2).Result;
            Trimestre02 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade2Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T2 - Avaliação On-Line 01",
                itemname = "Nota T2 - Avaliação On-Line 01",
                idnumber = "AE2A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre02,
            };
            var LSEEMetapa1Unidade2Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade2Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T2 - Avaliação On-Line 02",
                itemname = "Nota T2 - Avaliação On-Line 02",
                idnumber = "AE2A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre02,
            };
            var LSEEMetapa1Unidade2Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade2Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade2Atividade3).Result;
            Console.Write(".");

            #endregion

            #region Trimestre 3

            //Trimestre 3
            var LSREMpTrim3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 03",
                itemname = "Trimestre 03",
                idnumber = "Trimestre03",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim3Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim3Metodos.SetCategorisInfo(LSREMpTrim3).Result;
            Trimestre03 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade3Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T3 - Avaliação On-Line 01",
                itemname = "Nota T3 - Avaliação On-Line 01",
                idnumber = "AE3A2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre03,
            };
            var LSEEMetapa1Unidade3Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade2).Result;
            Console.Write(".");

            //Unidade 1 C
            var LSREMpEtapaUnidade3Atividade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "T3 - Avaliação On-Line 02",
                itemname = "Nota T3 - Avaliação On-Line 02",
                idnumber = "AE3A3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = Trimestre03,
            };
            var LSEEMetapa1Unidade3Avaliacao3Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade3Avaliacao3Metodos.SetCategorisInfo(LSREMpEtapaUnidade3Atividade3).Result;
            Console.Write(".");

            #endregion


        }

        private static void SESIRegularEnsinoMedioApenasTrimestreSemIntegraçãoDeNotas(int pIdSala)
        {
            int courseId = pIdSala;
            Retorno r;
            int regular, Trimestre1, UT1, Trimestre2, UT2, Trimestre3, UT3;
            #region Atividades Não Avaliativas

            //Atividades Não Avaliativas

            var LSREMpAtividadesNaoAvaliativas = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Não Avaliativa",
                itemname = "Não Avaliativa",
                idnumber = "NaoAvaliativa",
                aggregation = 0,
                weightoverride = 1,
                hiddenuntil = 1,
                aggregationcoef2 = 0,
                aggregateonlygraded = 1,
                grademax = 50,
                grademin = 0,
                gradepass = 0,
                decimals = 0,
            };
            var LSREMetapaNaoAvaliativa = new core_grades_create_gradecategory();
            r = LSREMetapaNaoAvaliativa.SetCategorisInfo(LSREMpAtividadesNaoAvaliativas).Result;

            Console.Write(".");
            #endregion

            #region Etapa Regular
            //Etapa REGULAR

            var LSREMpEtapaRegular = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Etapa Regular",
                itemname = "Média Etapa Regular",
                idnumber = "REGULAR",
                aggregation = 11,
                weightoverride = 1,
                aggregationcoef2 = 1.0M,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                //parentcategoryidnumber = "NT"
            };
            var LSREMetapaRegular = new core_grades_create_gradecategory();
            r = LSREMetapaRegular.SetCategorisInfo(LSREMpEtapaRegular).Result;
            regular = r.categoryids.FirstOrDefault();
            Console.Write(".");
            #endregion

            #region Trimestre 1

            //Trimestre 
            var LSREMpTrim1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 1",
                itemname = "Trimestre 1",
                idnumber = "Trimestre1",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim1Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim1Metodos.SetCategorisInfo(LSREMpTrim1).Result;
            Trimestre1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades Trimestre
            var LSREMpEtapaUnidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades Trimestre 1",
                itemname = "Unidades Trimestre 1",
                idnumber = "UT1",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = Trimestre1,
            };
            var LSREMetapa1Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa1Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1).Result;
            UT1 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapaUnidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A",
                itemname = "Unidade 1 A",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapaUnidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B",
                itemname = "Unidade 1 B",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT1,
            };
            var LSEEMetapa1Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa1Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapaUnidade1Atividade2).Result;
            Console.Write(".");

            #endregion

            #region Trimestre 2

            //Trimestre 
            var LSREMpTrim2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 2",
                itemname = "Trimestre 2",
                idnumber = "Trimestre2",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim2Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim2Metodos.SetCategorisInfo(LSREMpTrim2).Result;
            Trimestre2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades Trimestre
            var LSREMpEtapa2Unidade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades Trimestre 2",
                itemname = "Unidades Trimestre 2",
                idnumber = "UT2",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = Trimestre2,
            };
            var LSREMetapa2Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa2Unidades1Metodos.SetCategorisInfo(LSREMpEtapa2Unidade1).Result;
            UT2 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapa2Unidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A",
                itemname = "Unidade 1 A",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa2Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa2Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapa2Unidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapa2Unidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B",
                itemname = "Unidade 1 B",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT2,
            };
            var LSEEMetapa2Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa2Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapa2Unidade1Atividade2).Result;
            Console.Write(".");

            #endregion

            #region Trimestre 3

            //Trimestre 
            var LSREMpTrim3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Trimestre 3",
                itemname = "Trimestre 3",
                idnumber = "Trimestre3",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = regular
            };
            var LSREMTrim3Metodos = new core_grades_create_gradecategory();
            r = LSREMTrim3Metodos.SetCategorisInfo(LSREMpTrim3).Result;
            Trimestre3 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidades Trimestre
            var LSREMpEtapaUnidade3 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidades Trimestre 3",
                itemname = "Unidades Trimestre 3",
                idnumber = "UT3",
                aggregation = 13,
                aggregateonlygraded = 0,
                grademax = 50,
                grademin = 0,
                gradepass = 35,
                decimals = 0,
                parentcategoryid = Trimestre3,
            };
            var LSREMetapa3Unidades1Metodos = new core_grades_create_gradecategory();
            r = LSREMetapa3Unidades1Metodos.SetCategorisInfo(LSREMpEtapaUnidade3).Result;
            UT3 = r.categoryids.FirstOrDefault();
            Console.Write(".");

            //Unidade 1 A
            var LSREMpEtapa3Unidade1Atividade1 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 A",
                itemname = "Unidade 1 A",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa3Unidade1Avaliacao1Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa3Unidade1Avaliacao1Metodos.SetCategorisInfo(LSREMpEtapa3Unidade1Atividade1).Result;
            Console.Write(".");

            //Unidade 1 B
            var LSREMpEtapa3Unidade1Atividade2 = new CategoryCreateParams()
            {
                courseid = courseId,
                fullname = "Unidade 1 B",
                itemname = "Unidade 1 B",
                aggregation = 6,
                aggregateonlygraded = 0,
                grademax = 25,
                grademin = 0,
                gradepass = 13,
                decimals = 0,
                parentcategoryid = UT3,
            };
            var LSEEMetapa3Unidade1Avaliacao2Metodos = new core_grades_create_gradecategory();
            r = LSEEMetapa3Unidade1Avaliacao2Metodos.SetCategorisInfo(LSREMpEtapa3Unidade1Atividade2).Result;
            Console.Write(".");

            #endregion

        }

    }
}
