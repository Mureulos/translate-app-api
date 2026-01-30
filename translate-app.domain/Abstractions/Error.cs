using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translate_app.Domain.Abstractions
{
    public record Error(string Code, string Message)
    {
        public static Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("Error.NullValue", "A null value was provided.");
        public static Error InvalidCredentials = new("Auth.InvalidCredentials", "Invalid credentials.");
    }
}