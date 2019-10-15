using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Check;
using Check.Models.Maintenance;
using Models.Authentication;
using Models.Maintenance.Equipment;
using Models.Shared;
using Utility;
using Utility.Models;

namespace DataAccessor.Maintenance
{
    public class EquipmentDataAccessor
    {
        public static RequestResult GetTreeItems(List<Organization> organizationList, Guid organizationId, Account account)
        {
            RequestResult result = new RequestResult();

            try
            {
                var treeItemList = new List<TreeItem>();

                var attributes = new Dictionary<Define.EnumTreeAttribute, string>()
                {
                    { Define.EnumTreeAttribute.NodeType, string.Empty },
                    { Define.EnumTreeAttribute.ToolTip, string.Empty },
                    { Define.EnumTreeAttribute.OrganizationId, string.Empty },
                    { Define.EnumTreeAttribute.EquipmentId, string.Empty }
                };

                using (CheckContext context=new CheckContext())
                {
                    if (account.QueryableOrganizationIds.Contains(organizationId))
                    {
                        var equipmentList = context.Equipments.Where(x => x.AffiliationOrganizationId == organizationId).OrderBy(x => x.EId).ToList();

                        foreach (var equipment in equipmentList)
                        {
                            var treeItem = new TreeItem() { Title = equipment.Name };

                            attributes[Define.EnumTreeAttribute.NodeType] = Define.EnumTreeNodeType.Equipment.ToString();
                            attributes[Define.EnumTreeAttribute.ToolTip] = string.Format("{0}/{1}", equipment.EId, equipment.Name);
                            attributes[Define.EnumTreeAttribute.OrganizationId] = organizationId.ToString();
                            attributes[Define.EnumTreeAttribute.EquipmentId] = equipment.EquipmentId.ToString();

                            foreach (var attribute in attributes)
                            {
                                treeItem.Attributes[attribute.Key.ToString()] = attribute.Value;
                            }

                            treeItemList.Add(treeItem);
                        }
                    }

                    var organizations = organizationList.Where(x => x.ParentId == organizationId && account.VisibleOrganizationIds.Contains(x.OrganizationId)).OrderBy(x => x.OId).ToList();

                    foreach (var organization in organizations)
                    {
                        var treeItem = new TreeItem() { Title = organization.Name };

                        attributes[Define.EnumTreeAttribute.NodeType] = Define.EnumTreeNodeType.Organization.ToString();
                        attributes[Define.EnumTreeAttribute.ToolTip] = string.Format("{0}/{1}", organization.OId, organization.Name);
                        attributes[Define.EnumTreeAttribute.OrganizationId] = organization.OrganizationId.ToString();
                        attributes[Define.EnumTreeAttribute.EquipmentId] = string.Empty;

                        foreach (var attribute in attributes)
                        {
                            treeItem.Attributes[attribute.Key.ToString()] = attribute.Value;
                        }

                        var haveDownStreamOrganization = organizationList.Any(x => x.ParentId == organization.OrganizationId && account.VisibleOrganizationIds.Contains(x.OrganizationId));

                        var haveEquipment = account.QueryableOrganizationIds.Contains(organization.OrganizationId) && context.Equipments.Any(x => x.AffiliationOrganizationId == organization.OrganizationId);

                        if (haveDownStreamOrganization || haveEquipment)
                        {
                            treeItem.State = "closed";
                        }

                        treeItemList.Add(treeItem);
                    }
                }

                result.ReturnData(treeItemList);
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult Create(CreateFormModel createFormModel)
        {
            RequestResult result = new RequestResult();

            try
            {
                using (CheckContext context=new CheckContext())
                {
                    var exists = context.Equipments.FirstOrDefault(x => x.AffiliationOrganizationId == new Guid(createFormModel.OrganizationId) && x.EId == createFormModel.FormInput.EId);

                    if (exists == null)
                    {
                        context.Equipments.Add(new Equipment()
                        {
                            EquipmentId = new Guid(createFormModel.EquipmentId),                            
                            EId = createFormModel.FormInput.EId,
                            Name = createFormModel.FormInput.Name,
                            IsFeelItemDefaultNormal = false,
                            AffiliationOrganizationId = new Guid(createFormModel.OrganizationId),
                            MaintenanceOrganizationId = new Guid(createFormModel.FormInput.MaintenanceOrganizationId),
                            LastModifyTime = DateTime.Now
                        });                        

                        foreach (var part in createFormModel.EPartModels)
                        {
                            context.EParts.Add(new EPart()
                            {
                                EPartId = new Guid(part.EPartId),
                                EquipmentId = new Guid(createFormModel.EquipmentId),
                                Name = part.Name
                            });                            
                        }

                        context.SaveChanges();

                        result.ReturnSuccessMessage(string.Format("{0} {1} {2}", Resources.Resource.Create, Resources.Resource.Equipment, Resources.Resource.Success));
                    }
                    else
                    {
                        result.ReturnFailedMessage(string.Format("{0} {1}", Resources.Resource.EId, Resources.Resource.Exists));
                    }
                }
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult SavePageState(List<ESpecificationModel> eSpecificationModels, List<string> specPageStateList)
        {
            RequestResult result = new RequestResult();

            try
            {
                int seq = 1;

                foreach (string pageState in specPageStateList)
                {
                    string[] temp = pageState.Split(Define.Seperators, StringSplitOptions.None);

                    string specUniqueID = temp[0];
                    string optionUniqueID = temp[1];
                    string value = temp[2];

                    var spec = eSpecificationModels.First(x => x.ESpecificationId == specUniqueID);

                    spec.ESOptionId = optionUniqueID;
                    spec.Value = value;
                    spec.Seq = seq;

                    seq++;
                }

                eSpecificationModels = eSpecificationModels.OrderBy(x => x.Seq).ToList();

                result.ReturnData(eSpecificationModels);
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult SavePageState(List<MaterialModel> materialModels, List<string> pageStateList)
        {
            RequestResult result = new RequestResult();

            try
            {
                foreach (string pageState in pageStateList)
                {
                    string[] temp = pageState.Split(Define.Seperators, StringSplitOptions.None);

                    string materialId = temp[0];
                    string qty = temp[1];

                    var material = materialModels.First(x => x.MaterialId == materialId);

                    if (!string.IsNullOrEmpty(qty))
                    {
                        int q;

                        if (int.TryParse(qty, out q))
                        {
                            material.Quantity = q;
                        }
                        else
                        {
                            material.Quantity = 0;
                        }
                    }
                    else
                    {
                        material.Quantity = 0;
                    }
                }

                result.ReturnData(materialModels);
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult SavePageState(List<EPartModel> ePartModels, List<string> pageStateList)
        {
            RequestResult result = new RequestResult();

            try
            {
                foreach (string pageState in pageStateList)
                {
                    string[] temp = pageState.Split(Define.Seperators, StringSplitOptions.None);

                    string ePartId = temp[0];
                    string materialId = temp[1];
                    string qty = temp[2];

                    var material = ePartModels.First(x => x.EPartId == ePartId).MaterialModels.First(x => x.MaterialId == materialId);

                    if (!string.IsNullOrEmpty(qty))
                    {
                        int q;

                        if (int.TryParse(qty, out q))
                        {
                            material.Quantity = q;
                        }
                        else
                        {
                            material.Quantity = 0;
                        }
                    }
                    else
                    {
                        material.Quantity = 0;
                    }
                }

                result.ReturnData(ePartModels);
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult GetDetailViewModel(string equipmentId, Account account)
        {
            RequestResult result = new RequestResult();

            try
            {
                using (CheckContext context=new CheckContext())
                {
                    var equipment = context.Equipments.Include("EquipmentMaterials").First(x => x.EquipmentId == new Guid(equipmentId));
                    var equipmentParts = equipment.EquipmentParts.ToList();

                    result.ReturnData(new DetailViewModel()
                    {
                        EquipmentId = equipment.EquipmentId.ToString(),
                        Permission = account.OrganizationPermission(equipment.AffiliationOrganizationId),
                        OrganizationId = equipment.AffiliationOrganizationId.ToString(),
                        ParentOrganizationFullName = OrganizationDataAccessor.GetOrganizationFullName(equipment.AffiliationOrganizationId),
                        EId = equipment.EId,
                        Name = equipment.Name,
                        MaintenanceOrganizationFullName = OrganizationDataAccessor.GetOrganizationFullName(equipment.MaintenanceOrganizationId),
                        EquipmentSpecificationModels = (from x in context.EquipmentSpecificationOptions
                                    join s in context.ESpecifications
                                    on x.ESpecificationId equals s.ESpecificationId
                                    where x.EquipmentId == equipment.EquipmentId
                                    select new ESpecificationModel
                                    {
                                        ESpecificationId = s.ESpecificationId.ToString(),
                                        Name = s.Name,
                                        ESOptionId = x.ESOptionId.ToString(),
                                        Value = x.Value,
                                        Seq = x.Seq,
                                        ESOptionModels = context.ESOptions.Where(o => o.EquipmentSpecificationId == s.ESpecificationId).Select(o => new Models.Maintenance.Equipment.ESOptionModel
                                        {
                                            Seq = o.Seq,
                                            EquipmentSpecificationId = o.EquipmentSpecificationId.ToString(),
                                            Name = o.Name, 
                                            EquipmentSpecificationOptionId = o.ESOptionId.ToString()
                                        }).OrderBy(o => o.Seq).ToList()
                                    }).OrderBy(x => x.Seq).ToList(),
                        MaterialModels = (from x in equipment.EquipmentMaterials
                                        join m in context.Materials
                                        on x.MaterialId equals m.MaterialId
                                        //where x.EquipmentId == equipment.UniqueID && x.PartUniqueID == "*"
                                        select new MaterialModel
                                        {
                                            MaterialId = m.MaterialId.ToString(),
                                            MId = m.MId,
                                            Name = m.Name,
                                            Quantity = x.Quantity
                                        }).OrderBy(x => x.MId).ToList(),
                        EquipmentPartModels = (from p in context.EParts
                                    where p.EquipmentId == equipment.EquipmentId
                                    select new EPartModel
                                    {
                                        EPartId = p.EPartId.ToString(),
                                        Name = p.Name,
                                        MaterialModels = (from epm in p.EquipmentPartMaterials
                                                                       join m in context.Materials
                                                                       on epm.MaterialId equals m.MaterialId
                                                                       select new MaterialModel
                                                                       {
                                                                           MaterialId=m.MaterialId.ToString(),
                                                                           MId=m.MId,
                                                                           Name=m.Name,
                                                                           Quantity=epm.Quantity
                                                                       }).OrderBy(x=>x.MId).ToList()                                                                                                             
                                    }).OrderBy(x => x.Name).ToList()
                    });
                }
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult Query(QueryParameters queryParameters, Account account)
        {
            RequestResult result = new RequestResult();

            try
            {
                using (CheckContext context=new CheckContext())
                {
                    var downStreamOrganizationIds = OrganizationDataAccessor.GetDownStreamOrganizationIds(new Guid(queryParameters.OrganizationId), true);

                    var query = context.Equipments.Where(x => downStreamOrganizationIds.Contains(x.AffiliationOrganizationId) && account.QueryableOrganizationIds.Contains(x.AffiliationOrganizationId)).AsQueryable();

                    if (!string.IsNullOrEmpty(queryParameters.Keyword))
                    {
                        query = query.Where(x => x.EId.Contains(queryParameters.Keyword) || x.Name.Contains(queryParameters.Keyword));
                    }

                    var organization = OrganizationDataAccessor.GetOrganization(new Guid(queryParameters.OrganizationId));

                    var model = new GridViewModel()
                    {
                        OrganizationId = queryParameters.OrganizationId,
                        Permission = account.OrganizationPermission(new Guid(queryParameters.OrganizationId)),
                        OrganizationName = organization.Name,
                        FullOrganizationName = organization.FullName,
                        Items = query.ToList().Select(x => new GridItem()
                        {
                            EquipmentId = x.EquipmentId.ToString(),
                            Permission = account.OrganizationPermission(x.AffiliationOrganizationId),
                            OrganizationName = OrganizationDataAccessor.GetOrganizationName(x.AffiliationOrganizationId),
                            MaintenanceOrganization = OrganizationDataAccessor.GetOrganizationName(x.MaintenanceOrganizationId),
                            EId = x.EId,
                            Name = x.Name
                        }).OrderBy(x => x.OrganizationName).ThenBy(x => x.EId).ToList()
                    };

                    var upStreamList = OrganizationDataAccessor.GetUpStreamOrganizationIds(new Guid(queryParameters.OrganizationId), false);
                    var downStreamList = OrganizationDataAccessor.GetDownStreamOrganizationIds(new Guid(queryParameters.OrganizationId), false);

                    foreach (var upStream in upStreamList)
                    {
                        if (account.EditableOrganizationIds.Any(x => x == upStream))
                        {
                            model.MoveToTargets.Add(new MoveToTarget()
                            {
                                Id = upStream,
                                Name = OrganizationDataAccessor.GetOrganizationFullName(upStream),
                                Direction = Define.EnumMoveDirection.Up
                            });
                        }
                    }

                    foreach (var downStream in downStreamList)
                    {
                        if (account.EditableOrganizationIds.Any(x => x == downStream))
                        {
                            model.MoveToTargets.Add(new MoveToTarget()
                            {
                                Id = downStream,
                                Name = OrganizationDataAccessor.GetOrganizationFullName(downStream),
                                Direction = Define.EnumMoveDirection.Down
                            });
                        }
                    }

                    model.MoveToTargets = model.MoveToTargets.OrderBy(x => x.Name).ToList();

                    result.ReturnData(model);
                }
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }

        public static RequestResult GetRootTreeItems(List<Organization> organizationList, Guid rootOrganizationId, Account account)
        {
            RequestResult result = new RequestResult();

            try
            {
                var treeItemList = new List<TreeItem>();

                var attributes = new Dictionary<Define.EnumTreeAttribute, string>()
                {
                    { Define.EnumTreeAttribute.NodeType, string.Empty },
                    { Define.EnumTreeAttribute.ToolTip, string.Empty },
                    { Define.EnumTreeAttribute.OrganizationId, string.Empty },
                    { Define.EnumTreeAttribute.EquipmentId, string.Empty }
                };

                using (CheckContext context=new CheckContext())
                {
                    var organization = organizationList.First(x => x.OrganizationId == rootOrganizationId);

                    var treeItem = new TreeItem() { Title = organization.Name };

                    attributes[Define.EnumTreeAttribute.NodeType] = Define.EnumTreeNodeType.Organization.ToString();
                    attributes[Define.EnumTreeAttribute.ToolTip] = string.Format("{0}/{1}", organization.OId, organization.Name);
                    attributes[Define.EnumTreeAttribute.OrganizationId] = organization.OrganizationId.ToString();
                    attributes[Define.EnumTreeAttribute.EquipmentId] = string.Empty;

                    foreach (var attribute in attributes)
                    {
                        treeItem.Attributes[attribute.Key.ToString()] = attribute.Value;
                    }

                    var haveDownStreamOrganization = organizationList.Any(x => x.ParentId == organization.OrganizationId && account.VisibleOrganizationIds.Contains(x.OrganizationId));

                    var haveEquipment = account.QueryableOrganizationIds.Contains(organization.OrganizationId) && context.Equipments.Any(x => x.AffiliationOrganizationId == organization.OrganizationId);

                    if (haveDownStreamOrganization || haveEquipment)
                    {
                        treeItem.State = "closed";
                    }

                    treeItemList.Add(treeItem);
                }

                result.ReturnData(treeItemList);
            }
            catch (Exception ex)
            {
                var err = new Error(MethodBase.GetCurrentMethod(), ex);

                Logger.Log(err);

                result.ReturnError(err);
            }

            return result;
        }
    }
}
