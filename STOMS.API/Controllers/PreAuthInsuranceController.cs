using STOMS.API.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using STOMS.API.Models;

namespace STOMS.API.Controllers
{
    public class PreAuthInsuranceController: ApiController
    {

        [Route("api/Insurance/")]
        public HttpResponseMessage Post([FromBody]PreInsurance preinsurance)
        {
            ApiDA apiDA = new ApiDA();
            HttpResponseMessage responce = new HttpResponseMessage();

            // bool isEntity=apiDA.CheckEntity(kitOrder.AuthenticateToken);
            int TenantID = apiDA.CheckEntity(preinsurance.PreAuthoriztionToken);
            preinsurance.TenantID = TenantID;

            if (TenantID != 0)
            {
                apiDA.SaveInsurance(preinsurance);
            }


             return responce;

        }
    }
}