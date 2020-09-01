using GraphQL.Types;

using GraphQL.Server.Authorization.AspNetCore;
namespace GraphqlLoginService.Account
{
    public class AccountSchema
    {
        private ISchema _schema { get; set; }
        public ISchema GraphQLSchema
        {
            get
            {
                return this._schema;
            }
        }

        public AccountSchema()
        {
           
            this._schema = GraphQL.Types.Schema.For(@"
           

            type Query {
              login: String          
              logout:String          
              hello: String
            }
            ", _ =>
            {
               
                _.Types.Include<Query>();


            });
        }

    }

}
