﻿using GraphQL.Types;


namespace Auth.Service
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
            input LoginInput{
                login:String!
                password:String!
            }
            type AuthResponse{
                accessToken:String
                tokenType:String
                refreshToken:String
                expiresIn:Int
                expires:DateTime
            }

            type Query {
              login(login:LoginInput!): AuthResponse    
              refreshToken: AuthResponse    
              logout: AuthResponse    
              hello: String
            }
            ", _ =>
            {
               
                _.Types.Include<Query>();


            });
         
        }

    }

}