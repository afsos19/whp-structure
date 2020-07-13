using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Enums
{
    public enum FileSKUStatusEnum
    {
        PendingClassification = 1,
        NotParticipation = 2,
        AutomaticValidate = 3,
        PendingWHP = 4,
        Validated = 5,
        NotValidated = 6,
        NotFound = 7
    }
}
