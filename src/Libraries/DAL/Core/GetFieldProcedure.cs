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
    /// Prepares, validates, and executes the function "core.get_field(_hstore hstore, _column_name text)" on the database.
    /// </summary>
    public class GetFieldProcedure : DbAccess
    {
        /// <summary>
        /// The schema of this PostgreSQL function.
        /// </summary>
        public override string _ObjectNamespace => "core";
        /// <summary>
        /// The schema unqualified name of this PostgreSQL function.
        /// </summary>
        public override string _ObjectName => "get_field";
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
        /// Maps to "_hstore" argument of the function "core.get_field".
        /// </summary>
        public string Hstore { get; set; }
        /// <summary>
        /// Maps to "_column_name" argument of the function "core.get_field".
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Prepares, validates, and executes the function "core.get_field(_hstore hstore, _column_name text)" on the database.
        /// </summary>
        public GetFieldProcedure()
        {
        }

        /// <summary>
        /// Prepares, validates, and executes the function "core.get_field(_hstore hstore, _column_name text)" on the database.
        /// </summary>
        /// <param name="hstore">Enter argument value for "_hstore" parameter of the function "core.get_field".</param>
        /// <param name="columnName">Enter argument value for "_column_name" parameter of the function "core.get_field".</param>
        public GetFieldProcedure(string hstore, string columnName)
        {
            this.Hstore = hstore;
            this.ColumnName = columnName;
        }
        /// <summary>
        /// Prepares and executes the function "core.get_field".
        /// </summary>
        /// <exception cref="UnauthorizedException">Thown when the application user does not have sufficient privilege to perform this action.</exception>
        public string Execute()
        {
            if (!this.SkipValidation)
            {
                if (!this.Validated)
                {
                    this.Validate(AccessTypeEnum.Execute, this._LoginId, this._Catalog, false);
                }
                if (!this.HasAccess)
                {
                    Log.Information("Access to the function \"GetFieldProcedure\" was denied to the user with Login ID {LoginId}.", this._LoginId);
                    throw new UnauthorizedException("Access is denied.");
                }
            }
            string query = "SELECT * FROM core.get_field(@Hstore, @ColumnName);";

            query = query.ReplaceWholeWord("@Hstore", "@0::hstore");
            query = query.ReplaceWholeWord("@ColumnName", "@1::text");


            List<object> parameters = new List<object>();
            parameters.Add(this.Hstore);
            parameters.Add(this.ColumnName);

            return Factory.Scalar<string>(this._Catalog, query, parameters.ToArray());
        }


    }
}