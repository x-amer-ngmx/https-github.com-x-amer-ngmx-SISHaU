using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using ImportResult = Integration.HouseManagement.ImportResult;
using SISHaU.Library.API;

namespace SISHaU.Adapter.HouseManagement
{
    public class AccountDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        private NsiCommonDataModel NsiModel { get; }
        public List<importAccountRequestAccount> Accounts { get; set; }
        public List<long> AccountIds { get; set; }

        public AccountDataModel()
        {
            Accounts = new List<importAccountRequestAccount>();
            NsiModel = new NsiCommonDataModel();
            AccountIds = new List<long>();
        }

        public exportAccountResult ExportAccounts(string fiasHouseGuid)
        {
            var request = GenerateGenericType<exportAccountRequest>();
            request.FIASHouseGuid = fiasHouseGuid;
            return ProcessRequest<exportAccountResult>(request);
        }

        public importAccountRequest CreateRequest()
        {
            var request = GenerateGenericType<importAccountRequest>();
            request.Account = Accounts.ToArray();
            return request;
        }

        public void AddLivingRoom(importAccountRequestAccount account, string item)
        {
            account.Accommodation = new[] {
                new AccountTypeAccommodation{
                    LivingRoomGUID = item,
                    LivingRoomGUIDSpecified = true
            }};
        }

        public void AddHouse(importAccountRequestAccount account, string fiasHoseGuid)
        {
            account.Accommodation = new[] {
                new AccountTypeAccommodation{
                    FIASHouseGuid = fiasHoseGuid,
                    FIASHouseGuidSpecified = true,
                    SharePercent = decimal.Truncate(100 / (decimal)account.LivingPersonsNumber)
            }};
        }

        public void AddPremises(importAccountRequestAccount account, string premiseGuid)
        {
            account.Accommodation = new[] {
                new AccountTypeAccommodation{
                    PremisesGUID = premiseGuid,
                    PremisesGUIDSpecified = true,
                    SharePercent = decimal.Truncate(100 / (decimal)account.LivingPersonsNumber)
            }};
        }

        public AccountTypePayerInfo AddPayerInfo(string name)
        {
            var sName = name.Replace(".", "").Split(';');
            var atpi = new AccountTypePayerInfo
            {
                Ind = new AccountIndType
                {
                    FirstName = string.IsNullOrEmpty(sName[0]) ? "Нет данных" : sName[0],
                    Surname = string.IsNullOrEmpty(sName[1]) ? "Нет данных" : sName[1],
                    Patronymic = string.IsNullOrEmpty(sName[2]) ? "Нет данных" : sName[2]
                }
            };

            var rex = new Regex("$вна", RegexOptions.IgnoreCase);
            atpi.Ind.Sex = rex.IsMatch(atpi.Ind.Patronymic) ? AccountIndTypeSex.F : AccountIndTypeSex.M;
            
            return atpi;
        }

        public importAccountRequestAccount AddAccount(string accountNumber, string contractNumber, string name, decimal square, int personsCount, string accountGuidForUpdate = null)
        {
            var account = new importAccountRequestAccount
            {
                TransportGUID = TransportGuid,
                AccountGUID = string.IsNullOrEmpty(accountGuidForUpdate) ? null : accountGuidForUpdate,
                isRSOAccount = true,
                AccountNumber = accountNumber,
                PayerInfo = AddPayerInfo(name),
                HeatedArea = square,
                TotalSquare = square,
                ResidentialSquare = square,
                LivingPersonsNumber = (sbyte)personsCount,
                CreationDate = DateTime.Parse("11.04.2017"),
                AccountReasons = new AccountReasonsImportType
                {
                    SupplyResourceContract = new AccountReasonsImportTypeSupplyResourceContract
                    {
                        //ContractGUID = "ce70a9ad-bc24-42c9-940c-e04f2cc3aeed",
                        ContractNumber = contractNumber,
                        SigningDate = DateTime.Parse("11.04.2017")
                    }
                }
            };

            Accounts.Add(account);

            return account;
        }

        public getStateRequest BeginProcessRequest()
        {
            return BeginProcessRequest(CreateRequest());
        }

        public ImportResult ProcessRequest()
        {
            return ProcessRequest<ImportResult>(CreateRequest());
        }

        public void AddClosedAccount(string accountGuid)
        {
            var account = new importAccountRequestAccount
            {
                TransportGUID = TransportGuid,
                AccountGUID = accountGuid,
                Closed = new ClosedAccountAttributesType
                {
                    CloseDate = DateTime.Parse("11.04.2017"),
                    CloseReason = NsiModel.GetClosedReason("Ошибка ввода")
                },
                isRSOAccount = true,
                PayerInfo = AddPayerInfo(";;")
            };

            AddLivingRoom(account, TransportGuid);
            
            Accounts.Add(account);
        }
    }
}
