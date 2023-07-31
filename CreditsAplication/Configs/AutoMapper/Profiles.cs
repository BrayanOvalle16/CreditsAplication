using AutoMapper;
using CreditsAplication.Api.Dtos;
using CreditsAplication.Api.Models;

namespace CreditsAplication.Configs.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles() {
            //Client 
            CreateMap<ClientCreationDto, Client>();
            CreateMap<ClientUpdateDto, Client>();
            CreateMap<Client, ClientDto>();
            CreateMap<Client, ClientUpdateDto>();


            //Person
            CreateMap<PersonCreationDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<PersonDto, Person>();
            CreateMap<Person, PersonDto>();

            //Transaction
            CreateMap<TransactionDto, Transaction>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionCreationDto, Transaction>();
            CreateMap<TransactionUpdateDto, Transaction>();
            CreateMap<Transaction, TransactionUpdateDto>();

            //Account 
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountDto>();
            CreateMap<AccountCreationDto, Account>();
            CreateMap<AccountUpdateDto, Account>();
            CreateMap<Account, AccountUpdateDto>();


        }
    }
}
