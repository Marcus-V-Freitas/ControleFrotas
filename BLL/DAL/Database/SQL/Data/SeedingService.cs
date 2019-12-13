using ControleFrotasDLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFrotasDLL.DAL.Database.SQL.Data
{
    /*
     * Classe Responsável por popular os dados iniciais principais do banco de dados, 
     * É ativada se as tabelas abaixo estiverem vazias e se o site estiver em ambiente de desenvolvimento
     */
    public class SeedingService
    {
        private ControleFrotasContext _banco;

        public SeedingService(ControleFrotasContext banco)
        {
            _banco = banco;
        }

        //Método de preenchimento de tabelas (Verifica se vazio)
        public void seed()
        {
            if (_banco.CategoriaVeiculos.Any() ||
                _banco.Veiculos.Any() ||
                _banco.Marcas.Any() ||
                _banco.Modelos.Any() || 
                _banco.VeiculosEmpresa.Any() ||
                _banco.UnidadeMedidas.Any() ||
                _banco.Fornecedores.Any() ||
                _banco.Seguros.Any())
            {
                return;
            }

            CategoriaVeiculo cv1 = new CategoriaVeiculo("A", "Motos e Tricicolos");
            CategoriaVeiculo cv2 = new CategoriaVeiculo("B", "Carros de Passeio");
            CategoriaVeiculo cv3 = new CategoriaVeiculo("C", "Veículos de carga superior á 3,5 ton ");
            CategoriaVeiculo cv4 = new CategoriaVeiculo("D", "Veículos com mais de 8 passageiros");
            CategoriaVeiculo cv5 = new CategoriaVeiculo("E", "Veículos com unidade acoplada acima de 6 ton");


            Marca ma1 = new Marca("Ford");
            Marca ma2 = new Marca("Chevrolet");
            Marca ma3 = new Marca("Fiat");
            Marca ma4 = new Marca("Mercedes");
            Marca ma5 = new Marca("BMW");
            Marca ma6 = new Marca("Audi");
            Marca ma7 = new Marca("Nissan");
            Marca ma8 = new Marca("Jac");
            Marca ma9 = new Marca("Honda");
            Marca ma10 = new Marca("Peugeot");
            Marca ma11 = new Marca("Renault");
            Marca ma12 = new Marca("Volkswagen");
            Marca ma13 = new Marca("Toyota");

            Modelo mo1 = new Modelo("New Fiesta", ma1);
            Modelo mo2 = new Modelo("Fusion", ma1);
            Modelo mo3 = new Modelo("Onix", ma2);
            Modelo mo4 = new Modelo("Cobalt", ma2);
            Modelo mo5 = new Modelo("Estrada", ma3);
            Modelo mo6 = new Modelo("Uno", ma3);
            Modelo mo7 = new Modelo("C180", ma4);
            Modelo mo8 = new Modelo("Sprint", ma4);
            Modelo mo9 = new Modelo("X1", ma5);
            Modelo mo10 = new Modelo("320i", ma5);
            Modelo mo11 = new Modelo("A3", ma6);
            Modelo mo12 = new Modelo("Q5", ma6);
            Modelo mo13 = new Modelo("kicks", ma7);
            Modelo mo14 = new Modelo("Smart", ma7);
            Modelo mo15 = new Modelo("T40", ma8);
            Modelo mo16 = new Modelo("T50", ma8);
            Modelo mo17 = new Modelo("Civic", ma9);
            Modelo mo18 = new Modelo("City", ma9);
            Modelo mo19 = new Modelo("208", ma10);
            Modelo mo20 = new Modelo("308", ma10);
            Modelo mo21 = new Modelo("Sandero", ma11);
            Modelo mo22 = new Modelo("Kwid", ma11);
            Modelo mo23 = new Modelo("Voyage", ma12);
            Modelo mo24 = new Modelo("Golf", ma12);
            Modelo mo25 = new Modelo("Corolla", ma13);
            Modelo mo26 = new Modelo("Yaris", ma13);

            Veiculo ve1 = new Veiculo("NAT4483", "71523578945", "d", mo1, cv2);
            Veiculo ve2 = new Veiculo("KBH9416", "79991575690", "d", mo2, cv2);
            Veiculo ve3 = new Veiculo("LVP3598", "42512669614", "d", mo3, cv2);
            Veiculo ve4 = new Veiculo("HQW4056", "56059184910", "d", mo4, cv2);
            Veiculo ve5 = new Veiculo("KZX8751", "52957189993", "d", mo5, cv2);
            Veiculo ve6 = new Veiculo("NER4887", "22955510431", "d", mo6, cv2);
            Veiculo ve7 = new Veiculo("JFZ0148", "95954836754", "d", mo7, cv2);
            Veiculo ve8 = new Veiculo("BCP7805", "88401385128", "d", mo8, cv4);
            Veiculo ve9 = new Veiculo("AJI3429", "85331002921", "d", mo9, cv2);
            Veiculo ve10 = new Veiculo("GMJ4837", "26359501986", "d", mo10, cv2);
            Veiculo ve11 = new Veiculo("GPM9653", "77161055727", "d", mo11, cv2);
            Veiculo ve12 = new Veiculo("ERY0096", "06457805578", "d", mo12, cv2);
            Veiculo ve13 = new Veiculo("HSU6911", "58101597926", "d", mo13, cv2);
            Veiculo ve14 = new Veiculo("HBZ3621", "75055881248", "d", mo14, cv2);
            Veiculo ve15 = new Veiculo("NEU0334", "48300818160", "d", mo15, cv2);
            Veiculo ve16 = new Veiculo("KAH5893", "87987938032", "d", mo16, cv2);
            Veiculo ve17 = new Veiculo("AFL4580", "17105666690", "d", mo17, cv2);
            Veiculo ve18 = new Veiculo("MVE4589", "12696313875", "d", mo18, cv2);
            Veiculo ve19 = new Veiculo("LZE3740", "13732203416", "d", mo19, cv2);
            Veiculo ve20 = new Veiculo("MTG4507", "32779198245", "d", mo20, cv2);
            Veiculo ve21 = new Veiculo("NER4259", "59896361381", "d", mo21, cv2);
            Veiculo ve22 = new Veiculo("NEZ0679", "45287478548", "d", mo22, cv2);
            Veiculo ve23 = new Veiculo("NBP9301", "25400681973", "d", mo23, cv2);
            Veiculo ve24 = new Veiculo("KLY6758", "21545635260", "d", mo24, cv2);
            Veiculo ve25 = new Veiculo("IWK2599", "93381174328", "d", mo25, cv2);
            Veiculo ve26 = new Veiculo("AHB9756", "25258427359", "d", mo26, cv2);


            VeiculoEmpresa vem1 = new VeiculoEmpresa(ve1,150);
            VeiculoEmpresa vem2 = new VeiculoEmpresa(ve2, 150);
            VeiculoEmpresa vem3 = new VeiculoEmpresa(ve3, 150);
            VeiculoEmpresa vem4 = new VeiculoEmpresa(ve4, 150);
            VeiculoEmpresa vem5 = new VeiculoEmpresa(ve5, 150);
            VeiculoEmpresa vem6 = new VeiculoEmpresa(ve6, 150);
            VeiculoEmpresa vem7 = new VeiculoEmpresa(ve7, 150);
            VeiculoEmpresa vem8 = new VeiculoEmpresa(ve8, 150);
            VeiculoEmpresa vem9 = new VeiculoEmpresa(ve9, 150);
            VeiculoEmpresa vem10 = new VeiculoEmpresa(ve10, 150);
            VeiculoEmpresa vem11 = new VeiculoEmpresa(ve11, 150);
            VeiculoEmpresa vem12 = new VeiculoEmpresa(ve12,  150);
            VeiculoEmpresa vem13 = new VeiculoEmpresa(ve13, 150);
            VeiculoEmpresa vem14 = new VeiculoEmpresa(ve14, 150);
            VeiculoEmpresa vem15 = new VeiculoEmpresa(ve15, 150);
            VeiculoEmpresa vem16 = new VeiculoEmpresa(ve16, 150);
            VeiculoEmpresa vem17 = new VeiculoEmpresa(ve17, 150);
            VeiculoEmpresa vem18 = new VeiculoEmpresa(ve18, 150);
            VeiculoEmpresa vem19 = new VeiculoEmpresa(ve19, 150);
            VeiculoEmpresa vem20 = new VeiculoEmpresa(ve20, 150);
            VeiculoEmpresa vem21 = new VeiculoEmpresa(ve21,  150);
            VeiculoEmpresa vem22 = new VeiculoEmpresa(ve22,  150);
            VeiculoEmpresa vem23 = new VeiculoEmpresa(ve23,  150);
            VeiculoEmpresa vem24 = new VeiculoEmpresa(ve24,  150);
            VeiculoEmpresa vem25 = new VeiculoEmpresa(ve25,  150);
            VeiculoEmpresa vem26 = new VeiculoEmpresa(ve26,  150);

            UnidadeMedida un1 = new UnidadeMedida("un");
            UnidadeMedida un2 = new UnidadeMedida("cx");
            UnidadeMedida un3 = new UnidadeMedida("kg");
            UnidadeMedida un4 = new UnidadeMedida("g");
            UnidadeMedida un5 = new UnidadeMedida("it");
            UnidadeMedida un6 = new UnidadeMedida("ton");

            Fornecedor for1 = new Fornecedor("Bradesco","N/A" , "0800 701 7000");
            Fornecedor for2 = new Fornecedor("Banco do Brasil", "N/A", "0800 773 3000");
            Fornecedor for3 = new Fornecedor("SulAmérica", "N/A", "0800 702 2242");
            Fornecedor for4 = new Fornecedor("Porto Seguro", "N/A", "0800 727 2766");
            Fornecedor for5 = new Fornecedor("Caixa Seguros", "N/A", "0800 702 4000");
            Fornecedor for6 = new Fornecedor("Marítima-Yasuda", "N/A", "0800 771 9719");

            Seguro seg1 = new Seguro("Seguro Auto Light",0.41,for1);
            Seguro seg2 = new Seguro("Bradesco Seguro Auto", 0.60, for1);
            Seguro seg3 = new Seguro("Bradesco Auto Assistência Total", 1.00, for1);
            Seguro seg4 = new Seguro("Coberturas Básicas", 0.80, for2);
            Seguro seg5 = new Seguro("Garantias Adicionais", 1.20, for2);
            Seguro seg6 = new Seguro("Compreensiva",0.55, for3);
            Seguro seg7 = new Seguro("Indenização Integral",0.50, for3);
            Seguro seg8 = new Seguro("SulAmérica Roubo e Furto",1.40, for3);
            Seguro seg9 = new Seguro("Coberturas Básicas",0.75,for4);
            Seguro seg10 = new Seguro("Coberturas Adicionais", 2.05, for4);
            Seguro seg11 = new Seguro("Colisão",0.65, for5);
            Seguro seg12 = new Seguro("Roubo e furto", 0.70, for5);
            Seguro seg13 = new Seguro("Danos materiais a terceiros", 1.40, for5);
            Seguro seg14 = new Seguro("Coberturas Básicas", 0.55, for6);
            Seguro seg15 = new Seguro("Garantias Adicionais", 1.60, for6);

            Colaborador col1 = new Colaborador("Marcus Vinicius", "marcus@yahoo.com.br", "223344", "G");

            _banco.CategoriaVeiculos.AddRange(cv1, cv2, cv3, cv4, cv5);

            _banco.Marcas.AddRange(ma1, ma2, ma3, ma4, ma5, ma6, ma7, ma8, ma9, ma10, ma11, ma12, ma13);

            _banco.Modelos.AddRange(mo1, mo2, mo3, mo4, mo5, mo6, mo7, mo8, mo9, mo10, mo11, mo12, mo13,
                                     mo14, mo15, mo16, mo17, mo18, mo19, mo20, mo21, mo22, mo23, mo24, mo25, mo26);
            _banco.Veiculos.AddRange(ve1, ve2, ve3, ve4, ve5, ve6, ve7, ve8, ve9, ve10, ve11, ve12, ve13, ve14,
                                    ve15, ve16, ve17, ve18, ve19, ve20, ve21, ve22, ve23, ve24, ve25, ve26);

            _banco.SaveChanges();
            _banco.VeiculosEmpresa.AddRange(vem1, vem2, vem3, vem4, vem5, vem6, vem7, vem8, vem9, vem10, vem11, vem12, vem13, vem14,
                                   vem15, vem16, vem17, vem18, vem19, vem20, vem21, vem22, vem23, vem24, vem25, vem26);

            _banco.UnidadeMedidas.AddRange(un1, un2, un3, un4, un5, un6);

            _banco.Fornecedores.AddRange(for1, for2, for3, for4, for5, for6);

            _banco.Seguros.AddRange(seg1, seg2, seg3, seg4, seg5, seg6, seg7, seg8, seg9, seg10, seg11, seg12, seg13, seg14, seg15);

            _banco.Colaboradores.AddRange(col1);

            _banco.SaveChanges();
        }
    }
}
