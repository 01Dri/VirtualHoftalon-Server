using System.ComponentModel;

namespace VirtualHoftalon_Server.Enums;

public enum SectorTag
{
    [Description ("Para pacientes não preferenciais")]
    X = 1,
    [Description ("Para pacientes preferenciais")]
    P = 2
}