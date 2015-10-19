// ReSharper disable All
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Extensions;
using PetaPoco;
using MixERP.Net.Entities.Core;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MixERP.Net.Schemas.Core.Data
{
    /// <summary>
    /// Prepares, validates, and executes the function "core.count_purchases(_item_id integer, _unit_id integer, _store_id integer)" on the database.
    /// </summary>
    public class CountPurchasesProcedure : DbAccess
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "core";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "count_purchases";
        /// <summary>
        /// Login id of application user accessing this PostgreSQL function.
        /// </summary>
        public long _LoginId { get; set; }
        /// <summary>
        /// User id of application user accessing this table.
        /// </summary>
        public int _UserId { get; set; }
        /// <summary>
        /// The name of the database on which queries are being executed to.
        /// </summary>
        public string _Catalog { get; set; }

        /// <summary>
        /// Maps to "_item_id" argument of the function "core.count_purchases".
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Maps to "_unit_id" argument of the function "core.count_purchases".
        /// </summary>
        public int UnitId { get; set; }
        /// <summary>
        /// Maps to "_store_id" argument of the function "core.count_purchases".
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "core.count_purchases(_item_id integer, _unit_id integer, _store_id integer)" on the database.
        /// </summary>
        public CountPurchasesProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "core.count_purchases(_item_id integer, _unit_id integer, _store_id integer)" on the database.
        /// </summary>
        /// <param name="itemId">Enter argument value for "_item_id" parameter of the function "core.count_purchases".</param>
        /// <param name="unitId">Enter argument value for "_unit_id" parameter of the function "core.count_purchases".</param>
        /// <param name="storeId">Enter argument value for "_store_id" parameter of the function "core.count_purchases".</param>
        public CountPurchasesProcedure(int itemId, int unitId, int storeId)
        {
            this.ItemId = itemId;
            this.UnitId = unitId;
            this.StoreId = storeId;
        }
        /// <summary>
        /// Prepares and executes the function "core.count_purchases".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public decimal Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"CountPurchasesProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM core.count_purchases(@ItemId, @UnitId, @StoreId);";

            query = query.ReplaceWholeWord("@ItemId", "@0::integer");
            query = query.ReplaceWholeWord("@UnitId", "@1::integer");
            query = query.ReplaceWholeWord("@StoreId", "@2::integer");


            List<object> parameters = new List<object>();
            parameters.Add(this.ItemId);
            parameters.Add(this.UnitId);
            parameters.Add(this.StoreId);

            return Factory.Scalar<decimal>(this._Catalog, query, parameters.ToArray());
        }


    }
}