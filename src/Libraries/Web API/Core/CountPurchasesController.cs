// ReSharper disable All
using MixERP.Net.Framework;
using MixERP.Net.Entities.Core;
using MixERP.Net.Schemas.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using PetaPoco;
using MixERP.Net.EntityParser;
namespace MixERP.Net.Api.Core
{
    /// <summary>
    /// Provides a direct HTTP access to execute the function CountPurchases.
    /// </summary>
    [RoutePrefix("api/v1.5/core/procedures/count-purchases")]
    public class CountPurchasesController : ApiController
    {
        /// <summary>
        /// Login id of application user accessing this API.
        /// </summary>
        public long _LoginId { get; set; }

        /// <summary>
        /// User id of application user accessing this API.
        /// </summary>
        public int _UserId { get; set; }

        /// <summary>
        /// Currently logged in office id of application user accessing this API.
        /// </summary>
        public int _OfficeId { get; set; }

        /// <summary>
        /// The name of the database where queries are being executed on.
        /// </summary>
        public string _Catalog { get; set; }

        private CountPurchasesProcedure procedure;
        public class Annotation
        {
            public int ItemId { get; set; }
            public int UnitId { get; set; }
            public int StoreId { get; set; }
        }

        public CountPurchasesController()
        {
            this._LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this._UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this._OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this._Catalog = AppUsers.GetCurrentUserDB();
            this.procedure = new CountPurchasesProcedure
            {
                _Catalog = this._Catalog,
                _LoginId = this._LoginId,
                _UserId = this._UserId
            };
        }
        /// <summary>
        ///     Creates meta information of "count purchases" annotation.
        /// </summary>
        /// <returns>Returns the "count purchases" annotation meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("annotation")]
        [Route("~/api/core/procedures/count-purchases/annotation")]
        public EntityView GetAnnotation()
        {
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "_item_id",  PropertyName = "ItemId",  DataType = "int",  DbDataType = "integer",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "_unit_id",  PropertyName = "UnitId",  DataType = "int",  DbDataType = "integer",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 },
                                        new EntityColumn { ColumnName = "_store_id",  PropertyName = "StoreId",  DataType = "int",  DbDataType = "integer",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }


        [AcceptVerbs("POST")]
        [Route("execute")]
        [Route("~/api/core/procedures/count-purchases/execute")]
        public decimal Execute([FromBody] Annotation annotation)
        {
            try
            {
                this.procedure.ItemId = annotation.ItemId;
                this.procedure.UnitId = annotation.UnitId;
                this.procedure.StoreId = annotation.StoreId;


                return this.procedure.Execute();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (MixERPException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}