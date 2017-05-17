
using System;
using Integration.HouseManagement;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FResourceContractOperation.ResourceContract
{
    public class ResourceContractModel : BaseModel<ResourceContractEnginer>
    {
        private importSupplyResourceContractRequestContract SupplyContract { get; }

        public ResourceContractModel(ResourceContractEnginer baseModel) : base(baseModel)
        {
            SupplyContract = new importSupplyResourceContractRequestContract
            {
                TransportGUID = Guid.NewGuid().ToString()
            };
        }

        public ResourceContractModel Create()
        {
            //TODO:надо разобраться и доделать
            SupplyContract.ContractGUID = Guid.NewGuid().ToString();
            return this;
        }


        public ResourceContractModel Update(string docId)
        {
            //TODO:надо разобраться и доделать
            SupplyContract.ContractGUID = docId;
            return this;
        }


        public ResourceContractModel RollOver(DateTime dateRollOver)
        {
            SupplyContract.RollOverContract.RollOverDate = dateRollOver;
            return this;
        }

        public ResourceContractModel Cancelation(string reason)
        {
            SupplyContract.AnnulmentContract.ReasonOfAnnulment = reason;
            return this;
        }
    }
}
