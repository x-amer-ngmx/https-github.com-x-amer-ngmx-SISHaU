using System.Collections.Generic;
using Integration.HouseManagement;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement.SupplyResourceContract
{
    public class SupplyResourceContractModel<T, TU> : BaseModel<T, TU, HouseManagementModel> where T : class
    {
        public importSupplyResourceContractRequest ImportRequest { get; set; }
        public IList<importSupplyResourceContractRequestContract> Contracts { get; set; }

        //public Export Export()
        //{
        //    return new Export(this);
        //}

        //public Import Import()
        //{
        //    return new Import(this);
        //}

        public SupplyResourceContractModel(HouseManagementModel baseModel, IDictionary<T, TU> modelRequest) 
            : base(baseModel, modelRequest)
        {
            Contracts = new List<importSupplyResourceContractRequestContract>();
        }
    }

    //public class Export : EntityGenerator<Export>
    //{
    //    private readonly Import _entity;
    //    public Import Terminate()
    //    {

    //        return new Import(_entity);
    //    }

    //    public Import RollOver()
    //    {
    //        return new Import(_entity);
    //    }

    //    public AnnulmentContract Annulate()
    //    {
    //        var contract = new AnnulmentType();
            
    //        return new AnnulmentContract(contract, _entity);
    //    }

    //    public Import Update()
    //    {
    //        return new Import(_entity, BaseModelEntity);
    //    }

    //    public Export(Export entity, SupplyResourceContractModel baseModel) : base(entity, baseModel)
    //    {
    //        _entity = new Import(null, baseModel);
    //    }

    //    public Export(SupplyResourceContractModel baseModel) : base(null, baseModel){
    //        _entity = new Import(null, baseModel);
    //    }
    //}

    //public class AnnulmentContract : EntityGenerator<AnnulmentType, Import>
    //{
    //    public AnnulmentContract(AnnulmentType annulmentType, Import baseModel) : base(annulmentType, baseModel) { }
    //}

    //public class Import //: EntityGenerator<Import/*, SupplyResourceContractModel*/> 
    //{
    //    private importSupplyResourceContractRequestContract Contract { get; set; }
    //    //public Import(SupplyResourceContractModel baseModel) : base(baseModel){}
    //    //public Import(Import entity, SupplyResourceContractModel baseModel) : base(entity, baseModel){}

    //    private AnnulmentType annulmentType { get; set; }

    //    public AnnulmentContract Annulate()
    //    {
    //        annulmentType = new AnnulmentType();
    //        return new AnnulmentContract(annulmentType, this);
    //    }

    //    //ToDo Надо переделать
    //    //public override SupplyResourceContractModel Push()
    //    //{
    //    //    BaseModelEntity.Contracts.Add(Contract);
    //    //    return BaseModelEntity;
    //    //}
    //}
}
