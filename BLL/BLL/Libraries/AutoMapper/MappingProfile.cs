using AutoMapper;
using ControleFrotasDLL.BLL.Libraries.Constants;
using ControleFrotasDLL.BLL.Libraries.IntermediarioJS;
using Newtonsoft.Json;
using PagarMe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ControleFrotasDLL.BLL.Libraries.AutoMapper
{
    public class MappingProfile:Profile
    {
        /*
        * Classe usada para mapear objetos sem relação, podendo-se passar atributos entre essas classes
        */
        public MappingProfile()
        {
            CreateMap<Transaction, TransacaoPagarMe>();

            CreateMap<TransacaoPagarMe, AluguelPagarMe>()

                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(orig => orig.Id))
                .ForMember(dest => dest.FormaPagamento, opt => opt.MapFrom(orig => (orig.PaymentMethod == 0) ? MetodoPagamentoConstant.CartaoCredito : MetodoPagamentoConstant.Boleto))
                .ForMember(dest => dest.DadosTransaction, opt => opt.MapFrom(orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));
        }
    }
}