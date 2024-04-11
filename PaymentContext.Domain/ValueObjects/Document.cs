using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObjects;


using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        public Document(string document, EDocumentType type)
        {
            Number = document;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Validate(), "Document.Number", "Documento inválido")
                );
        }

        private bool Validate() { 
            
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
            {
                return true;
            }

            if (Type == EDocumentType.CPF && Number.Length == 11)
            {
                return true;
            }

            return false;
        }
    }
}
