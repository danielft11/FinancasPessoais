namespace FinancasPessoais.Domain.Utils
{
    public static class Constants
    {
        public const string INVALID_ID = "ID cannot be null.";
        public const string INVALID_SUBCATEGORY_ID = "Invalid subcategoryId, financial release cannot be created without a subcategory.";
        public const string INVALID_CREDIT_CARD_ID = "Invalid creditCardId, credit card release cannot be created without a creditCard.";
        public const string INVALID_VALUE = "Invalid value, release value cannot be greater than the account balance.";
        public const string SUBCATEGORY_WITHOUT_CATEGORY = "Subcategory cannot be created without a category.";
        public const string ACCOUNT_ALREADY_EXISTS = "Falha ao inserir a conta bancária: já existe uma conta bancária cadastrada com o mesmo nome e número informados.";
        public const string ACCOUNT_WITH_FINANCIAL_RELEASES = "Não foi possível remover a conta pois existem lançamentos financeiros que estão utilizando a mesma.";
        public const string CATEGORY_WITH_SUBCATEGORIES = "Failed to remove category. The category is being used by some subcategory(ies).";
        public const string AMOUNT_GREATHER_THAN_ACCOUNT_BALANCE = "Impossível incluir o lançamento financeiro: o valor informado é maior que o saldo da conta.";
        public const string FAILED_INCLUDE_FINANCIALRELEASE_WITHOUT_ACCOUNT = "Falha ao incluir lançamento: o lançamento deve pertencer a uma conta ou cartão de crédito.";
        public const string FAILED_INCLUDE_FINANCIALRELEASE_DATE_GREATER_THAN_CURRENT_DATE = "Falha ao incluir o lançamento financeiro. A data do lançamento não pode ser maior que a data atual.";
        public const string FAILED_INCLUDE_CREDITCARD_RELEASE_RELEASE_DATE_GREATER_THAN_CURRENT_DATE = "Falha ao incluir lançamento no cartão de crédito. A data do lançamento não pode ser maior que a data atual.";
        public const string PAYMENT_REGISTERED_SUCCESSFULY = "Pagamento da despesa realizado com sucesso!";
        public const string PAYMENT_DATE_INVALID = "Payment date cannot be less than the release date and cannot be greater than the current date.";
        public const string EXTRACT_NOT_FOUND = "Não existe extrato para o período informado.";
        public const string INVALID_RELEASE_TYPE = "Tipo de lançamento inválido.";
        public const string ACCOUNT_REMOVED_SUCCESFULLY = "Conta removida com sucesso";
        public const string CREDIT_CARD_ALREADY_EXISTS = "Falha ao criar o cartão de crédito: já existe um cartão de crédito cadastrado com este número.";
        public const string CREDIT_CARD_WITH_FINANCIAL_RELEASES = "Não foi possível remover este cartão pois existem compras que foram lançadas no mesmo.";

        public static string FieldRequired(string field) 
        {
            return $"{field} is required.";
        }

        public static string InvalidFieldMinimumCharacters(string field, int numCharacters) 
        {
            return $"Invalid {field}. Minimum {numCharacters} characters.";
        }

        public static string InvalidFieldMaximumCharacters(string field, int numCharacters)
        {
            return $"Invalid {field}. Maximum {numCharacters} characters.";
        }

        public static string InvalidFieldNumberCharacters(string field, int numCharacters)
        {
            return $"Invalid {field}. Must be {numCharacters} characters.";
        }

        public static string InvalidFieldGreatherThan(string field, int numCharacters)
        {
            return $"Invalid {field}. Must be greather than {numCharacters} characters.";
        }

        public static string EntityOperationSucessfully(string entity, Operations operation)
        {
            return operation == Operations.Insert ? $"{entity} inserida com sucesso!" :
                operation == Operations.Update ? $"{entity} atualizada com sucesso!"  :
                operation == Operations.Remove ? $"{entity} removida com sucesso!"    :
                "Operação não permitida";
        }

        public static string GenericFailure(Operations operation, string entity, string failure)
        {
            string operationString = string.Empty;
            switch (operation)
            {
                case Operations.Insert:
                    operationString = "insert";
                    break;
                case Operations.Update:
                    operationString = "update";
                    break;
                case Operations.Remove:
                    operationString = "remove";
                    break;
            }

            return $"Generic failure to {operationString} {entity}: {failure}";
        }

        public static string EntityNotFound(string entity) 
        {
            return $"{entity} não encontrado.";
        }

        public static string CreditCardInvalidDate(string field) 
        {
            return $"Failed to insert credit card: {field} must be between 1 and 31.";
        }

        public static string EntityNotExistError(Operations operation, string entity, string entityNotExist) 
        {
            string operationString = string.Empty;
            switch (operation)
            {
                case Operations.Insert:
                    operationString = "inserir";
                    break;
                case Operations.Update:
                    operationString = "atualizar";
                    break;
                case Operations.Remove:
                    operationString = "remover";
                    break;
            }

            return $"Falha ao {operationString} {entity}. A {entityNotExist} informada não existe.";
        }

        public static string EntityAlreadyExist(string entity, Operations operation)
        {
            string operationString = string.Empty;
            switch (operation)
            {
                case Operations.Insert:
                    operationString = "insert";
                    break;
                case Operations.Update:
                    operationString = "update";
                    break;
                case Operations.Remove:
                    operationString = "remove";
                    break;
            }
            return $"Failed to {operationString} {entity}. There is already another {entity} registered with this code.";
        }

    }
}
