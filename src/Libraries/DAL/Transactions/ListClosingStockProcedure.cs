// ReSharper disable All
using MixERP.Net.DbFactory;
using MixERP.Net.Framework;
using MixERP.Net.Framework.Extensions;
using PetaPoco;
using MixERP.Net.Entities.Transactions;
using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MixERP.Net.Schemas.Transactions.Data
{
    /// <summary>
    /// Prepares, validates, and executes the function "transactions.list_closing_stock(_store_id integer)" on the database.
    /// </summary>
    public class ListClosingStockProcedure : DbAccess
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "transactions";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "list_closing_stock";
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
        /// Maps to "_store_id" argument of the function "transactions.list_closing_stock".
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.list_closing_stock(_store_id integer)" on the database.
        /// </summary>
        public ListClosingStockProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "transactions.list_closing_stock(_store_id integer)" on the database.
        /// </summary>
        /// <param name="storeId">Enter argument value for "_store_id" parameter of the function "transactions.list_closing_stock".</param>
        public ListClosingStockProcedure(int storeId)
        {
            this.StoreId = storeId;
        }
        /// <summary>
        /// Prepares and executes the function "transactions.list_closing_stock".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public IEnumerable<DbListClosingStockResult> Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"ListClosingStockProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM transactions.list_closing_stock(@StoreId);";

            query = query.ReplaceWholeWord("@StoreId", "@0::integer");


            List<object> parameters = new List<object>();
            parameters.Add(this.StoreId);

            return Factory.Get<DbListClosingStockResult>(this._Catalog, query, parameters.ToArray());
        }


    }
}