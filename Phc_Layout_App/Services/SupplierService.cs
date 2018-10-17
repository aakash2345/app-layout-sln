using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phc_Layout.Models;

namespace Phc_Layout.Services
{
    public class SupplierService
    {
        readonly HTTPOperations _httpOperations;
        public SupplierService(HTTPOperations httpOperations)
        {
            this._httpOperations = httpOperations;
        }

        #region 'Public Method'

        public List<SupplierOption> GetSupplierCollections(String id)
        {
            return this.SupplierCollections(id);

        }
        #endregion

        #region 'Public Method'

        public List<SupplierAddressModel> GetSupplierAddressCollection(String id)
        {
            return this.SupplierAddressCollections(id);

        }
        #endregion

        #region 'Public Method'

        public List<DivisionsLocationOption> GetDivisionLocationCollections(String webId, String masterId)
        {
            return this.DivisionLocationCollections(webId, masterId);

        }
        #endregion

        #region 'Private Method'

        public List<SupplierOption> SupplierCollections(String id)
        {
            List<SupplierOption> supplierOptions = null;
            try
            {
                String response = this._httpOperations.GetRequest("Supplier/GetSuppliers", id).ToString();

                if (!String.IsNullOrWhiteSpace(response))
                {
                    supplierOptions = JsonConvert.DeserializeObject<List<SupplierOption>>(response);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return supplierOptions;

        }
        #endregion

        #region
        List<DivisionsLocationOption> DivisionLocationCollections(String webId, String masterId)
        {
            List<DivisionsLocationOption> divisionLocationOptions = null;
            try
            {

                DivisionLoctionModel divisionLoctionModel = new DivisionLoctionModel
                {
                    MasterId = masterId,
                    WebId = webId
                };

                String jsonRequestData = JsonConvert.SerializeObject(divisionLoctionModel);                

                String response = this._httpOperations.PostRequest("DivisionLocation/GetDivisionLocation", jsonRequestData).ToString();

                if (!String.IsNullOrWhiteSpace(response))
                {
                    divisionLocationOptions = JsonConvert.DeserializeObject<List<DivisionsLocationOption>>(response);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return divisionLocationOptions;

        }
        #endregion

        #region
        public List<SupplierAddressModel> SupplierAddressCollections(String masterId)
        {

            List<SupplierAddressModel> supplierAddressOptions = null;
            try
            {
                String response = this._httpOperations.GetRequest("Supplier/GetSuppliersAddress", masterId).ToString();

                if (!String.IsNullOrWhiteSpace(response))
                {
                    supplierAddressOptions = JsonConvert.DeserializeObject<List<SupplierAddressModel>>(response);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return supplierAddressOptions.Where(sa => sa.SupplierOrganizationId == masterId).ToList();

        }
        #endregion
    }
}
