using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IntelVault.ApplicationCore.Model;

public enum TypeOfCybInt
{
    MalwareAttacks,
    EmailPhishing,
    DoS,
    MaliciousSoftwareThatEncrypts,
    ManInTheMiddle,
    SqlInjection,
    CrossSiteScripting,
    InsiderThreats,
    ZeroDayExploits,
    DataBreaches,
    IoT,
    SocialEngineeringAttacks
}